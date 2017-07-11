
Imports DataElements
Imports PLC   'Added by VERGEER
Imports PLC.CommManager  'Added by VERGEER
Imports System.Windows.Threading 'Added by VERGEER


Public Class GuiOperator
    Private _BrockhausBlue As Color = System.Drawing.Color.FromArgb(0, 56, 103)
    Private _core As Core = Nothing

    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        _core = Core.Instance
        _core.Gui = Me
    End Sub

    Private Sub GuiOperator_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        dgvResults.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(220, 220, 255)

        lblStatus.Text = ""
        lblCurrentResult_clear()
        tbOperator.Text = _core.GetCurrentUser
        If _core.GetCurrentUserRole <> "Administrator" Then tsbPrintSetup.Visible = False

        SetEvaluationButtonAvailability(True)

        lblCurrentHarness_Refresh()

        AddHandler _core.CurrentWorkOrderChanged, AddressOf _core_CurrentWorkOrderChanged
        AddHandler _core.CurrentResultChanged, AddressOf _core_CurrentResultChanged
        AddHandler _core.CurrentResultsChanged, AddressOf _core_CurrentResultsChanged

        Me.AcceptButton = Nothing
        Me.CancelButton = btnAbandon

        'AddHandler dgvResults.SelectionChanged, AddressOf dgvResults_SelectionChanged






    End Sub
    Private Sub GuiOperator_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        EndComms() 'Added by VERGEER
        _core.CloseMPGexpert()

    End Sub

    Private Sub tbWorkOrder_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tbWorkOrder.KeyDown
        If Me.AcceptButton Is Nothing AndAlso e.KeyCode = Keys.Enter Then RefreshWorkOrder()
    End Sub
    Private Sub tbWorkOrder_TextChanged(sender As Object, e As EventArgs) Handles tbWorkOrder.TextChanged
        tbSerialNumber.Clear()
        Me.AcceptButton = Nothing
    End Sub

    Private Sub tbSerialNumber_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tbSerialNumber.KeyDown
        If e.KeyCode <> Keys.Enter Then Exit Sub

        dgvResults.ClearSelection()
        lblCurrentResult_clear()

        For i As Integer = 0 To _core.CurrentResults.Count - 1
            If Not _core.CurrentResults(i).Serial = tbSerialNumber.Text Then Continue For

            If dgvResults.Rows.Count - 1 < i Then Exit Sub
            dgvResults.Rows(i).Selected = True
            dgvResults.FirstDisplayedScrollingRowIndex() = i

            'RemoveHandler dgvResults.SelectionChanged, AddressOf dgvResults_SelectionChanged
            _core.CurrentResult = _core.CurrentResults(i)
            lblCurrentResult_update()
            '_core.NotifyCurrentResultChanged() add for testing
            'AddHandler dgvResults.SelectionChanged, AddressOf dgvResults_SelectionChanged
        Next
    End Sub
    Private Sub tbSerialNumber_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSerialNumber.TextChanged
        tbWeight.Clear()
    End Sub

    Private Sub tbWeight_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tbWeight.KeyDown
        If e.KeyCode <> Keys.Enter Then Exit Sub

        StartMeasurement()
    End Sub

    Private Sub dgvResults_CellFormatting(sender As System.Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvResults.CellFormatting
        If Not TypeOf e.Value Is Result.GradeT Then Exit Sub

        Select Case CType(e.Value, Result.GradeT)
            Case Result.GradeT.Passed : e.CellStyle.BackColor = _core.CurrentSettings.ColorPassed
            Case Result.GradeT.Green : e.CellStyle.BackColor = _core.CurrentSettings.ColorGreen
            Case Result.GradeT.Red, Result.GradeT.IOR : e.CellStyle.BackColor = _core.CurrentSettings.ColorRed
            Case Result.GradeT.Rejected : e.CellStyle.BackColor = _core.CurrentSettings.ColorRejected
            Case Result.GradeT.Yellow : e.CellStyle.BackColor = _core.CurrentSettings.ColorYellow
        End Select
    End Sub
    Private Sub dgvResults_SelectionChanged(sender As Object, e As EventArgs)
        If dgvResults.SelectedRows.Count = 0 Then Exit Sub

        _core.CurrentResult = _core.CurrentResults(dgvResults.SelectedRows(0).Index)
        '_core.NotifyCurrentResultChanged()
        lblCurrentResult_update()
        tbSerialNumber.Text = _core.CurrentResult.Serial
    End Sub

    Private Sub tsbRefresh_Click(sender As Object, e As EventArgs) Handles tsbRefresh.Click
        RefreshWorkOrder()
    End Sub
    Private Sub tsbPrint_Click(sender As Object, e As EventArgs) Handles tsbPrint.Click
        If dgvResults.SelectedRows.Count = 0 Then Exit Sub
        _core.Print(dgvResults.SelectedRows(0).Index)
    End Sub
    Private Sub tsbPrintSetup_Click(sender As Object, e As EventArgs) Handles tsbPrintSetup.Click
        _core.PrintSetup()
    End Sub
    Private Sub tsbSettings_Click(sender As Object, e As EventArgs) Handles tsbSettings.Click
        _core.LoadSettings()
        Dim newForm As New Settings.GuiSettings(_core.CurrentSettings)
        If newForm.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            _core.CurrentSettings = newForm.Settings
            _core.SaveSettings()

            _core_CurrentWorkOrderChanged()
        End If

        lblCurrentHarness_Refresh()
    End Sub

    Private Sub btnStart_Click(sender As System.Object, e As System.EventArgs) Handles btnStart.Click
        StartMeasurement()
    End Sub
    Private Sub btnStop_Click(sender As System.Object, e As System.EventArgs) Handles btnStop.Click
        SetEvaluationButtonAvailability(True)
        btnStart.Enabled = True

        _core.StopMeasurement()

        lblStatus.Text = My.Resources.Controls.sStatusMeasurementInterrupted
        lblStatus.ForeColor = Color.Blue
    End Sub

#Region "_core methods"
    Private Sub _core_CurrentResultChanged()

        Try
            Me.BeginInvoke(Sub()
                               Me.Activate()

                               Dim dgCoreWo As WorkOrderDgCore = TryCast(_core.CurrentWorkOrder, WorkOrderDgCore)
                               If _core.CurrentResult.Watts < dgCoreWo.LossLimit_MaxGreen * (Core.Instance.CurrentSettings.LowLossWarningPercentageOfGreenLimit / 100) AndAlso _core.CurrentResult.Watts > dgCoreWo.LossLimit_MaxGreen * 0.01 Then ' e.g. wrong work order
                                   Dim antwort As Integer = 0
                                   antwort = MsgBox("Extreme low Core Loss", MsgBoxStyle.OkOnly, "Warning")
                                   SetEvaluationButtonAvailability(False)
                               ElseIf _core.CurrentResult.Watts <= dgCoreWo.LossLimit_MaxGreen * 0.01 Then ' e.g. Harness not closed
                                   Dim antwort As Integer = 0
                                   antwort = MsgBox("Zero Core Loss", MsgBoxStyle.OkOnly, "Warning")
                                   SetEvaluationButtonAvailability(True, True)
                               Else
                                   SetEvaluationButtonAvailability(False)
                               End If
                               lblCurrentResult_update()

                               btnStart.Enabled = True

                               If _core.CurrentResult.Grade = Result.GradeT.IOR Then
                                   lblStatus.Text = My.Resources.Controls.sStatusFluxDensityOutOfRange
                                   lblStatus.ForeColor = Color.Red
                               Else
                                   lblStatus.Text = My.Resources.Controls.sStatusMeasurementFinished
                                   lblStatus.ForeColor = Color.Blue
                               End If

                               gbCurrentResult.Text = My.Resources.Controls.sResult + _core.CurrentWorkOrder.WorkOrderNo + "-" + _core.CurrentResult.Serial

                           End Sub)
        Catch ex As Exception
            Logger.Logger.Instance.Log(GetType(GuiOperator).ToString + "._core_CurrentResultChanged(): ", Logger.Logger.eStatus.Exception, ex)
        End Try
    End Sub
    Private Sub _core_CurrentWorkOrderChanged()
        Me.BeginInvoke(Sub()
                           If _core.CurrentWorkOrder Is Nothing Then Exit Sub
                           _core.UpdateOperatorHarnessInToroidWorkOrder()

                           pgOrderInfo.SelectedObject = _core.CurrentWorkOrder
                           tbNumberOfCoreTested.Text = _core.CurrentWorkOrder.QuantityDone.ToString + "/" +
                               _core.CurrentWorkOrder.OrderQuantity.ToString
                       End Sub)
    End Sub
    Private Sub _core_CurrentResultsChanged()
        Me.BeginInvoke(Sub()
                           RemoveHandler dgvResults.SelectionChanged, AddressOf dgvResults_SelectionChanged

                           If _core.CurrentWorkOrder Is Nothing Then Exit Sub

                           dgvResults.DataSource = _core.CurrentResults

                           If _core.CurrentWorkOrder.Sample.SampleType = Sample.SampleTypeT.DgCore Then
                               dgvResults.Columns("Amps").Visible = False
                               dgvResults.Columns("Volts").Visible = False
                               dgvResults.Columns("NumberOfTurns").Visible = False
                               dgvResults.Columns("Watts").Visible = True
                               dgvResults.Columns("AmpTurns").Visible = True
                           Else
                               dgvResults.Columns("Volts").Visible = True
                               dgvResults.Columns("Amps").Visible = True
                               dgvResults.Columns("NumberOfTurns").Visible = True
                               dgvResults.Columns("Watts").Visible = False
                               dgvResults.Columns("AmpTurns").Visible = False
                           End If

                           For i As Integer = 0 To _core.CurrentResults.Count - 1
                               If Not _core.CurrentResults(i).Serial = tbSerialNumber.Text Then Continue For

                               If dgvResults.Rows.Count - 1 < i Then Exit Sub
                               dgvResults.Rows(i).Selected = True
                               dgvResults.FirstDisplayedScrollingRowIndex() = i
                           Next
                           AddHandler dgvResults.SelectionChanged, AddressOf dgvResults_SelectionChanged
                       End Sub)
    End Sub
#End Region

#Region "Evaluation buttons"
    Private Sub btnAccept_Click(sender As System.Object, e As System.EventArgs) Handles btnAccept.Click
        btnEvaluation(Result.StateT.Accept)
        Me.AcceptButton = Nothing
    End Sub
    Private Sub btnAbandon_Click(sender As System.Object, e As System.EventArgs) Handles btnAbandon.Click
        btnEvaluation(Result.StateT.Abandon)
    End Sub
    Private Sub btnOverwrite_Click(sender As System.Object, e As System.EventArgs) Handles btnOverwrite.Click
        btnEvaluation(Result.StateT.Overwrite)
    End Sub
    Private Sub btnReprocessed_Click(sender As System.Object, e As System.EventArgs) Handles btnReprocessed.Click
        btnEvaluation(Result.StateT.Reprocessed)
    End Sub
    Private Sub btnRemade_Click(sender As System.Object, e As System.EventArgs) Handles btnRemade.Click
        btnEvaluation(Result.StateT.Remade)
    End Sub

    Private Sub btnEvaluation(ByVal state As Result.StateT)
        Select Case state
            Case Result.StateT.Remade, Result.StateT.Reprocessed, Result.StateT.Overwrite, Result.StateT.Accept
                If _core.CurrentResult.Grade = Result.GradeT.Rejected OrElse
                    _core.CurrentResult.Grade = Result.GradeT.IOR Then
                    If MsgBox(My.Resources.Controls.sAcceptResultsWithError + vbNewLine + "Grade: " + _core.CurrentResult.Grade.ToString, MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
                End If

                If Not _core.CurrentResultSave(state) Then
                    'Fehlermeldung
                    Exit Sub
                Else
                    _core.Print()

                    RefreshContent_WorkOrderAndResults()
                End If
            Case Result.StateT.Abandon
                lblCurrentResult_clear()
        End Select
        SetEvaluationButtonAvailability(True)
        btnStart.Enabled = True
        tsbRefresh.Enabled = True
        tbSerialNumber.Select()
        tbSerialNumber.SelectAll()
    End Sub

    Private Sub SetEvaluationButtonAvailability(Optional disableAll As Boolean = False, Optional IsError As Boolean = False)
        If disableAll Then
            btnAbandon.Enabled = IsError
            btnAccept.Enabled = False
            btnOverwrite.Enabled = False
            btnReprocessed.Enabled = False
            btnRemade.Enabled = False
            Exit Sub
        End If

        For Each element In _core.CurrentResults
            If element.Serial = _core.CurrentResult.Serial Then
                btnAccept.Enabled = False
                btnOverwrite.Enabled = True
                btnReprocessed.Enabled = True
                btnRemade.Enabled = True
                btnAbandon.Enabled = True
                Exit Sub
            End If
        Next
        btnAccept.Enabled = Not IsError
        btnAbandon.Enabled = True
        Me.AcceptButton = btnAccept
    End Sub
#End Region

    Private Sub lblCurrentResult_clear()
        lblPercentageActualJValue.Text = ""
        lblPercentageActualJValue.BackColor = System.Drawing.SystemColors.Control
        lblWattsOrVTrmsValue.Text = ""
        lblATrmsOrAmpsValue.Text = ""
        lblGradeValue.Text = ""
        lblGradeValue.BackColor = System.Drawing.SystemColors.Control
        lblPercentageGreenLossLevelValue.Text = ""
        gbCurrentResult.Text = My.Resources.Controls.sResult
    End Sub
    Private Sub lblCurrentResult_update()
        If _core.CurrentWorkOrder Is Nothing Then Exit Sub
        lblPercentageActualJValue.Text = _core.CurrentResult.PercentageActualJ.ToString("F3")
        lblGradeValue.Text = _core.CurrentResult.Grade.ToString
        lblPercentageGreenLossLevelValue.Text = _core.CurrentResult.PercentageGreenLossLevel.ToString("F3")
        Select Case _core.CurrentResult.Grade
            Case Result.GradeT.Passed : lblGradeValue.BackColor = _core.CurrentSettings.ColorPassed
            Case Result.GradeT.Green : lblGradeValue.BackColor = _core.CurrentSettings.ColorGreen
            Case Result.GradeT.Red : lblGradeValue.BackColor = _core.CurrentSettings.ColorRed
            Case Result.GradeT.Rejected : lblGradeValue.BackColor = _core.CurrentSettings.ColorRejected
            Case Result.GradeT.Yellow : lblGradeValue.BackColor = _core.CurrentSettings.ColorYellow
            Case Result.GradeT.IOR
                lblGradeValue.BackColor = _core.CurrentSettings.ColorRed
                lblPercentageActualJValue.BackColor = _core.CurrentSettings.ColorRed
            Case Else : lblGradeValue.BackColor = System.Drawing.SystemColors.Control
        End Select

        If _core.CurrentWorkOrder.Sample.SampleType = Sample.SampleTypeT.DgCore Then
            lblATrmsOrAmpsValue.Text = _core.CurrentResult.AmpTurns.ToString("F3")
            lblATrmsOrAmps.Text = My.Resources.Controls.sATrms

            lblWattsOrVTrmsValue.Text = _core.CurrentResult.Watts.ToString("F3")
            lblWattsOrVTrms.Text = My.Resources.Controls.sWatts
        Else
            lblATrmsOrAmpsValue.Text = _core.CurrentResult.Amps.ToString("F3")
            lblATrmsOrAmps.Text = My.Resources.Controls.sAmps

            lblWattsOrVTrmsValue.Text = _core.CurrentResult.Volts.ToString("F3")
            lblWattsOrVTrms.Text = My.Resources.Controls.sVolts
        End If
    End Sub
    Private Sub lblCurrentHarness_Refresh()
        lblCurrentHarness.Text = My.Resources.Controls.sHarness
        tbCurrentHarness.Text = _core.CurrentSettings.HarnessDefaultN1.ToString + " : " +
            _core.CurrentSettings.HarnessDefaultN2.ToString
    End Sub

    Private Sub StartMeasurement()
        If _core.CurrentWorkOrder Is Nothing Then Exit Sub

        If _core.CurrentWorkOrder.Sample.SampleType = Sample.SampleTypeT.Toroid Then
            Dim toroidWO As WorkOrderToroid = CType(_core.CurrentWorkOrder, WorkOrderToroid)

            If (toroidWO.OperatorNumberOfTurns <> toroidWO.NumberOfTurns) OrElse (_core.CurrentSettings.HarnessDefaultN1 <> _core.CurrentSettings.HarnessDefaultN2) Then
                Dim msg As String = String.Format(My.Resources.Controls.sCheckHarnessWorkOrder,
                                                  toroidWO.NumberOfTurns.ToString + ":" + toroidWO.NumberOfTurns.ToString,
                                                  _core.CurrentSettings.HarnessDefaultN1.ToString + ":" +
                                                  _core.CurrentSettings.HarnessDefaultN2.ToString)

                If MsgBox(msg, MsgBoxStyle.OkCancel, My.Resources.Controls.sCheckHarnessTitle) = MsgBoxResult.Cancel Then Exit Sub
            End If
        Else
            If _core.CurrentSettings.HarnessDefaultN1 = _core.CurrentSettings.HarnessDefaultN2 Then
                Dim msg As String = String.Format(My.Resources.Controls.sCheckHarnessSettingsDgCore,
                                                  _core.CurrentSettings.HarnessDefaultN1.ToString + ":" +
                                                  _core.CurrentSettings.HarnessDefaultN2.ToString)

                If MsgBox(msg, MsgBoxStyle.OkCancel, My.Resources.Controls.sCheckHarnessTitle) = MsgBoxResult.Cancel Then Exit Sub
            End If
        End If


        'Check serial number
        Dim serialNumber As String = tbSerialNumber.Text

        'Check work order

        'Check weight
        If Not IsNumeric(tbWeight.Text) Then Exit Sub
        Dim weight As Double = 0
        If Not Double.TryParse(tbWeight.Text, weight) Then Exit Sub

        If Not _core.CheckWeight(weight) Then
            lblStatus.Text = My.Resources.Controls.sStatusPleaseCheckWeight
            lblStatus.ForeColor = Color.Red
            Exit Sub
        End If

        btnStart.Enabled = False
        tsbRefresh.Enabled = False
        SetEvaluationButtonAvailability(True)
        lblCurrentResult_clear()

        If _core.StartMeasurement(serialNumber, weight) Then
            lblStatus.Text = My.Resources.Controls.sStatusMeasurementInProgress
            lblStatus.ForeColor = Color.Red
        Else
            lblStatus.Text = My.Resources.Controls.sStatusMeasurementNotStarted
            lblStatus.ForeColor = Color.Blue
        End If
    End Sub
    Private Sub RefreshWorkOrder()
        tbNumberOfCoreTested.Clear()

        RefreshContent_WorkOrderAndResults()

        lblCurrentResult_clear()
        SetEvaluationButtonAvailability(True)
    End Sub
    Private Sub RefreshContent_WorkOrderAndResults()
        If Not IsNumeric(tbWorkOrder.Text) Then Exit Sub

        Try
            RemoveHandler dgvResults.SelectionChanged, AddressOf dgvResults_SelectionChanged
            _core.CurrentWorkOrder_Refresh(tbWorkOrder.Text)
            AddHandler dgvResults.SelectionChanged, AddressOf dgvResults_SelectionChanged
        Catch ex As Exception
            Logger.Logger.Instance.Log(GetType(GuiOperator).ToString + ".RefreshContent_WorkOrderAndResults(): ", Logger.Logger.eStatus.Exception, ex)
        End Try
    End Sub

#Region "AB Communication"  ' Entire Section added by VERGEER
    Private Shared FullUpdateCheckTmr As New DispatcherTimer()
    Private Sub InitComms()
        Channels.AddChannel(New Channel(PLCDriverType.AllenBradley_CLX, "AT_1", "192.168.1.244", "0", 3000, 4))
        FullUpdateCheckTmr.Interval = TimeSpan.FromMilliseconds(100)
        AddHandler FullUpdateCheckTmr.Tick, AddressOf FullUpdateCheckTmr_Tick


        FullUpdateCheckTmr.Start()


        DummyTimer.Interval = TimeSpan.FromMilliseconds(100)
        AddHandler DummyTimer.Tick, AddressOf DummyTimer_Tick



    End Sub

    Private Shared DummyTimer As New DispatcherTimer()  'To be deleted
    Private Sub DummyTimer_Tick(sender As Object, e As EventArgs)    'To be deleted 

        If ABCommunication.ToPLC.TestInProgress Then
            ABCommunication.ToPLC.TestComplete = True
            DummyTimer.Stop()
        Else
            ABCommunication.ToPLC.TestInProgress = True
        End If


    End Sub
    Private Sub FullUpdateCheckTmr_Tick(sender As Object, e As EventArgs)
        Dim Success As Boolean = False
        Dim Index As Int32 = 0


        Success = ABCommunication.AutoTesterInterface.PeekPLC()
        If Success Then
            ABCommunication.FromPLC.AutoMode = ABCommunication.ToPLC.InAuto

            If ABCommunication.ToPLC.InAuto And ABCommunication.FromPLC.Reset = False Then
                ABCommunication.ToPLC.ReadyForData = True

                If ABCommunication.FromPLC.InitTest Then
                    If ABCommunication.FromPLC.Weight > 0.01 And ABCommunication.FromPLC.Temp < 0.01 And ABCommunication.FromPLC.SerialNumber = "" Then
                        '  GET SERVER DATA
                        'WAIT SERVER DATA  THEN SET READY TO TEST
                        ABCommunication.ToPLC.ReadyToTest = True
                        If ABCommunication.FromPLC.InitTest Then
                            'START TEST
                            ABCommunication.ToPLC.TestInProgress = True
                            DummyTimer.Start()
                        End If
                    Else
                        ABCommunication.ToPLC.Faulted = False
                        ABCommunication.ToPLC.ErrorMsg = "No Weight and/or Temp And/or Serial Number At Init"

                    End If


                Else
                    ABCommunication.ToPLC.ReadyForData = False
                    ABCommunication.ToPLC.ReadyToTest = False

                    ABCommunication.ToPLC.FetchingFromServer = False
                    ABCommunication.ToPLC.TestInProgress = False
                    ABCommunication.ToPLC.TestComplete = False
                    ABCommunication.ToPLC.TestFailedToComplete = False
                    If ABCommunication.FromPLC.Reset Then
                        ABCommunication.ToPLC.ErrorMsg = "RESETING"
                    Else
                        ABCommunication.ToPLC.ErrorMsg = "Not In Auto"
                    End If
                    ABCommunication.ToPLC.Faulted = False
                    ABCommunication.ToPLC.ActualCoreLoss = -999.99
                    ABCommunication.ToPLC.ActualAmpTurns = -999.99
                    ABCommunication.ToPLC.ServerResult = -99
                    DummyTimer.stop()
                End If

            End If


            Success = ABCommunication.AutoTesterInterface.PokePLC()
        End If


    End Sub

    Private Sub EndComms()
        CommManager.ShutDownABDriver()
    End Sub




#End Region


End Class
