<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GuiPrinter
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.EinrichtTafel1 = New LabelPrinter.EinrichtTafel()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(9, 587)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'EinrichtTafel1
        '
        Me.EinrichtTafel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EinrichtTafel1.Location = New System.Drawing.Point(9, 10)
        Me.EinrichtTafel1.Margin = New System.Windows.Forms.Padding(2)
        Me.EinrichtTafel1.Name = "EinrichtTafel1"
        Me.EinrichtTafel1.Size = New System.Drawing.Size(909, 567)
        Me.EinrichtTafel1.TabIndex = 4
        '
        'GuiPrinter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(923, 622)
        Me.ControlBox = False
        Me.Controls.Add(Me.EinrichtTafel1)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "GuiPrinter"
        Me.ShowInTaskbar = False
        Me.Text = "Print setup"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents EinrichtTafel1 As LabelPrinter.EinrichtTafel
End Class
