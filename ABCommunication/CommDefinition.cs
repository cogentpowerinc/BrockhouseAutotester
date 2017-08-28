using Common;
using PLC;
using System;
using System.IO;
using System.Windows.Threading;

namespace ABCommunication
{
    public class CommDefinition_Comms
    {
        #region IsOnline
        private bool _IsOnline = false;
        public bool IsOnline
        {
            get
            {
                return _IsOnline;
            }
            set
            {
                if (_IsOnline != value)
                {
                    _IsOnline = value;
                    PrimaryVariables.MasterLiveDataMode.State = value;
                }
            }
        }
        #endregion

        private string _Name = "AT_1";
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }


        private CommManager.PLCDriverType _DriverType = CommManager.PLCDriverType.AllenBradley_CLX;
        public CommManager.PLCDriverType DriverType
        {
            get
            {
                return _DriverType;
            }
            set
            {
                _DriverType = value;

            }
        }


        private string _IPAddress = "172.16.1.101";
        public string IPAddress
        {
            get
            {
                return _IPAddress;
            }
            set
            {
                _IPAddress = value;

            }
        }
        private string _SlotNum = "0";
        public string SlotNum
        {
            get
            {
                return _SlotNum;
            }
            set
            {
                _SlotNum = value;

            }
        }
        private Int32 _Timeout = 5000;
        public Int32 Timeout
        {
            get
            {
                return _Timeout;
            }
            set
            {
                _Timeout = value;
            }
        }

        private Int32 _CPUType = 4;
        public Int32 CPUType
        {
            get
            {
                return _CPUType;
            }
            set
            {
                _CPUType = value;

            }
        }

        private Int32 _UpdateInterval_ms = 50;
        public Int32 UpdateInterval_ms
        {
            get
            {
                return _UpdateInterval_ms;
            }
            set
            {
                _UpdateInterval_ms = value;

            }
        }



        private Int32 _HeartBeatInterval_ms = 100;
        public Int32 HeartBeatInterval_ms
        {
            get
            {
                return _HeartBeatInterval_ms;
            }
            set
            {
                _HeartBeatInterval_ms = value;

            }
        }


