Namespace Settings
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class GuiSettings
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
            Me.pgSettings = New System.Windows.Forms.PropertyGrid()
            Me.btnAccept = New System.Windows.Forms.Button()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'pgSettings
            '
            Me.pgSettings.Location = New System.Drawing.Point(12, 12)
            Me.pgSettings.Name = "pgSettings"
            Me.pgSettings.PropertySort = System.Windows.Forms.PropertySort.Categorized
            Me.pgSettings.Size = New System.Drawing.Size(636, 448)
            Me.pgSettings.TabIndex = 0
            '
            'btnAccept
            '
            Me.btnAccept.Location = New System.Drawing.Point(16, 512)
            Me.btnAccept.Name = "btnAccept"
            Me.btnAccept.Size = New System.Drawing.Size(75, 23)
            Me.btnAccept.TabIndex = 1
            Me.btnAccept.Text = "Accept"
            Me.btnAccept.UseVisualStyleBackColor = True
            '
            'btnCancel
            '
            Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.btnCancel.Location = New System.Drawing.Point(97, 512)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(75, 23)
            Me.btnCancel.TabIndex = 2
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = True
            '
            'GuiSettings
            '
            Me.AcceptButton = Me.btnAccept
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.btnCancel
            Me.ClientSize = New System.Drawing.Size(660, 547)
            Me.ControlBox = False
            Me.Controls.Add(Me.btnCancel)
            Me.Controls.Add(Me.btnAccept)
            Me.Controls.Add(Me.pgSettings)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Name = "GuiSettings"
            Me.ShowInTaskbar = False
            Me.Text = "Settings"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents pgSettings As System.Windows.Forms.PropertyGrid
        Friend WithEvents btnAccept As System.Windows.Forms.Button
        Friend WithEvents btnCancel As System.Windows.Forms.Button
    End Class
End Namespace
