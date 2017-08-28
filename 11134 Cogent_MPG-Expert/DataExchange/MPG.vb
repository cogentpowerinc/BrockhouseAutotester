Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Ipc
Imports DataElements

Namespace DataExchange
    Public Class MPG
        Private Class DataExchangeService
            Inherits MarshalByRefObject
            Implements DataExchangeInterface.Impg200Calls

#Region "not implemented"
            Public Sub SaveResults(results As System.Collections.Generic.List(Of DataExchangeInterface.Result)) Implements DataExchangeInterface.Impg200Calls.SaveResults

            End Sub
            Public Sub SendCurrentStep([step] As DataExchangeInterface.Result) Implements DataExchangeInterface.Impg200Calls.SendCurrentStep

            End Sub
            Public Sub Dispose() Implements DataExchangeInterface.Impg200Calls.Dispose

            End Sub

            Public Sub Init() Implements DataExchangeInterface.Impg200Calls.Init

            End Sub
#End Region

            Public Sub SendFinalResult(result As DataExchangeInterface.Result) Implements DataExchangeInterface.Impg200Calls.SendFinalResult
                Try
                    Dim newResult As New Result
                    With newResult
                        .Date = System.DateTime.Now
                        .User = Core.Instance.GetCurrentUser
                        .Serial = result.sample.name
                        .PercentageActualJ = CSng(Math.PercentageJ(result.jMax, result.setPoint.value))
                        .Weight = CSng(Core.Instance.CurrentWorkOrder.Sample.weight)

                        Select Case result.resultState
                            Case DataExchangeInterface.eResultStatusT.OverflowGenerator : .Grade = DataElements.Result.GradeT.IOR
                        End Select

                        If Core.Instance.CurrentWorkOrder.Sample.SampleType = Sample.SampleTypeT.Toroid Then
                            .Amps = CSng(result.iEff)
                            Dim workOrder As WorkOrderToroid = TryCast(Core.Instance.CurrentWorkOrder, WorkOrderToroid)
                            .Grade = Math.GetToroidGrade(workOrder.OperatorMaxAmps, result.iEff)
                            .PercentageGreenLossLevel = Math.PercentageGreenLossLevel(result.iEff, workOrder.OperatorMaxAmps)
                            .Volts = CSng(result.uEff)
                            .NumberOfTurns = workOrder.OperatorNumberOfTurns
                        Else
                            .AmpTurns = CSng(Math.AmpsTurn(result.coilsystem.n1, result.iEff))

                            Dim workOrder As WorkOrderDgCore = TryCast(Core.Instance.CurrentWorkOrder, WorkOrderDgCore)
                            Dim realWeight As Double = Math.CalcDgCoreWeight(result.sample.outerDiameter, _
                                                      result.sample.innerDiameter, _
                                                      result.sample.thickness, _
                                                      result.sample.stackingFactor, _
                                                      result.sample.density)
                            .Watts = CSng(result.ps * realWeight)
                            .Grade = Math.GetDgCoreGrade(workOrder.LossLimit_MaxGreen, _
                                                   workOrder.LossLimit_MaxYellow, _
                                                   workOrder.LossLimit_MaxRed, .Watts)

                            .PercentageGreenLossLevel = Math.PercentageGreenLossLevel(.Watts, workOrder.LossLimit_MaxGreen)
                        End If
                    End With

                    Core.Instance.CurrentResult = newResult
                    Core.Instance.NotifyCurrentResultChanged()
                Catch ex As Exception
                    Core.Instance.CurrentResult = New Result
                    Core.Instance.NotifyCurrentResultChanged()
                    Logger.Logger.Instance.Log(GetType(DataExchangeService).ToString + ".DataExchangeService.SendFinalResult(): ", Logger.Logger.eStatus.Exception, ex)
                End Try
            End Sub

            Public Sub MeasurementDone(ByVal sampleName As String) Implements DataExchangeInterface.Impg200Calls.MeasurementDone
                Core.Instance.MeasurementDone(sampleName)
            End Sub
            Public Sub CloseApp() Implements DataExchangeInterface.Impg200Calls.CloseApp
                Core.Instance.CloseApp()
            End Sub
            Public Sub HideApp() Implements DataExchangeInterface.Impg200Calls.HideApp
                Core.Instance.HideApp()
            End Sub
            Public Sub ShowApp() Implements DataExchangeInterface.Impg200Calls.ShowApp
                Core.Instance.ShowApp()
            End Sub

            'Public Sub MeasurementStart() Implements DataExchangeInterface.Impg200Calls.MeasurementStart

            'End Sub   /// Vergeer... this was commented out. It had no calls, and it did not compile.
        End Class
        Private Class DataExchangeClient
