Imports DataElements

Public Class Core
    Private _level2 As DataExchange.Level2 = Nothing

    Private _gui As GuiOperator = Nothing
    Public WriteOnly Property Gui As GuiOperator
        Set(value As GuiOperator)
            _gui = value
        End Set
    End Property

#Region "Singleton"
    Private Shared _instance As Core = Nothing
    Public Shared ReadOnly Property Instance As Core
        Get
            If _instance Is Nothing Then _instance = New Core
            Return _instance
        End Get
    End Property
    Private Sub New()

        _level2 = New DataExchange.Level2

        _mpg = New DataExchange.MPG

        _CurrentResult = New Result
        _settingManager = New Settings.SettingsManager

        _settingManager.Load(CurrentSettings)
        _settingManager.Save(CurrentSettings)

        _printer = New Printer
    End Sub
#End Region

#Region "Current result"
    Private _CurrentResult As Result
    Public Property CurrentResult() As Result
        Get
            Return _CurrentResult
        End Get
        Set(ByVal value As Result)
            _CurrentResult = value
        End Set
    End Property

    Public Event CurrentResultChanged()
    Public Sub NotifyCurrentResultChanged()
        RaiseEvent CurrentResultChanged()
    End Sub
#End Region
    
#Region "Level2 Workorder"
    Public Event CurrentWorkOrderChanged()

    Private _currentWorkOrderDgCore As WorkOrderDgCore = Nothing
    Private _currentWorkOrderToroid As WorkOrderToroid = Nothing
    Public Property CurrentWorkOrder As WorkOrder
        Set(value As WorkOrder)
            If TypeOf value Is WorkOrderDgCore Then
                _currentWorkOrderDgCore = TryCast(value, WorkOrderDgCore)
                _currentWorkOrderToroid = Nothing
            Else
                _currentWorkOrderToroid = TryCast(value, WorkOrderToroid)
                _currentWorkOrderDgCore = Nothing
            End If
            RaiseEvent CurrentWorkOrderChanged()
        End Set
        Get
            If _currentWorkOrderDgCore IsNot Nothing Then : Return _currentWorkOrderDgCore
            ElseIf _currentWorkOrderToroid IsNot Nothing Then : Return _currentWorkOrderToroid
            Else : Return Nothing
            End If
        End Get
    End Property

    Public Sub CurrentWorkOrder_Refresh(ByVal workOrderNo As String)

        If Not _level2.RequestData(workOrderNo) Then
            CurrentResults = New List(Of Result)
            Exit Sub
        End If

        Dim samples = From item In CurrentResults()
                      Where item.Test.Length = 0

        CurrentWorkOrder.QuantityDone = CUInt(samples.Count)
    End Sub
#End Region

#Region "Level2 Results"
    Public Event CurrentResultsChanged()

    Private _currentResults As List(Of Result) = Nothing
    Public Property CurrentResults As List(Of Result)
        Set(value As List(Of Result))
            _currentResults = value
            RaiseEvent CurrentResultsChanged()
        End Set
        Get
            If _currentResults Is Nothing Then Return New List(Of Result)

            If CurrentWorkOrder.Sample.SampleType = Sample.SampleTypeT.Toroid Then
                Dim sortedResults = From item As Result In _currentResults
                                    Order By item.SortedSerial, item.Test

                _currentResults = sortedResults.ToList
            End If
            Return _currentResults
        End Get
    End Property

    Public Function CurrentResultSave(ByVal state As Result.StateT) As Boolean
        Try
            Dim tmpResults As List(Of Result) = Nothing

            _CurrentResult.State = state
            _CurrentResult.MpgDevice = CurrentSettings.MpgDeviceId
            Select Case state
                Case Result.StateT.Abandon

                    Return True
                Case Result.StateT.Accept
                    Return _level2.SaveResult(CurrentWorkOrder, _CurrentResult)
                Case Result.StateT.Overwrite
                    If Not SubResults(CurrentWorkOrder.Sample.SerialNo, tmpResults) Then Return False

                    _CurrentResult.Retest = tmpResults.Last.Retest
                    _CurrentResult.DuplicateTest = tmpResults.Last.DuplicateTest
                    Return _level2.SaveResult(CurrentWorkOrder, _CurrentResult)
                Case Result.StateT.Reprocessed
                    Dim retest As Integer = 0
                    If SubResults(CurrentWorkOrder.Sample.SerialNo, tmpResults) Then
                        retest = tmpResults.Last.Retest
                        _CurrentResult.DuplicateTest = tmpResults.Last.DuplicateTest
                    End If
                    retest += 1
                    _CurrentResult.Retest = retest
                    Return _level2.SaveResult(CurrentWorkOrder, _CurrentResult)

                Case Result.StateT.Remade
                    Dim duplicateTest As String = ""
                    _CurrentResult.Retest = 0
                    If SubResults(CurrentWorkOrder.Sample.SerialNo, tmpResults) Then
                        duplicateTest = tmpResults.Last.DuplicateTest
                    End If
                    If duplicateTest = "" Then
                        _CurrentResult.DuplicateTest = "A"
                    Else
                        If System.Text.ASCIIEncoding.ASCII.GetBytes(duplicateTest).Count < 0 Then Return False
                        Dim bDuplicateTest As Byte = System.Text.ASCIIEncoding.ASCII.GetBytes(duplicateTest)(0)
                        bDuplicateTest = CByte(bDuplicateTest + 1)
                        _CurrentResult.DuplicateTest = System.Text.ASCIIEncoding.ASCII.GetChars(New Byte() {bDuplicateTest})
                    End If
                    Return _level2.SaveResult(CurrentWorkOrder, _CurrentResult)
            End Select

            Return False
        Catch ex As Exception
            Logger.Logger.Instance.Log(GetType(Core).ToString + ".CurrentResultSave(): ", Logger.Logger.eStatus.Exception, ex)
            Return False
        End Try
    End Function

    Private Function SubResults(ByVal serialNumber As String, ByRef results As List(Of Result)) As Boolean
        Dim subResultsView As IEnumerable(Of Result) = Nothing
        subResultsView = From element In CurrentResults() Where element.Serial = serialNumber

        If subResultsView.Count = 0 Then Return False
        results = subResultsView.ToList
        Return True
    End Function
