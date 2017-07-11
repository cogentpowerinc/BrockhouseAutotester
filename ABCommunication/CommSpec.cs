using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCommunication
{
    public static class CommSpec
    {
        public static string Name { get; set; } = "AB_1";
        public static string IPAddress { get; set; } = "192.168.1.244";
        public static string SlotNum { get; set; } = "0";
        public static Int32 Timeout { get; set; } = 5000;


    }

    public static class FromPLC
    {
        public static string SerialNumber { get; set; } = "Not Init.";
        public static string SerialNumber_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.SerialNumber_Adrs}";
        public static double Weight { get; set; } = -1;
        public static string Weight_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.Weight_Adrs}";
        public static double Temp { get; set; } = -1;
        public static string Temp_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.Temp_Adrs}";
        public static bool InitTest { get; set; } = false;
        public static string InitTest_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.InitTest_Adrs}";
        public static bool Reset { get; set; } = false;
        public static string Reset_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.Reset_Adrs}";
        public static Int32 ResultResponse { get; set; } = -99;
        public static string ResultResponse_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.ResultResponse_Adrs}";
        public static bool AutoMode { get; set; } = false;
        public static string AutoMode_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.AutoMode_Adrs}";
        public static bool Heartbeat { get; set; } = false;
        public static string Heartbeat_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.FromCogent.Handshakde_Adrs}";

    }

    public static class ToPLC
    {
        public static bool ReadyForData { get; set; } = false;
        public static string ReadyForData_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.ReadyForData_Adrs}";
        public static bool ReadyToTest { get; set; } = false;
        public static string ReadyToTest_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.ReadyToTest_Adrs}";
        public static bool FetchingFromServer { get; set; } = false;
        public static string FetchingFromServer_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.FetchingFromServer_Adrs}";
        public static bool TestInProgress { get; set; } = false;
        public static string TestInProgress_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.TestInProgress_Adrs}";
        public static bool TestComplete { get; set; } = false;
        public static string TestComplete_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.TestComplete_Adrs}";
        public static bool TestFailedToComplete { get; set; } = false;
        public static string TestFailedToComplete_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.TestFailedToComplete_Adrs}";
        public static string ErrorMsg { get; set; } = "Not Init.";
        public static string ErrorMsg_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.ErrorMsg_Adrs}";
        public static string ErrorCode { get; set; } = "Not Init.";
        public static string ErrorCode_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.ErrorCode_Adrs}";
        public static bool Faulted { get; set; } = false;
        public static string Faulted_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.Faulted_Adrs}";
        public static double ActualCoreLoss { get; set; } = -99;
        public static string ActualCoreLoss_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.Weight_Adrs}";
        public static double ActualAmpTurns { get; set; } = -99;
        public static string ActualAmpTurns_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.Weight_Adrs}";
        public static Int32 ServerResult { get; set; } = -99;
        public static string ServerResult_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.Weight_Adrs}";
       
        public static string HeartbeatEcho_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.Weight_Adrs}";
        public static bool InAuto { get; set; } = false;
        public static string InAuto_Address { get; set; } = "{Ch_AT_1_Ch}{Adrs_PROGRAM:Tester.BrockhouseInterface.ToCogent.Weight_Adrs}";
    }

    public static class AutoTesterInterface
    {
        public static bool PeekPLC()
        {
            bool Success = true;
            if (Success) FromPLC.SerialNumber = PLC.CommManager.GetPLCValue_STRING(FromPLC.SerialNumber_Address, out Success);
            if (Success) FromPLC.Weight = PLC.CommManager.GetPLCValue_DOUBLE(FromPLC.Weight_Address, out Success);
            if (Success) FromPLC.Temp = PLC.CommManager.GetPLCValue_DOUBLE(FromPLC.Temp_Address, out Success);
            if (Success) FromPLC.InitTest = PLC.CommManager.GetPLCValue_BOOL(FromPLC.InitTest_Address, out Success);
            if (Success) FromPLC.Reset = PLC.CommManager.GetPLCValue_BOOL(FromPLC.Reset_Address, out Success);
            if (Success) FromPLC.ResultResponse = PLC.CommManager.GetPLCValue_INT32(FromPLC.ResultResponse_Address, out Success);
            if (Success) FromPLC.AutoMode = PLC.CommManager.GetPLCValue_BOOL(FromPLC.AutoMode_Address, out Success);
            if (Success) FromPLC.Heartbeat = PLC.CommManager.GetPLCValue_BOOL(FromPLC.Heartbeat_Address, out Success);

            return Success;
        }

        public static bool PokePLC()
        {
            bool Success = true;
            if (Success) Success = PLC.CommManager.SetPLCValue(ToPLC.ReadyForData_Address, ToPLC.ReadyForData);
            if (Success) Success = PLC.CommManager.SetPLCValue(ToPLC.ReadyToTest_Address, ToPLC.ReadyToTest);
            if (Success) Success = PLC.CommManager.SetPLCValue(ToPLC.FetchingFromServer_Address, ToPLC.FetchingFromServer);
            if (Success) Success = PLC.CommManager.SetPLCValue(ToPLC.TestInProgress_Address, ToPLC.TestInProgress);
            if (Success) Success = PLC.CommManager.SetPLCValue(ToPLC.TestComplete_Address, ToPLC.TestComplete);
            if (Success) Success = PLC.CommManager.SetPLCValue(ToPLC.TestFailedToComplete_Address, ToPLC.TestFailedToComplete);
            if (Success) Success = PLC.CommManager.SetPLCValue(ToPLC.ErrorMsg_Address, ToPLC.ErrorMsg);
            if (Success) Success = PLC.CommManager.SetPLCValue(ToPLC.ErrorCode_Address, ToPLC.ErrorCode);
            if (Success) Success = PLC.CommManager.SetPLCValue(ToPLC.Faulted_Address, ToPLC.Faulted);
            if (Success) Success = PLC.CommManager.SetPLCValue(ToPLC.ActualCoreLoss_Address, ToPLC.ActualCoreLoss);
            if (Success) Success = PLC.CommManager.SetPLCValue(ToPLC.ActualAmpTurns_Address, ToPLC.ActualAmpTurns);
            if (Success) Success = PLC.CommManager.SetPLCValue(ToPLC.ServerResult_Address, ToPLC.ServerResult);
            if (Success) Success = PLC.CommManager.SetPLCValue(ToPLC.InAuto_Address, ToPLC.InAuto);
            if (Success) Success = PLC.CommManager.SetPLCValue(ToPLC.HeartbeatEcho_Address, !FromPLC.Heartbeat);
           

            return Success;
        }

    }

}
