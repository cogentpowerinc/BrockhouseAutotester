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


        private string _IPAddress = "192.168.1.244";
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
                _SerialNumber = PLC.CommManager.GetPLCValue_STRING(_SerialNumber_Address, out Success);
                return _SerialNumber;
            }
            set
            {
                _SerialNumber = value;

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
        private string _Heartbeat_Address = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.Heartbeat_Adrs}";
        private bool _Heartbeat = false;
        public event EventHandler HeartBeat_Changed;
        protected virtual void HeartBeatStatusUpdate()
        {
            if (HeartBeat_Changed != null) HeartBeat_Changed(this, EventArgs.Empty);
        }
        public bool Heartbeat
        {
            get
            {
                _Heartbeat = PLC.CommManager.GetPLCValue_BOOL(_Heartbeat_Address, out Success);
                if (_Heartbeat != Heartbeat)
                    HeartBeatStatusUpdate();
               
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
                    Success = PLC.CommManager.SetPLCValue(_ReadyToTest_Address, _ReadyToTest);
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
                    Success = PLC.CommManager.SetPLCValue(_ActualAmpTurns_Address, _ActualCoreLoss);
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

    static class myPLCTools
    {
        private static DispatcherTimer UpdateTmr = new DispatcherTimer();
        private static DispatcherTimer HeartBeat_Late_Tmr = new DispatcherTimer();
        private static bool PLCHeartbeat = false;
        private static bool PLCHeartbeat_Last = false;
        public static void ElectroCardiogramMachine()
        {
            if (MyPLC.Comm.IsOnline)
            {
                bool PLCHeartbeat = MyPLC.FromPLC.Heartbeat;
                
            }
        }
        public static bool Initialize(string _path, string _CommFile, string _InputFile, string _OutputFile)
        {
            if (LoadConfig(_path,_CommFile,_InputFile,_OutputFile))
                SaveConfig(_path, _CommFile, _InputFile, _OutputFile);

           
            MyPLC.Comm.IsOnline = Core.PingTest_byIPAddress(MyPLC.Comm.IPAddress);

            if (MyPLC.Comm.IsOnline && CommManager.Channels.ChannelsList.Count==0)
                CommManager.Channels.AddChannel(new
                    CommManager.Channel(MyPLC.Comm.DriverType, MyPLC.Comm.Name, MyPLC.Comm.IPAddress, MyPLC.Comm.SlotNum, MyPLC.Comm.Timeout, MyPLC.Comm.CPUType));
            else
                return false;
     
            HeartBeat_Late_Tmr.Interval = TimeSpan.FromSeconds(5);
            HeartBeat_Late_Tmr.Start();
            HeartBeat_Late_Tmr.Tick += HeartBeat_Late_Tmr_Tick;
            UpdateTmr.Interval = TimeSpan.FromMilliseconds(MyPLC.Comm.UpdateInterval_ms);
            UpdateTmr.Tick += UpdateTmr_Tick;
            MyPLC.FromPLC.HeartBeat_Changed += FromPLC_HeartBeat_Changed;

            return true;
        }

        private static void FromPLC_HeartBeat_Changed(object sender, EventArgs e)
        {
            HeartBeat_Late_Tmr.Start();
        }

        private static void UpdateTmr_Tick(object sender, EventArgs e)
        {
            MyPLC.ToPLC.HeartbeatEcho = !MyPLC.ToPLC.HeartbeatEcho;
        }

        private static void HeartBeat_Late_Tmr_Tick(object sender, EventArgs e)
        {
            MyPLC.Comm.IsOnline = false;
            HeartBeat_Late_Tmr.Stop();
           
        }

        public static bool SaveConfig(string _path, string _CommFile, string _InputFile, string _OutputFile)
        {
            try
            {
                if (!Directory.Exists(_path))
                    Directory.CreateDirectory(_path);

                
                XMLTools.ToXML<CommDefinition_Comms>(MyPLC.Comm, _path + _CommFile);
                XMLTools.ToXML<CommDefinition_Input>(MyPLC.FromPLC, _path + _InputFile);
                XMLTools.ToXML<CommDefinition_Output>(MyPLC.ToPLC, _path + _OutputFile);
                return true;
            }
            catch { return  false; }

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
