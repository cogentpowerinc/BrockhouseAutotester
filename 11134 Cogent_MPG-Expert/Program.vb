Public Class Program
    Public Shared Sub Main()
        'Set path for logging

        Try

            Logger.Logger.Instance.FilePath = FileIO.SpecialDirectories.AllUsersApplicationData + "\Cogent_MPG-Expert_log.txt"

            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New GuiOperator)

        Catch ex As Exception
            Logger.Logger.Instance.Log("Program.vb;+ Load", Logger.Logger.eStatus.Exception, ex)
        End Try
    End Sub
End Class
