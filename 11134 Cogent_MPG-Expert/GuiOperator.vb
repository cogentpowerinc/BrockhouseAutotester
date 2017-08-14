Imports DataElements
Imports PLC   'Added by VERGEER
Imports System.Windows.Threading 'Added by VERGEER
Imports ABCommunication 'Added by VERGEER

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

        InitComms()   'Added by VERGEER
        ControlModeIsAutomatic = True   'Added by VERGEER
        ChangeControlMode()     'Added by VERGEER


    End Sub
    Private Sub GuiOperator_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        CommManager.ShutDownABDriver() 'Added by VERGEER
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

                               UpdateResults()  ' added by VERGEER

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
                           tbNumberOfCoreTested.Text = _core.CurrentWorkOrder.QuantityDone.ToString + "/" + _
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
                If _core.CurrentResult.Grade = Result.GradeT.Rejected OrElse _
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
        tbCurrentHarness.Text = _core.CurrentSettings.HarnessDefaultN1.ToString + " : " + _
            _core.CurrentSettings.HarnessDefaultN2.ToString
    End Sub

    Private Sub StartMeasurement()
        If _core.CurrentWorkOrder Is Nothing Then Exit Sub

        If _core.CurrentWorkOrder.Sample.SampleType = Sample.SampleTypeT.Toroid Then
            Dim toroidWO As WorkOrderToroid = CType(_core.CurrentWorkOrder, WorkOrderToroid)

            If (toroidWO.OperatorNumberOfTurns <> toroidWO.NumberOfTurns) OrElse (_core.CurrentSettings.HarnessDefaultN1 <> _core.CurrentSettings.HarnessDefaultN2) Then
                Dim msg As String = String.Format(My.Resources.Controls.sCheckHarnessWorkOrder, _
                                                  toroidWO.NumberOfTurns.ToString + ":" + toroidWO.NumberOfTurns.ToString, _
                                                  _core.CurrentSettings.HarnessDefaultN1.ToString + ":" + _
                                                  _core.CurrentSettings.HarnessDefaultN2.ToString)

                If MsgBox(msg, MsgBoxStyle.OkCancel, My.Resources.Controls.sCheckHarnessTitle) = MsgBoxResult.Cancel Then Exit Sub
            End If
        Else
            If _core.CurrentSettings.HarnessDefaultN1 = _core.CurrentSettings.HarnessDefaultN2 Then
                Dim msg As String = String.Format(My.Resources.Controls.sCheckHarnessSettingsDgCore, _
                                                  _core.CurrentSettings.HarnessDefaultN1.ToString + ":" + _
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


    Private Shared JobCheck_Tmr As New DispatcherTimer()
    Private Sub InitComms()

        myPLCTools.Initialize("C:\\CogentRobot\\", "ABComms.xml", "ABInputs.xml", "ABOutputs.xml")

        JobCheck_Tmr.Interval = TimeSpan.FromMilliseconds(30)
        AddHandler JobCheck_Tmr.Tick, AddressOf JobCheckTmr_Tick

        JobCheck_Tmr.Start()


        AddHandler myPLCTools.HeartBeat.Status, AddressOf HeartbeatChanged

    End Sub


    Private Sub HeartbeatChanged(sender As Object, e As EventArgs)
        rb_HeartBeat.Checked = myPLCTools.HeartBeat.State

    End Sub



    Private Sub UpdateResults()
        MyPLC.ToPLC.ActualCoreLoss = 101.101
        MyPLC.ToPLC.ActualAmpTurns = _core.CurrentResult.Watts
        MyPLC.ToPLC.TestInProcess = False
        MyPLC.ToPLC.TestComplete = True
    End Sub

    Private Sub JobCheckTmr_Tick(sender As Object, e As EventArgs)
        Dim Success As Boolean = False

        If (MyPLC.Comm.IsOnline) Then
            MyPLC.ToPLC.InAuto = MyPLC.FromPLC.AutoMode
            If MyPLC.ToPLC.InAuto And MyPLC.FromPLC.Reset = False Then
                MyPLC.ToPLC.ReadyForData = True

                'set up display
                tbWorkOrder.Text = MyPLC.FromPLC.WorkOrder
                tbSerialNumber.Text = MyPLC.FromPLC.SerialNumber
                tbWeight.Text = MyPLC.FromPLC.Weight.ToString

                RefreshWorkOrder()
                lblCurrentResult_update()

                If MyPLC.FromPLC.InitTest Then
                    If MyPLC.FromPLC.Weight > 0.01 And MyPLC.FromPLC.Temp > 0.01 And MyPLC.FromPLC.SerialNumber <> "" Then
                        '  '   GET SERVER DATA
                        '   WAIT SERVER DATA  THEN SET READY TO TEST
                        MyPLC.ToPLC.ReadyToTest = True
                        If MyPLC.FromPLC.InitTest Then
                            If MyPLC.ToPLC.TestInProcess = False Then
                                StartMeasurement()   '  <-------------------------------------------------------------------   Actual INIT  -- MUST BE ENABLED

                            Else
                                If MyPLC.ToPLC.TestComplete Or MyPLC.ToPLC.TestFailedToComplete Then
                                    '  TEST COMPLETE... 
                                    If MyPLC.FromPLC.ResultResponse = 0 Then    ' To be added.... mod for server results....
                                        MyPLC.ToPLC.ServerResult = 0
                                    ElseIf MyPLC.FromPLC.ResultResponse = 1 Then
                                        btnEvaluation(Result.StateT.Accept)
                                        Me.AcceptButton = Nothing
                                        MyPLC.ToPLC.ServerResult = 1
                                    ElseIf MyPLC.FromPLC.ResultResponse = 2 Then
                                        btnEvaluation(Result.StateT.Abandon)
                                        MyPLC.ToPLC.ServerResult = 2
                                    ElseIf MyPLC.FromPLC.ResultResponse = 3 Then
                                        btnEvaluation(Result.StateT.Overwrite)
                                        MyPLC.ToPLC.ServerResult = 3
                                    ElseIf MyPLC.FromPLC.ResultResponse = 4 Then
                                        btnEvaluation(Result.StateT.Reprocessed)
                                        MyPLC.ToPLC.ServerResult = 4
                                    ElseIf MyPLC.FromPLC.ResultResponse = 5 Then
                                        btnEvaluation(Result.StateT.Remade)
                                        MyPLC.ToPLC.ServerResult = 5
                                    Else
                                        MyPLC.ToPLC.Faulted = True
                                        MyPLC.ToPLC.ErrorMsg = "Result Response Not Supported"
                                    End If
                                End If

                            End If
                        End If
                    Else
                        MyPLC.ToPLC.Faulted = True
                        MyPLC.ToPLC.ErrorMsg = "No Weight and/or Temp And/or Serial Number At Init"

                    End If

                End If
            Else
                MyPLC.ToPLC.ReadyForData = False
                MyPLC.ToPLC.ReadyToTest = False

                MyPLC.ToPLC.FetchingFromServer = False
                MyPLC.ToPLC.TestInProcess = False
                MyPLC.ToPLC.TestComplete = False
                MyPLC.ToPLC.TestFailedToComplete = False
                If MyPLC.FromPLC.Reset Then
                    MyPLC.ToPLC.ErrorMsg = "RESETING"
                    tbWorkOrder.Text = ""
                    tbSerialNumber.Text = ""
                    tbWeight.Text = ""

                    RefreshWorkOrder()
                    lblCurrentResult_update()
                Else
                    MyPLC.ToPLC.ErrorMsg = "Not In Auto"
                End If
                MyPLC.ToPLC.Faulted = False
                MyPLC.ToPLC.ActualCoreLoss = -999.99
                MyPLC.ToPLC.ActualAmpTurns = -999.99
                MyPLC.ToPLC.ServerResult = -99

            End If



        End If


    End Sub



    Private Shared ControlModeIsAutomatic As New Boolean()
    Private Sub btn_AutoManMode_Click(sender As Object, e As EventArgs) Handles Btn_AutoManualMode.Click
        ControlModeIsAutomatic = Not (ControlModeIsAutomatic)
        ChangeControlMode()
    End Sub

    Private Sub ChangeControlMode()
        If ControlModeIsAutomatic Then
            Btn_AutoManualMode.BackColor = Color.LightGreen
            Btn_AutoManualMode.Text = "AUTOMODE MODE"
            tbWorkOrder.Enabled = False
            tbSerialNumber.Enabled = False
            tbWeight.Enabled = False
            btnAccept.Enabled = False
            btnAbandon.Enabled = False
            btnOverwrite.Enabled = False
            btnReprocessed.Enabled = False
            btnRemade.Enabled = False
            btnStart.Enabled = False
            btnStop.Enabled = False
            myPLCTools.CommEnabled = True
            myPLCTools.Initialize("C:\\CogentRobot\\", "ABComms.xml", "ABInputs.xml", "ABOutputs.xml")
            _core.StopMeasurement()
        Else
            Btn_AutoManualMode.BackColor = Color.LightBlue
            Btn_AutoManualMode.Text = "MANUAL MODE"
            tbWorkOrder.Enabled = True
            tbSerialNumber.Enabled = True
            tbWeight.Enabled = True
            btnAccept.Enabled = True
            btnAbandon.Enabled = True
            btnOverwrite.Enabled = True
            btnReprocessed.Enabled = True
            btnRemade.Enabled = True
            btnStart.Enabled = True
            btnStop.Enabled = True
            myPLCTools.CommEnabled = False
            myPLCTools.CommShutDown()
            _core.StopMeasurement()
        End If


    End Sub

    Private Sub lblStatus_Click(sender As Object, e As EventArgs) Handles lblStatus.Click

    End Sub


#End Region




End Class