#Region "Singleton"
            Private Shared _instance As DataExchangeClient = Nothing
            Public Shared ReadOnly Property Instance As DataExchangeClient
                Get
                    If _instance Is Nothing Then _instance = New DataExchangeClient
                    Return _instance
                End Get
            End Property
            Private Sub New()

            End Sub
#End Region
            Public Sub AddSample(ByVal sample As DataExchangeInterface.Sample)
                Try
                    Dim obj As DataExchangeInterface.ISample = _
                      DirectCast(Activator.GetObject(GetType(DataExchangeInterface.ISample), _
                      "ipc://MPG-Expert_Intern_Channel/ExchangeObj"), DataExchangeInterface.ISample)
                    obj.AddSample(sample)
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(DataExchangeClient).ToString + ".AddSample(): ", Logger.Logger.eStatus.Exception, ex)
                End Try
            End Sub
            Public Sub RemoveSample(ByVal sampleName As String)
                Try
                    Dim obj As DataExchangeInterface.ISample = _
                      DirectCast(Activator.GetObject(GetType(DataExchangeInterface.ISample), _
                      "ipc://MPG-Expert_Intern_Channel/ExchangeObj"), DataExchangeInterface.ISample)
                    obj.RemoveSample(sampleName)
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(DataExchangeClient).ToString + ".RemoveSample(): ", Logger.Logger.eStatus.Exception, ex)
                End Try
            End Sub
            Public Function GetCoilsystems() As List(Of String)
                Try
                    Dim obj As DataExchangeInterface.ICoilsystem = _
                      DirectCast(Activator.GetObject(GetType(DataExchangeInterface.ICoilsystem), _
                      "ipc://MPG-Expert_Intern_Channel/ExchangeObj"), DataExchangeInterface.ICoilsystem)
                    Return obj.GetCoilsystems()
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(DataExchangeClient).ToString + ".GetCoilsystems(): ", Logger.Logger.eStatus.Exception, ex)
                    Return New List(Of String)
                End Try
            End Function
            Public Function StartMeasurement(ByVal sample As String, ByVal coilsystem As String, ByVal setPoints As List(Of DataExchangeInterface.SetPoint)) As Boolean
                Try
                    Dim obj As DataExchangeInterface.IOperating = _
                        DirectCast(Activator.GetObject(GetType(DataExchangeInterface.IOperating), _
                    "ipc://MPG-Expert_Intern_Channel/ExchangeObj"), DataExchangeInterface.IOperating)
                    Return obj.StartMeasurement(sample, coilsystem, setPoints)
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(DataExchangeClient).ToString + " .StartMeasurement(): ", Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try
            End Function
            Public Sub StopMeasurement()
                Try
                    Dim obj As DataExchangeInterface.IOperating = _
                        DirectCast(Activator.GetObject(GetType(DataExchangeInterface.IOperating), _
                    "ipc://MPG-Expert_Intern_Channel/ExchangeObj"), DataExchangeInterface.IOperating)
                    obj.StopMeasurement()
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(DataExchangeClient).ToString + ".StopMeasurement(): ", Logger.Logger.eStatus.Exception, ex)
                End Try
            End Sub
            Public Function MeasurementSeqAvailable(ByVal name As String) As Boolean
                Try
                    Dim list As List(Of String) = Nothing

                    Dim obj As DataExchangeInterface.IOperating = _
                        DirectCast(Activator.GetObject(GetType(DataExchangeInterface.IOperating), _
                    "ipc://MPG-Expert_Intern_Channel/ExchangeObj"), DataExchangeInterface.IOperating)
                    list = obj.GetMeasurementSequences()

                    If list.Contains(name) Then Return True

                    Return False
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(DataExchangeClient).ToString + ".MeasurementSeqAvailable(): ", Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try
            End Function



            Public Function GetCurrentUser() As String
                Try
                    Dim obj As DataExchangeInterface.IOperating = _
                        DirectCast(Activator.GetObject(GetType(DataExchangeInterface.IOperating), _
                    "ipc://MPG-Expert_Intern_Channel/ExchangeObj"), DataExchangeInterface.IOperating)
                    Return obj.GetCurrentUser
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(DataExchangeClient).ToString + ".GetCurrentUser(): ", Logger.Logger.eStatus.Exception, ex)
                    Return String.Empty
                End Try
            End Function
            Public Function GetCurrentUserRole() As String
                Try
                    Dim obj As DataExchangeInterface.IOperating = _
                        DirectCast(Activator.GetObject(GetType(DataExchangeInterface.IOperating), _
                    "ipc://MPG-Expert_Intern_Channel/ExchangeObj"), DataExchangeInterface.IOperating)
                    Return obj.GetCurrentUserRole
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(DataExchangeClient).ToString + ".GetCurrentUserRole(): ", Logger.Logger.eStatus.Exception, ex)
                    Return String.Empty
                End Try
            End Function
            Public Sub CloseMPGexpert()
                Try
                    Dim obj As DataExchangeInterface.IOperating = _
                  DirectCast(Activator.GetObject(GetType(DataExchangeInterface.IOperating), _
                  "ipc://MPG-Expert_Intern_Channel/ExchangeObj"), DataExchangeInterface.IOperating)
                    obj.CloseMPGapp()
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(DataExchangeClient).ToString + ".CloseMPGexpert(): ", Logger.Logger.eStatus.Exception, ex)
                End Try
            End Sub
        End Class
        Private Class DataExchangeServer