        private Int32 _HeartBeatMonitorTime_ms = 20000;
        public Int32 HeartBeatMonitorTime_ms
        {
            get
            {
                return _HeartBeatMonitorTime_ms;
            }
            set
            {
                _HeartBeatMonitorTime_ms = value;

            }
        }

    }

    public class CommDefinition_Input
    {



        private bool Success = false;
        #region WorkOrder Region
        private string _WorkOrder_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.WorkOrder_Adrs}";
        private string _WorkOrder = "Not Init.";
        public string WorkOrder
        {
            get
            {
                if (PrimaryVariables.MasterLiveDataMode.State)
                    _WorkOrder = PLC.CommManager.GetPLCValue_STRING(_WorkOrder_Address, out Success);
                return _WorkOrder;
            }
            set
            {
                _WorkOrder = value;

            }
        }
        #endregion

        #region SerialNumber Region
        private string _SerialNumber_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.SerialNumber_Adrs}";
        public string _SerialNumber = "Not Init.";
        public string SerialNumber
        {
            get
            {
                if (PrimaryVariables.MasterLiveDataMode.State)
                    _SerialNumber = PLC.CommManager.GetPLCValue_STRING(_SerialNumber_Address, out Success);
                return _SerialNumber;
            }
            set
            {
                _SerialNumber = value;

            }
        }
        #endregion

        #region SubLineNumber Region
        private string _SubLineNumber_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.SubLineNumber_Adrs}";
        public string _SubLineNumber = "Not Init.";
        public string SubLineNumber
        {
            get
            {
                if (PrimaryVariables.MasterLiveDataMode.State)
                    _SubLineNumber = PLC.CommManager.GetPLCValue_STRING(_SubLineNumber_Address, out Success);
                return _SubLineNumber;
            }
            set
            {
                _SubLineNumber = value;

            }
        }
        #endregion

        #region Weight Region
        private string _Weight_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.Weight_Adrs}";
        private double _Weight = -1;
        public double Weight
        {
            get
            {
                if (PrimaryVariables.MasterLiveDataMode.State)
                    _Weight = PLC.CommManager.GetPLCValue_DOUBLE(_Weight_Address, out Success);
                return _Weight;
            }
            set
            {
                _Weight = value;

            }
        }

        #endregion

        #region Temp Region
        private string _Temp_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.Temp_Adrs}";
        private double _Temp = -1;
        public double Temp
        {
            get
            {
                if (PrimaryVariables.MasterLiveDataMode.State)
                    _Temp = PLC.CommManager.GetPLCValue_DOUBLE(_Temp_Address, out Success);
                return _Temp;
            }
            set
            {
                _Temp = value;

            }
        }
        #endregion

        #region InitTest Region

        private string _InitTest_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.InitTest_Adrs}";
        private bool _InitTest = false;
        public bool InitTest
        {
            get
            {
                if (PrimaryVariables.MasterLiveDataMode.State)
                    _InitTest = PLC.CommManager.GetPLCValue_BOOL(_InitTest_Address, out Success);
                return _InitTest;
            }
            set
            {
                _InitTest = value;

            }
        }

        #endregion

        #region Reset Region
        private string _Reset_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.Reset_Adrs}";
        private bool _Reset = false;
        public bool Reset
        {
            get
            {
                if (PrimaryVariables.MasterLiveDataMode.State)
                    _Reset = PLC.CommManager.GetPLCValue_BOOL(_Reset_Address, out Success);
                return _Reset;
            }
            set
            {
                _Reset = value;

            }
        }
        #endregion

        #region ResultResponse Region

        private string _ResultResponse_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.ResultResponse_Adrs}";
        private Int32 _ResultResponse = -99;
        public Int32 ResultResponse
        {
            get
            {
                if (PrimaryVariables.MasterLiveDataMode.State)
                    _ResultResponse = PLC.CommManager.GetPLCValue_INT32(_ResultResponse_Address, out Success);
                return _ResultResponse;
            }
            set
            {
                _ResultResponse = value;

            }
        }

        #endregion
        #region AutoMode Region
        private string _AutoMode_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.AutoMode_Adrs}";
        private bool _AutoMode = false;
        public bool AutoMode
        {
            get
            {
                if (PrimaryVariables.MasterLiveDataMode.State)
                    _AutoMode = PLC.CommManager.GetPLCValue_BOOL(_AutoMode_Address, out Success);
                return _AutoMode;
            }
            set
            {
                _AutoMode = value;

            }
        }

        #endregion

        #region Heartbeat Region
      //  private string _Heartbeat_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.Heartbeat_Adrs}";
        private bool _Heartbeat = false;
        public bool Heartbeat
        {
            get
            {
                return _Heartbeat;
            }
            set
            {
               
                    _Heartbeat = value;

            }
        }

        #endregion

    }

    public class CommDefinition_Output
    {
        bool Success = false;

        #region ReadyForData Region

        private string _ReadyForData_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.ReadyForData_Adrs}";
        private bool _ReadyForData = false;
        public bool ReadyForData
        {
            get
            {
                return _ReadyForData;
            }
            set
            {
                if (_ReadyForData != value)
                {
                    _ReadyForData = value;
                    if (PrimaryVariables.MasterLiveDataMode.State)
                        Success = PLC.CommManager.SetPLCValue(_ReadyForData_Address, _ReadyForData);
                }

            }
        }

        #endregion

        #region ReadyToTest Region

        private string _ReadyToTest_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.ReadyToTest_Adrs}";
        private bool _ReadyToTest = false;
        public bool ReadyToTest
        {
            get
            {
                return _ReadyToTest;
            }
            set
            {
                if (_ReadyToTest != value)
                {
                    _ReadyToTest = value;
                    if (PrimaryVariables.MasterLiveDataMode.State)
                        Success = PLC.CommManager.SetPLCValue(_ReadyToTest_Address, _ReadyToTest);
                }

            }
        }

        #endregion

        #region TestInProcess Region

        private string _TestInProcess_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.TestInProcess_Adrs}";
        private bool _TestInProcess = false;
        public bool TestInProcess
        {
            get
            {
                return _TestInProcess;
            }
            set
            {
                if (_TestInProcess != value)
                {
                    _TestInProcess = value;
                    if (PrimaryVariables.MasterLiveDataMode.State)
                        Success = PLC.CommManager.SetPLCValue(_TestInProcess_Address, _TestInProcess);
                }

            }
        }

        #endregion

        #region FetchingFromServer Region

        private string _FetchingFromServer_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.FetchingFromServer_Adrs}";
        private bool _FetchingFromServer = false;
        public bool FetchingFromServer
        {
            get
            {
                return _FetchingFromServer;
            }
            set
            {
                if (_FetchingFromServer != value)
                {
                    _FetchingFromServer = value;
                    if (PrimaryVariables.MasterLiveDataMode.State)
                        Success = PLC.CommManager.SetPLCValue(_FetchingFromServer_Address, _FetchingFromServer);
                }

            }
        }

        #endregion

        #region TestComplete Region

        private string _TestComplete_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.TestComplete_Adrs}";
        private bool _TestComplete = false;
        public bool TestComplete
        {
            get
            {
                return _TestComplete;
            }
            set
            {
                if (_TestComplete != value)
                {
                    _TestComplete = value;
                    if (PrimaryVariables.MasterLiveDataMode.State)
                        Success = PLC.CommManager.SetPLCValue(_TestComplete_Address, _TestComplete);
                }

            }
        }

        #endregion

        #region TestFailedToComplete Region

        private string _TestFailedToComplete_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.TestFailedToComplete_Adrs}";
        private bool _TestFailedToComplete = false;
        public bool TestFailedToComplete
        {
            get
            {
                return _TestFailedToComplete;
            }
            set
            {
                if (_TestFailedToComplete != value)
                {
                    _TestFailedToComplete = value;
                    if (PrimaryVariables.MasterLiveDataMode.State)
                        Success = PLC.CommManager.SetPLCValue(_TestFailedToComplete_Address, _TestFailedToComplete);
                }

            }
        }

        #endregion

        #region ErrorMsg Region

        private string _ErrorMsg_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.ErrorMsg_Adrs}";
        private string _ErrorMsg = "Not Init.";
        public string ErrorMsg
        {
            get
            {
                return _ErrorMsg;
            }
            set
            {
                if (_ErrorMsg != value)
                {
                    _ErrorMsg = value;
                    if (PrimaryVariables.MasterLiveDataMode.State)
                        Success = PLC.CommManager.SetPLCValue(_ErrorMsg_Address, _ErrorMsg);
                }

            }
        }

        #endregion

        #region ErrorCode Region

        private string _ErrorCode_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.ErrorCode_Adrs}";
        private string _ErrorCode = "Not Init.";
        public string ErrorCode
        {
            get
            {
                return _ErrorCode;
            }
            set
            {
                if (_ErrorCode != value)
                {
                    _ErrorCode = value;
                    if (PrimaryVariables.MasterLiveDataMode.State)
                        Success = PLC.CommManager.SetPLCValue(_ErrorCode_Address, _ErrorCode);
                }

            }
        }

        #endregion

        #region Faulted Region

        private string _Faulted_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.Faulted_Adrs}";
        private bool _Faulted = false;
        public bool Faulted
        {
            get
            {
                return _Faulted;
            }
            set
            {
                if (_Faulted != value)
                {
                    _Faulted = value;
                    if (PrimaryVariables.MasterLiveDataMode.State)
                        Success = PLC.CommManager.SetPLCValue(_Faulted_Address, _Faulted);
                }

            }
        }

        #endregion

        #region ActualCoreLoss Region

        private string _ActualCoreLoss_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.ActualCoreLoss_Adrs}";
        private double _ActualCoreLoss = -999.111;
        public double ActualCoreLoss
        {
            get
            {
                return _ActualCoreLoss;
            }
            set
            {
                if (_ActualCoreLoss != value)
                {
                    _ActualCoreLoss = value;
                    if (PrimaryVariables.MasterLiveDataMode.State)
                        Success = PLC.CommManager.SetPLCValue(_ActualCoreLoss_Address, _ActualCoreLoss);
                }

            }
        }

        #endregion

        #region ActualAmpTurns Region

        private string _ActualAmpTurns_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.ActualAmpTurns_Adrs}";
        private double _ActualAmpTurns = -999.222;
        public double ActualAmpTurns
        {
            get
            {
                return _ActualAmpTurns;
            }
            set
            {
                if (_ActualAmpTurns != value)
                {
                    _ActualAmpTurns = value;
                    if (PrimaryVariables.MasterLiveDataMode.State)
                        Success = PLC.CommManager.SetPLCValue(_ActualAmpTurns_Address, _ActualCoreLoss);
                }

            }
        }

        #endregion

        #region isRepo Region

        private string _isRepo_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.IsRepo_Adrs}";
        private bool _isRepo = false;
        public bool isRepo
        {
            get
            {
                return _isRepo;
            }
            set
            {
                if (_isRepo != value)
                {
                    _isRepo = value;
                    if (PrimaryVariables.MasterLiveDataMode.State)
                        Success = PLC.CommManager.SetPLCValue(_isRepo_Address, _isRepo);
                }

            }
        }

        #endregion

        #region CoreGrade Region

        private string _CoreGrade_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.CoreGrade_Adrs}";
        private Int32 _CoreGrade = -99;
        public Int32 CoreGrade
        {
            get
            {
                return _CoreGrade;
            }
            set
            {
                if (_CoreGrade != value)
                {
                    _CoreGrade = value;
                    if (PrimaryVariables.MasterLiveDataMode.State)
                        Success = PLC.CommManager.SetPLCValue(_CoreGrade_Address, _CoreGrade);
                }

            }
        }

        #endregion

        #region ServerResult Region

        private string _ServerResult_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.ServerResult_Adrs}";
        private Int32 _ServerResult = -99;
        public Int32 ServerResult
        {
            get
            {
                return _ServerResult;
            }
            set
            {
                if (_ServerResult != value)
                {
                    _ServerResult = value;
                    if (PrimaryVariables.MasterLiveDataMode.State)
                        Success = PLC.CommManager.SetPLCValue(_ServerResult_Address, _ServerResult);
                }

            }
        }

        #endregion


        #region HeartbeatEcho Region

        private string _HeartbeatEcho_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.HeartbeatEcho_Adrs}";
        private bool _HeartbeatEcho = false;
        public bool HeartbeatEcho
        {
            get
            {
                return _HeartbeatEcho;
            }
            set
            {
                if (_HeartbeatEcho != value)
                {
                    _HeartbeatEcho = value;
                    if (PrimaryVariables.MasterLiveDataMode.State)
                        Success = PLC.CommManager.SetPLCValue(_HeartbeatEcho_Address, _HeartbeatEcho);
                }

            }
        }

        #endregion

        #region InAuto Region

        private string _InAuto_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.InAuto_Adrs}";
        private bool _InAuto = false;
        public bool InAuto
        {
            get
            {
                return _InAuto;
            }
            set
            {
                if (_InAuto != value)
                {
                    _InAuto = value;
                    if (PrimaryVariables.MasterLiveDataMode.State)
                        Success = PLC.CommManager.SetPLCValue(_InAuto_Address, _InAuto);
                }

            }
        }

        #endregion


    }

    public static class MyPLC
    {

        public static CommDefinition_Comms Comm = new CommDefinition_Comms();

        public static CommDefinition_Input FromPLC = new CommDefinition_Input();

        public static CommDefinition_Output ToPLC = new CommDefinition_Output();




    }

    public static class myPLCTools
    {
        private static DispatcherTimer UpdateTmr = new DispatcherTimer();
        private static DispatcherTimer HeartBeat_Late_Tmr = new DispatcherTimer();
        private static DispatcherTimer HeartBeat_Pulse_Tmr = new DispatcherTimer();

        public static bool CommEnabled = false;
        public static BoolWithEventStatus HeartBeat = new BoolWithEventStatus();

        public static bool Initialize(string _path, string _CommFile, string _InputFile, string _OutputFile)
        {
            CommEnabled = true;

            PrimaryVariables.MasterLiveDataMode.State = false;
            if (!LoadConfig(_path, _CommFile, _InputFile, _OutputFile))
                SaveConfig(_path, _CommFile, _InputFile, _OutputFile);


            MyPLC.Comm.IsOnline = Core.PingTest_byIPAddress(MyPLC.Comm.IPAddress);

            if (MyPLC.Comm.IsOnline && CommManager.Channels.ChannelsList.Count == 0)
                CommManager.Channels.AddChannel(new
                    CommManager.Channel(MyPLC.Comm.DriverType, MyPLC.Comm.Name, MyPLC.Comm.IPAddress, MyPLC.Comm.SlotNum, MyPLC.Comm.Timeout, MyPLC.Comm.CPUType));
            else
                return false;

            PrimaryVariables.MasterLiveDataMode.State = true;

            HeartBeat_Pulse_Tmr.Interval = TimeSpan.FromMilliseconds(MyPLC.Comm.HeartBeatInterval_ms);
            HeartBeat_Pulse_Tmr.Start();
            HeartBeat_Pulse_Tmr.Tick += HeartBeat_Pulse_Tmr_Tick;

            HeartBeat_Late_Tmr.Interval = TimeSpan.FromMilliseconds(MyPLC.Comm.HeartBeatMonitorTime_ms);
            HeartBeat_Late_Tmr.Start();
            HeartBeat_Late_Tmr.Tick += HeartBeat_Late_Tmr_Tick;


            UpdateTmr.Interval = TimeSpan.FromMilliseconds(MyPLC.Comm.UpdateInterval_ms);
            UpdateTmr.Tick += UpdateTmr_Tick;
            HeartBeat.Status += Heartbeat_Changed;

            PrimaryVariables.MasterLiveDataMode.Status += MasterLiveDataMode_Status;


            return true;
        }

        public static bool CommShutDown()
        {
            try
            {
                CommEnabled = false;
                MyPLC.Comm.IsOnline = false;
                PrimaryVariables.MasterLiveDataMode.State = false;
                HeartBeat_Late_Tmr.Stop();
                HeartBeat_Pulse_Tmr.Stop();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static DispatcherTimer CommRetry_Tmr = new DispatcherTimer();
        private static void MasterLiveDataMode_Status(object sender, EventArgs e)
        {
            if (!PrimaryVariables.MasterLiveDataMode.State && CommEnabled)
            {
                CommRetry_Tmr.Interval = TimeSpan.FromSeconds(15);
                CommRetry_Tmr.Start();
                CommRetry_Tmr.Tick += CommRetry_Tmr_Tick;
            }
        }

        private static void CommRetry_Tmr_Tick(object sender, EventArgs e)
        {
            if (CommEnabled)
            {
                bool CommCheck = Core.PingTest_byIPAddress(MyPLC.Comm.IPAddress);
                if (CommCheck)
                {
                    MyPLC.Comm.IsOnline = true;
                    PrimaryVariables.MasterLiveDataMode.State = true;
                    HeartBeat_Late_Tmr.Start();
                    HeartBeat_Pulse_Tmr.Start();
                    bool HeartCheck = MyPLC.FromPLC.Heartbeat;
                }
            }
        }

        private static void UpdateTmr_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private static string _Heartbeat_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.Heartbeat_Adrs}";
        private static void HeartBeat_Pulse_Tmr_Tick(object sender, EventArgs e)
        {
            if (CommEnabled)
            {
                MyPLC.ToPLC.HeartbeatEcho = !MyPLC.ToPLC.HeartbeatEcho;
                MyPLC.FromPLC.Heartbeat = true;

                if (PrimaryVariables.MasterLiveDataMode.State)
                {
                    bool Success = false;
                    HeartBeat.State = PLC.CommManager.GetPLCValue_BOOL(_Heartbeat_Address, out Success);
                   
                }
              

            }
        }

        private static void Heartbeat_Changed(object sender, EventArgs e)
        {
            if (CommEnabled)
            {
                HeartBeat_Late_Tmr.Stop();
                HeartBeat_Late_Tmr.Start();
            }
        }



        private static void HeartBeat_Late_Tmr_Tick(object sender, EventArgs e)
        {
            if (CommEnabled)
            {
                //MyPLC.Comm.IsOnline = false;
                //PrimaryVariables.MasterLiveDataMode.State = false;
                //HeartBeat_Late_Tmr.Stop();
                //HeartBeat_Pulse_Tmr.Stop();

            }
        }

        public static bool SaveConfig(string _path, string _CommFile, string _InputFile, string _OutputFile)
        {
            try
            {
                if (!Directory.Exists(_path))
                    Directory.CreateDirectory(_path);
                PrimaryVariables.MasterLiveDataMode.State = false;

                XMLTools.ToXML<CommDefinition_Comms>(MyPLC.Comm, _path + _CommFile);
                XMLTools.ToXML<CommDefinition_Input>(MyPLC.FromPLC, _path + _InputFile);
                XMLTools.ToXML<CommDefinition_Output>(MyPLC.ToPLC, _path + _OutputFile);
                PrimaryVariables.MasterLiveDataMode.State = false;
                return true;
            }
            catch { return false; }

        }
        public static bool LoadConfig(string _path, string _CommFile, string _InputFile, string _OutputFile)
        {
            if (File.Exists(_path + _CommFile) && File.Exists(_path + _InputFile) && File.Exists(_path + _OutputFile))
            {
                try
                {
                    MyPLC.Comm = XMLTools.FromXML<CommDefinition_Comms>(_path + _CommFile);
                    MyPLC.FromPLC = XMLTools.FromXML<CommDefinition_Input>(_path + _InputFile);
                    MyPLC.ToPLC = XMLTools.FromXML<CommDefinition_Output>(_path + _OutputFile);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }


}
