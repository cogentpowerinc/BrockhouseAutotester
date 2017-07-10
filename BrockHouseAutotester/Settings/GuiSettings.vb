Namespace Settings
    Public Class GuiSettings
        Public Sub New(ByVal settings As Settings)

            ' Dieser Aufruf ist für den Designer erforderlich.
            InitializeComponent()

            ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
            _settings = settings
        End Sub


        Private Sub GuiSettings_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            pgSettings.SelectedObject = _settings
        End Sub

        Private _settings As Settings = Nothing
        Public ReadOnly Property Settings As Settings
            Get
                Return _settings
            End Get
        End Property

        Private Sub btnAccept_Click(sender As System.Object, e As System.EventArgs) Handles btnAccept.Click
            DialogResult = Windows.Forms.DialogResult.OK
            Close()
        End Sub

        Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
            DialogResult = Windows.Forms.DialogResult.Cancel
            Close()
        End Sub
    End Class
End Namespace