#Region "Singleton"
            Private Shared _instance As DataExchangeServer = Nothing
            Public Shared ReadOnly Property Instance As DataExchangeServer
                Get
                    If _instance Is Nothing Then _instance = New DataExchangeServer
                    Return _instance
                End Get
            End Property
            Private Sub New()
                InitService()
            End Sub
#End Region

            Private Sub InitService()
                For Each channel In ChannelServices.RegisteredChannels
                    If channel.ChannelName = "MPG-Expert_Extern_Channel" Then Exit Sub
                Next
                Dim ipcChannel As New IpcChannel("MPG-Expert_Extern_Channel")
                ChannelServices.RegisterChannel(ipcChannel, False)
                RemotingConfiguration.RegisterWellKnownServiceType(GetType(DataExchangeService), "ExchangeObj", WellKnownObjectMode.Singleton)
            End Sub
        End Class

        Public Sub New()
            _client = DataExchangeClient.Instance
            _server = DataExchangeServer.Instance
        End Sub

        Private _client As DataExchangeClient = Nothing
        Private _server As DataExchangeServer = Nothing

        Public Function MeasurementSeqAvailable(ByVal name As String) As Boolean
            Return _client.MeasurementSeqAvailable(name)
        End Function
        Public Function GetCoilSystems() As List(Of String)
            Return _client.GetCoilsystems
        End Function
        Public Function StartMeasurement(ByVal workOrder As WorkOrder, ByVal coilSystem As String, ByVal frequency As Double, ByVal fluxDensity As Double, ByVal demag As Boolean) As Boolean
            Dim sample As New DataExchangeInterface.Sample
            With sample
                .density = workOrder.Sample.density
                .grade = ""
                .name = workOrder.Sample.SerialNo
                .number = 1
                .innerDiameter = Math.Inch2Cm(workOrder.Sample.InnerDiameter) / 100
                .outerDiameter = Math.Inch2Cm(workOrder.Sample.OuterDiameter) / 100
                .thickness = Math.Inch2Cm(workOrder.Sample.MaterialWidth) / 100
                .stackingFactor = workOrder.Sample.StackingFactor
                .type = DataExchangeInterface.eSampleTypeT.Ring
                .weight = 0 'Math.Pound2KG(workOrder.Sample.weight) * 1000
            End With
            _client.AddSample(sample)


            Dim setPoints As New List(Of DataExchangeInterface.SetPoint)
            If demag Then
                Dim demagSetPoint As DataExchangeInterface.SetPoint
                With demagSetPoint
                    .f = 30
                    .type = DataExchangeInterface.eMeasurementTypeT.Demag
                    .value = 150
                End With
                setPoints.Add(demagSetPoint)
            End If

            Dim setPoint As DataExchangeInterface.SetPoint
            With setPoint
                .f = frequency
                .type = DataExchangeInterface.eMeasurementTypeT.JMax
                .value = fluxDensity
            End With
            setPoints.Add(setPoint)

            Return _client.StartMeasurement(sample.name, coilSystem, setPoints)
        End Function
        Public Sub StopMeasurement()
            _client.StopMeasurement()
        End Sub
        Public Sub RemoveSample(ByVal sampleName As String)
            _client.RemoveSample(sampleName)
        End Sub
        Public Function GetCurrentUser() As String
            Return _client.GetCurrentUser
        End Function
        Public Function GetCurrentUserRole() As String
            Return _client.GetCurrentUserRole
        End Function
        Public Sub CloseMPGexpert()
            _client.CloseMPGexpert()
        End Sub
    End Class
End Namespace