#End Region

#Region "MPG"
    Private _mpg As DataExchange.MPG = Nothing

    Public Function StartMeasurement(ByVal serialNumber As String, ByVal weight As Double) As Boolean
        If CurrentWorkOrder Is Nothing Then Return False

        Dim tmp As Sample = CurrentWorkOrder.Sample
        tmp.weight = weight
        tmp.SerialNo = serialNumber
        tmp.density = CurrentSettings.DensityDefault

        CurrentWorkOrder.Sample = tmp

        Dim coilsystem As String = CurrentSettings.HarnessDefaultN1.ToString + ":" + CurrentSettings.HarnessDefaultN2.ToString
        Return _mpg.StartMeasurement(CurrentWorkOrder, coilsystem, CurrentWorkOrder.Frequency, CurrentWorkOrder.NominalInduction, False)
    End Function
    Public Sub StopMeasurement()
        _mpg.StopMeasurement()
    End Sub

    Public Sub MeasurementDone(ByVal sampleName As String)
        'GuiOperatorForm.Instance.BeginInvoke(Sub()
        '                                         GuiOperatorForm.Instance.btnStart_Enabled(True)
        '                                         GuiOperatorForm.Instance.tbStateDisplay_update("")
        '                                     End Sub)
        '_mpg.RemoveSample(sampleName)
    End Sub

    Public Sub CloseApp()
        _gui.BeginInvoke(New MethodInvoker(AddressOf _gui.Close))
    End Sub
    Public Sub HideApp()
        _gui.BeginInvoke(Sub() _gui.Visible = False)
    End Sub
    Public Sub ShowApp()
        _gui.BeginInvoke(Sub() _gui.Visible = True)
    End Sub

    Public Sub CloseMPGexpert()
        _mpg.CloseMPGexpert()
    End Sub

    Public Function GetCurrentUser() As String
        Return _mpg.GetCurrentUser
    End Function
    Public Function GetCurrentUserRole() As String
        Return _mpg.GetCurrentUserRole
    End Function
#End Region

#Region "Printer"
    Private _printer As Printer
    Public Sub Print(Optional ByVal resultIndex As Integer = -1)
        If resultIndex > -1 Then
            _printer.Print(CurrentWorkOrder, CurrentResults(resultIndex), CurrentSettings)
        Else
            _printer.Print(CurrentWorkOrder, _CurrentResult, CurrentSettings)
        End If
    End Sub
    Public Sub PrintSetup()
        _printer.ShowPrintSetup()
    End Sub
#End Region

#Region "Settings"
    Private _settingManager As Settings.SettingsManager = Nothing
    Public Property CurrentSettings As Settings.Settings

    Public Sub LoadSettings()
        _settingManager.Load(CurrentSettings)
    End Sub
    Public Sub SaveSettings()
        _settingManager.Save(CurrentSettings)
    End Sub
#End Region

    Public Function CheckWeight(ByVal weight As Double) As Boolean
        Dim maxWeight As Double = CurrentWorkOrder.NominalWeight + ((CurrentWorkOrder.NominalWeight / 100) * CurrentSettings.WeightRange)
        Dim minWeight As Double = CurrentWorkOrder.NominalWeight - ((CurrentWorkOrder.NominalWeight / 100) * CurrentSettings.WeightRange)

        If maxWeight < weight Then Return False
        If minWeight > weight Then Return False

        Return True
    End Function

    Public Sub UpdateOperatorHarnessInToroidWorkOrder()
        If _currentWorkOrderToroid Is Nothing Then Exit Sub
        If Not TypeOf _currentWorkOrderToroid Is WorkOrderToroid Then Exit Sub

        With _currentWorkOrderToroid
            .OperatorNumberOfTurns = CurrentSettings.HarnessDefaultN1
            .OperatorMaxAmps = .MaxAmps / (.OperatorNumberOfTurns / .NumberOfTurns)
        End With
    End Sub
End Class


