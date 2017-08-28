<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GuiOperator
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GuiOperator))
        Me.tbWorkOrder = New System.Windows.Forms.TextBox()
        Me.lblWorkOrderNo = New System.Windows.Forms.Label()
        Me.tbOperator = New System.Windows.Forms.TextBox()
        Me.lblOperator = New System.Windows.Forms.Label()
        Me.gbWorkOrderInfo = New System.Windows.Forms.GroupBox()
        Me.pgOrderInfo = New System.Windows.Forms.PropertyGrid()
        Me.gbResults = New System.Windows.Forms.GroupBox()
        Me.dgvResults = New System.Windows.Forms.DataGridView()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.gbPrepare = New System.Windows.Forms.GroupBox()
        Me.btnBypassTest = New System.Windows.Forms.Button()
        Me.rb_HeartBeat = New System.Windows.Forms.RadioButton()
        Me.Btn_AutoManualMode = New System.Windows.Forms.Button()
        Me.tbNumberOfCoreTested = New System.Windows.Forms.TextBox()
        Me.lblNumberOfCores = New System.Windows.Forms.Label()
        Me.tbCurrentHarness = New System.Windows.Forms.TextBox()
        Me.lblCurrentHarness = New System.Windows.Forms.Label()
        Me.gbCurrentResult = New System.Windows.Forms.GroupBox()
        Me.lblPercentageActualJValue = New System.Windows.Forms.Label()
        Me.lblPercentageGreenLossLevelValue = New System.Windows.Forms.Label()
        Me.lblPercentageActualJ = New System.Windows.Forms.Label()
        Me.lblGradeValue = New System.Windows.Forms.Label()
        Me.lblWattsOrVTrms = New System.Windows.Forms.Label()
        Me.lblATrmsOrAmpsValue = New System.Windows.Forms.Label()
        Me.lblATrmsOrAmps = New System.Windows.Forms.Label()
        Me.lblWattsOrVTrmsValue = New System.Windows.Forms.Label()
        Me.lblGrade = New System.Windows.Forms.Label()
        Me.lblPercentageGreenLossLevel = New System.Windows.Forms.Label()
        Me.btnRemade = New System.Windows.Forms.Button()
        Me.btnReprocessed = New System.Windows.Forms.Button()
        Me.btnOverwrite = New System.Windows.Forms.Button()
        Me.btnAbandon = New System.Windows.Forms.Button()
        Me.btnAccept = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.tbWeight = New System.Windows.Forms.TextBox()
        Me.tbSerialNumber = New System.Windows.Forms.TextBox()
        Me.lblWeight = New System.Windows.Forms.Label()
        Me.lblSerialNumber = New System.Windows.Forms.Label()
        Me.gbStatus = New System.Windows.Forms.GroupBox()
        Me.lblCustomerMessage = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.tsMain = New System.Windows.Forms.ToolStrip()
        Me.tsbRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tsbPrint = New System.Windows.Forms.ToolStripButton()
        Me.tsbPrintSetup = New System.Windows.Forms.ToolStripButton()
        Me.tsbSettings = New System.Windows.Forms.ToolStripButton()
        Me.pbLogo = New System.Windows.Forms.PictureBox()
        Me.gbWorkOrderInfo.SuspendLayout()
        Me.gbResults.SuspendLayout()
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbPrepare.SuspendLayout()
        Me.gbCurrentResult.SuspendLayout()
        Me.gbStatus.SuspendLayout()
        Me.tsMain.SuspendLayout()
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbWorkOrder
        '
        Me.tbWorkOrder.Location = New System.Drawing.Point(109, 48)
        Me.tbWorkOrder.Margin = New System.Windows.Forms.Padding(4)
        Me.tbWorkOrder.Name = "tbWorkOrder"
        Me.tbWorkOrder.Size = New System.Drawing.Size(132, 22)
        Me.tbWorkOrder.TabIndex = 3
        '
        'lblWorkOrderNo
        '
        Me.lblWorkOrderNo.AutoSize = True
        Me.lblWorkOrderNo.Location = New System.Drawing.Point(8, 50)
        Me.lblWorkOrderNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblWorkOrderNo.Name = "lblWorkOrderNo"
        Me.lblWorkOrderNo.Size = New System.Drawing.Size(91, 17)
        Me.lblWorkOrderNo.TabIndex = 1
        Me.lblWorkOrderNo.Text = "Work order #"
        '
        'tbOperator
        '
        Me.tbOperator.Location = New System.Drawing.Point(109, 16)
        Me.tbOperator.Margin = New System.Windows.Forms.Padding(4)
        Me.tbOperator.Name = "tbOperator"
        Me.tbOperator.ReadOnly = True
        Me.tbOperator.Size = New System.Drawing.Size(132, 22)
        Me.tbOperator.TabIndex = 2
        '
        'lblOperator
        '
        Me.lblOperator.AutoSize = True
        Me.lblOperator.Location = New System.Drawing.Point(8, 20)
        Me.lblOperator.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblOperator.Name = "lblOperator"
        Me.lblOperator.Size = New System.Drawing.Size(65, 17)
        Me.lblOperator.TabIndex = 0
        Me.lblOperator.Text = "Operator"
        '
        'gbWorkOrderInfo
        '
        Me.gbWorkOrderInfo.BackColor = System.Drawing.SystemColors.Control
        Me.gbWorkOrderInfo.Controls.Add(Me.pgOrderInfo)
        Me.gbWorkOrderInfo.Location = New System.Drawing.Point(709, 31)
        Me.gbWorkOrderInfo.Margin = New System.Windows.Forms.Padding(4)
        Me.gbWorkOrderInfo.Name = "gbWorkOrderInfo"
        Me.gbWorkOrderInfo.Padding = New System.Windows.Forms.Padding(4)
        Me.gbWorkOrderInfo.Size = New System.Drawing.Size(423, 290)
        Me.gbWorkOrderInfo.TabIndex = 2
        Me.gbWorkOrderInfo.TabStop = False
        Me.gbWorkOrderInfo.Text = "Work Order"
        '
        'pgOrderInfo
        '
        Me.pgOrderInfo.BackColor = System.Drawing.SystemColors.Control
        Me.pgOrderInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgOrderInfo.HelpVisible = False
        Me.pgOrderInfo.LineColor = System.Drawing.SystemColors.ControlDark
        Me.pgOrderInfo.Location = New System.Drawing.Point(4, 19)
        Me.pgOrderInfo.Margin = New System.Windows.Forms.Padding(4)
        Me.pgOrderInfo.Name = "pgOrderInfo"
        Me.pgOrderInfo.PropertySort = System.Windows.Forms.PropertySort.NoSort
        Me.pgOrderInfo.Size = New System.Drawing.Size(415, 267)
        Me.pgOrderInfo.TabIndex = 4
        Me.pgOrderInfo.ToolbarVisible = False
        '
        'gbResults
        '
        Me.gbResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbResults.BackColor = System.Drawing.SystemColors.Control
        Me.gbResults.Controls.Add(Me.dgvResults)
        Me.gbResults.Location = New System.Drawing.Point(0, 401)
        Me.gbResults.Margin = New System.Windows.Forms.Padding(4)
        Me.gbResults.Name = "gbResults"
        Me.gbResults.Padding = New System.Windows.Forms.Padding(4)
        Me.gbResults.Size = New System.Drawing.Size(1256, 570)
        Me.gbResults.TabIndex = 3
        Me.gbResults.TabStop = False
        Me.gbResults.Text = "Results"
        '
        'dgvResults
        '
        Me.dgvResults.AllowUserToAddRows = False
        Me.dgvResults.AllowUserToDeleteRows = False
        Me.dgvResults.AllowUserToResizeColumns = False
        Me.dgvResults.AllowUserToResizeRows = False
        Me.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvResults.Location = New System.Drawing.Point(4, 19)
        Me.dgvResults.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvResults.MultiSelect = False
        Me.dgvResults.Name = "dgvResults"
        Me.dgvResults.ReadOnly = True
        Me.dgvResults.RowHeadersVisible = False
        Me.dgvResults.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvResults.Size = New System.Drawing.Size(1248, 547)
        Me.dgvResults.TabIndex = 0
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(551, 16)
        Me.btnStart.Margin = New System.Windows.Forms.Padding(4)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(145, 57)
        Me.btnStart.TabIndex = 6
        Me.btnStart.Text = "Start Measurement"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'gbPrepare
        '
        Me.gbPrepare.BackColor = System.Drawing.SystemColors.Control
        Me.gbPrepare.Controls.Add(Me.btnBypassTest)
        Me.gbPrepare.Controls.Add(Me.rb_HeartBeat)
        Me.gbPrepare.Controls.Add(Me.Btn_AutoManualMode)
        Me.gbPrepare.Controls.Add(Me.tbNumberOfCoreTested)
        Me.gbPrepare.Controls.Add(Me.lblNumberOfCores)
        Me.gbPrepare.Controls.Add(Me.tbCurrentHarness)
        Me.gbPrepare.Controls.Add(Me.lblCurrentHarness)
        Me.gbPrepare.Controls.Add(Me.gbCurrentResult)
        Me.gbPrepare.Controls.Add(Me.btnRemade)
        Me.gbPrepare.Controls.Add(Me.btnReprocessed)
        Me.gbPrepare.Controls.Add(Me.btnOverwrite)
        Me.gbPrepare.Controls.Add(Me.tbWorkOrder)
        Me.gbPrepare.Controls.Add(Me.lblWorkOrderNo)
        Me.gbPrepare.Controls.Add(Me.btnAbandon)
        Me.gbPrepare.Controls.Add(Me.tbOperator)
        Me.gbPrepare.Controls.Add(Me.btnAccept)
        Me.gbPrepare.Controls.Add(Me.lblOperator)
        Me.gbPrepare.Controls.Add(Me.btnStop)
        Me.gbPrepare.Controls.Add(Me.tbWeight)
        Me.gbPrepare.Controls.Add(Me.btnStart)
        Me.gbPrepare.Controls.Add(Me.tbSerialNumber)
        Me.gbPrepare.Controls.Add(Me.lblWeight)
        Me.gbPrepare.Controls.Add(Me.lblSerialNumber)
        Me.gbPrepare.Location = New System.Drawing.Point(0, 31)
        Me.gbPrepare.Margin = New System.Windows.Forms.Padding(4)
        Me.gbPrepare.Name = "gbPrepare"
        Me.gbPrepare.Padding = New System.Windows.Forms.Padding(4)
        Me.gbPrepare.Size = New System.Drawing.Size(705, 290)
        Me.gbPrepare.TabIndex = 0
        Me.gbPrepare.TabStop = False
        '
        'btnBypassTest
        '
        Me.btnBypassTest.BackColor = System.Drawing.Color.LightBlue
        Me.btnBypassTest.Location = New System.Drawing.Point(428, 81)
        Me.btnBypassTest.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBypassTest.Name = "btnBypassTest"
        Me.btnBypassTest.Size = New System.Drawing.Size(68, 102)
        Me.btnBypassTest.TabIndex = 87
        Me.btnBypassTest.Text = "Bypass Test MODE OFF"
        Me.btnBypassTest.UseVisualStyleBackColor = False
        '
        'rb_HeartBeat
        '
        Me.rb_HeartBeat.AutoSize = True
        Me.rb_HeartBeat.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.rb_HeartBeat.Location = New System.Drawing.Point(367, 151)
        Me.rb_HeartBeat.Margin = New System.Windows.Forms.Padding(4)
        Me.rb_HeartBeat.Name = "rb_HeartBeat"
        Me.rb_HeartBeat.Size = New System.Drawing.Size(17, 16)
        Me.rb_HeartBeat.TabIndex = 86
        Me.rb_HeartBeat.TabStop = True
        Me.rb_HeartBeat.UseVisualStyleBackColor = False
        '
        'Btn_AutoManualMode
        '
        Me.Btn_AutoManualMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Btn_AutoManualMode.Location = New System.Drawing.Point(271, 81)
        Me.Btn_AutoManualMode.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_AutoManualMode.Name = "Btn_AutoManualMode"
        Me.Btn_AutoManualMode.Size = New System.Drawing.Size(135, 102)
        Me.Btn_AutoManualMode.TabIndex = 85
        Me.Btn_AutoManualMode.Text = "AUTO MODE"
        Me.Btn_AutoManualMode.UseVisualStyleBackColor = False
        '
        'tbNumberOfCoreTested
        '
        Me.tbNumberOfCoreTested.Location = New System.Drawing.Point(428, 49)
        Me.tbNumberOfCoreTested.Margin = New System.Windows.Forms.Padding(4)
        Me.tbNumberOfCoreTested.Name = "tbNumberOfCoreTested"
        Me.tbNumberOfCoreTested.ReadOnly = True
        Me.tbNumberOfCoreTested.Size = New System.Drawing.Size(68, 22)
        Me.tbNumberOfCoreTested.TabIndex = 84
        '
        'lblNumberOfCores
        '
        Me.lblNumberOfCores.AutoSize = True
        Me.lblNumberOfCores.Location = New System.Drawing.Point(267, 50)
        Me.lblNumberOfCores.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNumberOfCores.Name = "lblNumberOfCores"
        Me.lblNumberOfCores.Size = New System.Drawing.Size(153, 17)
        Me.lblNumberOfCores.TabIndex = 83
        Me.lblNumberOfCores.Text = "Number of core tested:"
        '
        'tbCurrentHarness
        '
        Me.tbCurrentHarness.Location = New System.Drawing.Point(428, 17)
        Me.tbCurrentHarness.Margin = New System.Windows.Forms.Padding(4)
        Me.tbCurrentHarness.Name = "tbCurrentHarness"
        Me.tbCurrentHarness.ReadOnly = True
        Me.tbCurrentHarness.Size = New System.Drawing.Size(68, 22)
        Me.tbCurrentHarness.TabIndex = 82
        '
        'lblCurrentHarness
        '
        Me.lblCurrentHarness.AutoSize = True
        Me.lblCurrentHarness.Location = New System.Drawing.Point(267, 20)
        Me.lblCurrentHarness.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCurrentHarness.Name = "lblCurrentHarness"
        Me.lblCurrentHarness.Size = New System.Drawing.Size(120, 17)
        Me.lblCurrentHarness.TabIndex = 81
        Me.lblCurrentHarness.Text = "Current Harness: "
        '
        'gbCurrentResult
        '
        Me.gbCurrentResult.Controls.Add(Me.lblPercentageActualJValue)
        Me.gbCurrentResult.Controls.Add(Me.lblPercentageGreenLossLevelValue)
        Me.gbCurrentResult.Controls.Add(Me.lblPercentageActualJ)
        Me.gbCurrentResult.Controls.Add(Me.lblGradeValue)
        Me.gbCurrentResult.Controls.Add(Me.lblWattsOrVTrms)
        Me.gbCurrentResult.Controls.Add(Me.lblATrmsOrAmpsValue)
        Me.gbCurrentResult.Controls.Add(Me.lblATrmsOrAmps)
        Me.gbCurrentResult.Controls.Add(Me.lblWattsOrVTrmsValue)
        Me.gbCurrentResult.Controls.Add(Me.lblGrade)
        Me.gbCurrentResult.Controls.Add(Me.lblPercentageGreenLossLevel)
        Me.gbCurrentResult.Location = New System.Drawing.Point(11, 191)
        Me.gbCurrentResult.Margin = New System.Windows.Forms.Padding(4)
        Me.gbCurrentResult.Name = "gbCurrentResult"
        Me.gbCurrentResult.Padding = New System.Windows.Forms.Padding(4)
        Me.gbCurrentResult.Size = New System.Drawing.Size(685, 49)
        Me.gbCurrentResult.TabIndex = 16
        Me.gbCurrentResult.TabStop = False
        Me.gbCurrentResult.Text = "Current result"
        '
        'lblPercentageActualJValue
        '
        Me.lblPercentageActualJValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPercentageActualJValue.Location = New System.Drawing.Point(35, 22)
        Me.lblPercentageActualJValue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPercentageActualJValue.Name = "lblPercentageActualJValue"
        Me.lblPercentageActualJValue.Size = New System.Drawing.Size(69, 18)
        Me.lblPercentageActualJValue.TabIndex = 29
        Me.lblPercentageActualJValue.Text = "Value"
        '
        'lblPercentageGreenLossLevelValue
        '
        Me.lblPercentageGreenLossLevelValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPercentageGreenLossLevelValue.Location = New System.Drawing.Point(599, 22)
        Me.lblPercentageGreenLossLevelValue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPercentageGreenLossLevelValue.Name = "lblPercentageGreenLossLevelValue"
        Me.lblPercentageGreenLossLevelValue.Size = New System.Drawing.Size(69, 18)
        Me.lblPercentageGreenLossLevelValue.TabIndex = 33
        Me.lblPercentageGreenLossLevelValue.Text = "Value"
        '
        'lblPercentageActualJ
        '
        Me.lblPercentageActualJ.AutoSize = True
        Me.lblPercentageActualJ.Location = New System.Drawing.Point(7, 23)
        Me.lblPercentageActualJ.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPercentageActualJ.Name = "lblPercentageActualJ"
        Me.lblPercentageActualJ.Size = New System.Drawing.Size(27, 17)
        Me.lblPercentageActualJ.TabIndex = 19
        Me.lblPercentageActualJ.Text = "%J"
        '
        'lblGradeValue
        '
        Me.lblGradeValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblGradeValue.Location = New System.Drawing.Point(459, 22)
        Me.lblGradeValue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblGradeValue.Name = "lblGradeValue"
        Me.lblGradeValue.Size = New System.Drawing.Size(69, 18)
        Me.lblGradeValue.TabIndex = 32
        Me.lblGradeValue.Text = "Value"
        '
        'lblWattsOrVTrms
        '
        Me.lblWattsOrVTrms.AutoSize = True
        Me.lblWattsOrVTrms.Location = New System.Drawing.Point(119, 23)
        Me.lblWattsOrVTrms.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblWattsOrVTrms.Name = "lblWattsOrVTrms"
        Me.lblWattsOrVTrms.Size = New System.Drawing.Size(44, 17)
        Me.lblWattsOrVTrms.TabIndex = 21
        Me.lblWattsOrVTrms.Text = "Watts"
        '
        'lblATrmsOrAmpsValue
        '
        Me.lblATrmsOrAmpsValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblATrmsOrAmpsValue.Location = New System.Drawing.Point(317, 22)
        Me.lblATrmsOrAmpsValue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblATrmsOrAmpsValue.Name = "lblATrmsOrAmpsValue"
        Me.lblATrmsOrAmpsValue.Size = New System.Drawing.Size(69, 18)
        Me.lblATrmsOrAmpsValue.TabIndex = 31
        Me.lblATrmsOrAmpsValue.Text = "Value"
        '
        'lblATrmsOrAmps
        '
        Me.lblATrmsOrAmps.AutoSize = True
        Me.lblATrmsOrAmps.Location = New System.Drawing.Point(256, 23)
        Me.lblATrmsOrAmps.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblATrmsOrAmps.Name = "lblATrmsOrAmps"
        Me.lblATrmsOrAmps.Size = New System.Drawing.Size(54, 17)
        Me.lblATrmsOrAmps.TabIndex = 23
        Me.lblATrmsOrAmps.Text = "AT-rms"
        '
        'lblWattsOrVTrmsValue
        '
        Me.lblWattsOrVTrmsValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWattsOrVTrmsValue.Location = New System.Drawing.Point(173, 22)
        Me.lblWattsOrVTrmsValue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblWattsOrVTrmsValue.Name = "lblWattsOrVTrmsValue"
        Me.lblWattsOrVTrmsValue.Size = New System.Drawing.Size(69, 18)
        Me.lblWattsOrVTrmsValue.TabIndex = 30
        Me.lblWattsOrVTrmsValue.Text = "Value"
        '
        'lblGrade
        '
        Me.lblGrade.AutoSize = True
        Me.lblGrade.Location = New System.Drawing.Point(401, 23)
        Me.lblGrade.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblGrade.Name = "lblGrade"
        Me.lblGrade.Size = New System.Drawing.Size(48, 17)
        Me.lblGrade.TabIndex = 25
        Me.lblGrade.Text = "Grade"
        '
        'lblPercentageGreenLossLevel
        '
        Me.lblPercentageGreenLossLevel.AutoSize = True
        Me.lblPercentageGreenLossLevel.Location = New System.Drawing.Point(543, 23)
        Me.lblPercentageGreenLossLevel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPercentageGreenLossLevel.Name = "lblPercentageGreenLossLevel"
        Me.lblPercentageGreenLossLevel.Size = New System.Drawing.Size(48, 17)
        Me.lblPercentageGreenLossLevel.TabIndex = 27
        Me.lblPercentageGreenLossLevel.Text = "% Typ"
        '
        'btnRemade
        '
        Me.btnRemade.Location = New System.Drawing.Point(593, 249)
        Me.btnRemade.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRemade.Name = "btnRemade"
        Me.btnRemade.Size = New System.Drawing.Size(104, 28)
        Me.btnRemade.TabIndex = 13
        Me.btnRemade.Text = "Remade"
        Me.btnRemade.UseVisualStyleBackColor = True
        '
        'btnReprocessed
        '
        Me.btnReprocessed.Location = New System.Drawing.Point(445, 249)
        Me.btnReprocessed.Margin = New System.Windows.Forms.Padding(4)
        Me.btnReprocessed.Name = "btnReprocessed"
        Me.btnReprocessed.Size = New System.Drawing.Size(104, 28)
        Me.btnReprocessed.TabIndex = 12
        Me.btnReprocessed.Text = "Reprocessed"
        Me.btnReprocessed.UseVisualStyleBackColor = True
        '
        'btnOverwrite
        '
        Me.btnOverwrite.Location = New System.Drawing.Point(301, 249)
        Me.btnOverwrite.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOverwrite.Name = "btnOverwrite"
        Me.btnOverwrite.Size = New System.Drawing.Size(104, 28)
        Me.btnOverwrite.TabIndex = 11
        Me.btnOverwrite.Text = "Overwrite"
        Me.btnOverwrite.UseVisualStyleBackColor = True
        '
        'btnAbandon
        '
        Me.btnAbandon.Location = New System.Drawing.Point(157, 249)
        Me.btnAbandon.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAbandon.Name = "btnAbandon"
        Me.btnAbandon.Size = New System.Drawing.Size(104, 28)
        Me.btnAbandon.TabIndex = 10
        Me.btnAbandon.Text = "Abandon"
        Me.btnAbandon.UseVisualStyleBackColor = True
        '
        'btnAccept
        '
        Me.btnAccept.Location = New System.Drawing.Point(12, 249)
        Me.btnAccept.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(104, 28)
        Me.btnAccept.TabIndex = 9
        Me.btnAccept.Text = "Accept"
        Me.btnAccept.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Location = New System.Drawing.Point(551, 81)
        Me.btnStop.Margin = New System.Windows.Forms.Padding(4)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(145, 57)
        Me.btnStop.TabIndex = 7
        Me.btnStop.Text = "Stop Measurement"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'tbWeight
        '
        Me.tbWeight.Location = New System.Drawing.Point(109, 108)
        Me.tbWeight.Margin = New System.Windows.Forms.Padding(4)
        Me.tbWeight.Name = "tbWeight"
        Me.tbWeight.Size = New System.Drawing.Size(132, 22)
        Me.tbWeight.TabIndex = 5
        '
        'tbSerialNumber
        '
        Me.tbSerialNumber.Location = New System.Drawing.Point(109, 78)
        Me.tbSerialNumber.Margin = New System.Windows.Forms.Padding(4)
        Me.tbSerialNumber.Name = "tbSerialNumber"
        Me.tbSerialNumber.Size = New System.Drawing.Size(132, 22)
        Me.tbSerialNumber.TabIndex = 4
        '
        'lblWeight
        '
        Me.lblWeight.AutoSize = True
        Me.lblWeight.Location = New System.Drawing.Point(8, 111)
        Me.lblWeight.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblWeight.Name = "lblWeight"
        Me.lblWeight.Size = New System.Drawing.Size(84, 17)
        Me.lblWeight.TabIndex = 1
        Me.lblWeight.Text = "Weight (lbs)"
        '
        'lblSerialNumber
        '
        Me.lblSerialNumber.AutoSize = True
        Me.lblSerialNumber.Location = New System.Drawing.Point(8, 81)
        Me.lblSerialNumber.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSerialNumber.Name = "lblSerialNumber"
        Me.lblSerialNumber.Size = New System.Drawing.Size(56, 17)
        Me.lblSerialNumber.TabIndex = 0
        Me.lblSerialNumber.Text = "Serial #"
        '
        'gbStatus
        '
        Me.gbStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbStatus.Controls.Add(Me.lblCustomerMessage)
        Me.gbStatus.Controls.Add(Me.lblStatus)
        Me.gbStatus.Location = New System.Drawing.Point(0, 329)
        Me.gbStatus.Margin = New System.Windows.Forms.Padding(4)
        Me.gbStatus.Name = "gbStatus"
        Me.gbStatus.Padding = New System.Windows.Forms.Padding(4)
        Me.gbStatus.Size = New System.Drawing.Size(1248, 64)
        Me.gbStatus.TabIndex = 10
        Me.gbStatus.TabStop = False
        Me.gbStatus.Text = "Status"
        '
        'lblCustomerMessage
        '
        Me.lblCustomerMessage.AutoSize = True
        Me.lblCustomerMessage.ForeColor = System.Drawing.Color.Black
        Me.lblCustomerMessage.Location = New System.Drawing.Point(12, 85)
        Me.lblCustomerMessage.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCustomerMessage.Name = "lblCustomerMessage"
        Me.lblCustomerMessage.Size = New System.Drawing.Size(0, 17)
        Me.lblCustomerMessage.TabIndex = 80
        '
        'lblStatus
        '
        Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.Blue
        Me.lblStatus.Location = New System.Drawing.Point(4, 19)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(1240, 41)
        Me.lblStatus.TabIndex = 8
        Me.lblStatus.Text = "Ready for operation"
        '
        'tsMain
        '
        Me.tsMain.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.tsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbRefresh, Me.tsbPrint, Me.tsbPrintSetup, Me.tsbSettings})
        Me.tsMain.Location = New System.Drawing.Point(0, 0)
        Me.tsMain.Name = "tsMain"
        Me.tsMain.Size = New System.Drawing.Size(1261, 27)
        Me.tsMain.TabIndex = 11
        Me.tsMain.Text = "ToolStrip1"
        '
        'tsbRefresh
        '
        Me.tsbRefresh.Image = Global.My.Resources.Resources._112_RefreshArrow_Blue_48x48_72
        Me.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbRefresh.Name = "tsbRefresh"
        Me.tsbRefresh.Size = New System.Drawing.Size(82, 24)
        Me.tsbRefresh.Text = "Refresh"
        '
        'tsbPrint
        '
        Me.tsbPrint.Image = Global.My.Resources.Resources.Printer
        Me.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPrint.Name = "tsbPrint"
        Me.tsbPrint.Size = New System.Drawing.Size(60, 24)
        Me.tsbPrint.Text = "Print"
        '
        'tsbPrintSetup
        '
        Me.tsbPrintSetup.Image = Global.My.Resources.Resources._007_PrintView_48x48_72
        Me.tsbPrintSetup.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPrintSetup.Name = "tsbPrintSetup"
        Me.tsbPrintSetup.Size = New System.Drawing.Size(102, 24)
        Me.tsbPrintSetup.Text = "Print Setup"
        '
        'tsbSettings
        '
        Me.tsbSettings.Image = Global.My.Resources.Resources.settings_48
        Me.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSettings.Name = "tsbSettings"
        Me.tsbSettings.Size = New System.Drawing.Size(83, 24)
        Me.tsbSettings.Text = "Settings"
        '
        'pbLogo
        '
        Me.pbLogo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbLogo.Image = Global.My.Resources.Resources.BM_Logo_E
        Me.pbLogo.Location = New System.Drawing.Point(1136, 31)
        Me.pbLogo.Margin = New System.Windows.Forms.Padding(4)
        Me.pbLogo.Name = "pbLogo"
        Me.pbLogo.Size = New System.Drawing.Size(120, 156)
        Me.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbLogo.TabIndex = 1
        Me.pbLogo.TabStop = False
        '
        'GuiOperator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1261, 978)
        Me.Controls.Add(Me.tsMain)
        Me.Controls.Add(Me.gbStatus)
        Me.Controls.Add(Me.gbPrepare)
        Me.Controls.Add(Me.gbResults)
        Me.Controls.Add(Me.gbWorkOrderInfo)
        Me.Controls.Add(Me.pbLogo)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "GuiOperator"
        Me.Text = "Cogent MPG-Expert"
        Me.gbWorkOrderInfo.ResumeLayout(False)
        Me.gbResults.ResumeLayout(False)
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbPrepare.ResumeLayout(False)
        Me.gbPrepare.PerformLayout()
        Me.gbCurrentResult.ResumeLayout(False)
        Me.gbCurrentResult.PerformLayout()
        Me.gbStatus.ResumeLayout(False)
        Me.gbStatus.PerformLayout()
        Me.tsMain.ResumeLayout(False)
        Me.tsMain.PerformLayout()
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbOperator As System.Windows.Forms.TextBox
    Friend WithEvents lblOperator As System.Windows.Forms.Label
    Friend WithEvents tbWorkOrder As System.Windows.Forms.TextBox
    Friend WithEvents lblWorkOrderNo As System.Windows.Forms.Label
    Friend WithEvents gbWorkOrderInfo As System.Windows.Forms.GroupBox
    Friend WithEvents gbResults As System.Windows.Forms.GroupBox
    Friend WithEvents pgOrderInfo As System.Windows.Forms.PropertyGrid
    Friend WithEvents dgvResults As System.Windows.Forms.DataGridView
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents gbPrepare As System.Windows.Forms.GroupBox
    Friend WithEvents tbWeight As System.Windows.Forms.TextBox
    Friend WithEvents tbSerialNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblWeight As System.Windows.Forms.Label
    Friend WithEvents lblSerialNumber As System.Windows.Forms.Label
    Friend WithEvents lblPercentageGreenLossLevel As System.Windows.Forms.Label
    Friend WithEvents lblGrade As System.Windows.Forms.Label
    Friend WithEvents lblATrmsOrAmps As System.Windows.Forms.Label
    Friend WithEvents lblWattsOrVTrms As System.Windows.Forms.Label
    Friend WithEvents lblPercentageActualJ As System.Windows.Forms.Label
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnRemade As System.Windows.Forms.Button
    Friend WithEvents btnReprocessed As System.Windows.Forms.Button
    Friend WithEvents btnOverwrite As System.Windows.Forms.Button
    Friend WithEvents btnAbandon As System.Windows.Forms.Button
    Friend WithEvents btnAccept As System.Windows.Forms.Button
    Friend WithEvents gbStatus As System.Windows.Forms.GroupBox
    Friend WithEvents lblCustomerMessage As System.Windows.Forms.Label
    Friend WithEvents lblPercentageGreenLossLevelValue As System.Windows.Forms.Label
    Friend WithEvents lblGradeValue As System.Windows.Forms.Label
    Friend WithEvents lblATrmsOrAmpsValue As System.Windows.Forms.Label
    Friend WithEvents lblWattsOrVTrmsValue As System.Windows.Forms.Label
    Friend WithEvents lblPercentageActualJValue As System.Windows.Forms.Label
    Friend WithEvents gbCurrentResult As System.Windows.Forms.GroupBox
    Friend WithEvents lblCurrentHarness As System.Windows.Forms.Label
    Friend WithEvents tbCurrentHarness As System.Windows.Forms.TextBox
    Friend WithEvents tsMain As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbPrintSetup As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSettings As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbNumberOfCoreTested As System.Windows.Forms.TextBox
    Friend WithEvents lblNumberOfCores As System.Windows.Forms.Label
    Friend WithEvents pbLogo As System.Windows.Forms.PictureBox
    Friend WithEvents rb_HeartBeat As RadioButton
    Friend WithEvents Btn_AutoManualMode As Button
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnBypassTest As Button
End Class
