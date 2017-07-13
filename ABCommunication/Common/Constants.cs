using System;
using System.Diagnostics;
using System.Reflection;

namespace Common
{
    /// <summary>
    /// 
    /// </summary>
    public class PrimaryVariables
    {

       
        public const string ChannelHeader = "{Ch_";
        public const string ChannelFooter = "_Ch}";
       
        public const string AddressHeader = "{Adrs_";
        public const string AddressFooter = "_Adrs}";

        public const string FailedRead = "{***Read Failure***}";
        public static int ABDataReadErrors = 0;
        public static int ABDataWriteErrors = 0;


        public static string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static string fileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
        public static string productVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

        public static string ProgramVersion = "Assembly Version : " + assemblyVersion + "   File Version : " + fileVersion + "  Product Version : " + productVersion;



        public static BoolWithEventStatus MasterLiveDataMode = new BoolWithEventStatus();
       
    }

    public class BoolWithEventStatus 
    {

        public  event EventHandler Status;
        protected  virtual void StatusChange()
        {
            if (Status != null) Status(this, EventArgs.Empty);
        }
        private  bool _State = true;
        public  bool State
        {
            get
            {
                return _State;
            }
            set
            {
                _State = value;
                if (!_State)
                    StatusChange();
            }
        }
    }

}
    /// <summary>
    /// These are the LEGAL classes that AB will accept as a DataType.
    /// This should be used in writing and data.
    /// This format will be returned with the Variable Names.
    /// </summary>
    public enum ABTagClass
{

    BOOL = 0,
    SINT = 1,
   INT = 2,
    DINT = 3,
    REAL = 4,
    STRING = 5,
    LINT = 6,
    OBJECT = 7,
    NotFound = 99

}




