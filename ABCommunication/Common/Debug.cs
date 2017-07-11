using System;
using System.Collections.Generic;


namespace Common
{
    class DebugTools
    {
        public static List<string> ActionList = new List<string>();

        public static string UpdateActionList(string ActionText)
        {
            string myAction = ActionText + "   " + DateTime.Now.ToLongDateString() + "   " + DateTime.Now.ToLongTimeString();
            //if (Config.myProgConfig.SaveFaultLog)
            //{
            //    using (StreamWriter sw = File.AppendText(FileLocations.ProjectInfoPath + FileLocations.FileActions))
            //    {
            //        sw.WriteLine(myAction);
            //    }

            //}
            ActionList.Add(myAction + System.Environment.NewLine);
            if (ActionList.Count > 50)
                ActionList.RemoveAt(0);

            return ActionText;
        }
    }
}