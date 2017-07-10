Imports System.Collections.Generic


Namespace Common
    Class DebugTools
        Public Shared ActionList As New List(Of String)()




        Public Shared Function UpdateActionList(ActionText As String) As String
            Dim myAction As String = (ActionText & Convert.ToString("   ")) + DateTime.Now.ToLongDateString() + "   " + DateTime.Now.ToLongTimeString()
            'if (Config.myProgConfig.SaveFaultLog)
            '{
            '    using (StreamWriter sw = File.AppendText(FileLocations.ProjectInfoPath + FileLocations.FileActions))
            '    {
            '        sw.WriteLine(myAction);
            '    }

            '}
            'ActionList.Add(myAction + System.Environment.NewLine);
            'if (ActionList.Count > 50)
            '    ActionList.RemoveAt(0);

            Return ActionText
        End Function
    End Class
End Namespace
