using System;
using System.Collections.Generic;
using Logix;
using System.Collections.ObjectModel;
using Common;

namespace ABDriver
{
    /// <summary>
    /// 
    /// </summary>
    public class CLX_Communications
    {

        #region Global Variables

        /// <summary>
        /// The comm group
        /// </summary>
        private static List<TagGroup> CommGroup = new List<TagGroup>();
        /// <summary>
        /// The ab pl cs
        /// </summary>
        private static List<Controller> AB_PLCs = new List<Controller>();
        /// <summary>
        /// The tags loaded
        /// </summary>
        private static bool[] TagsLoaded = new bool[] { false, false, false, false, false, false, false, false, false, false };
        /// <summary>
        /// The components
        /// </summary>
        private System.ComponentModel.IContainer components = null;



        #endregion

        #region Public Functions

        /// <summary>
        /// Finds the type of the data.
        /// </summary>
        /// <param name="_tag">The tag.</param>
        /// <param name="_DataType">Type of the data.</param>
        /// <returns></returns>
        private static Tag FindDataType(Tag _tag, ABTagClass _DataType)
        {

            switch (_DataType)
            {
                case ABTagClass.BOOL:
                    _tag.NetType = typeof(System.Boolean);
                    break;
                case ABTagClass.SINT:
                    _tag.NetType = typeof(System.SByte);
                    break;
                case ABTagClass.INT:
                    _tag.NetType = typeof(System.Int16);
                    break;
                case ABTagClass.DINT:
                    _tag.NetType = typeof(System.Int32);
                    break;
                case ABTagClass.REAL:
                    _tag.NetType = typeof(System.Single);
                    break;
                case ABTagClass.STRING:
                    _tag.NetType = typeof(System.String);
                    break;
                case ABTagClass.LINT:
                    _tag.NetType = typeof(System.Int64);
                    break;
                case ABTagClass.OBJECT:
                    _tag.NetType = typeof(System.Object);
                    break;
                default:
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to decode Tag Type.");
                    }
                    break;
            }
            return _tag;
        }

        /// <summary>
        /// Initialize Communication to PLC on defined Channel
        /// </summary>
        /// <param name="_channel">The channel.</param>
        /// <param name="_IPAddress">The ip address.</param>
        /// <param name="_slot">The slot.</param>
        /// <param name="_timeout">The timeout.</param>
        /// <returns>
        /// bool. True if Connection is made.
        /// </returns>
        public static Int32 CommInit(string _IPAddress, string _slot, int _timeout)
        {
            AB_PLCs.Add(new Controller());
            CommGroup.Add(new TagGroup());

            Int32 _channel = AB_PLCs.Count - 1;
            
            AB_PLCs[_channel].IPAddress = _IPAddress;
            AB_PLCs[_channel].Path = _slot;
            AB_PLCs[_channel].Timeout = _timeout;
            AB_PLCs[_channel].CPUType = Controller.CPU.LOGIX;

            if (ResultCode.E_SUCCESS == AB_PLCs[_channel].Connect())
            {
                CommGroup[_channel].Controller = AB_PLCs[_channel];
                CommGroup[_channel].ScanStart();
            }
            else
            {
                DebugTools.UpdateActionList("AB Driver, Unable to Establish Comms.");
            }
            return _channel;

        }

        /// <summary>
        /// Initialize Data Write to PLC on defined Channel at an address
        /// </summary>
        /// <param name="_channel">The channel.</param>
        /// <param name="_address">The address.</param>
        /// <param name="_value">The value.</param>
        /// <param name="_DataType">Type of the data.</param>
        /// <returns>
        /// bool. True if write is successful.
        /// </returns>
        public static bool PLC_Write(int _channel, string _address, string _value, ABTagClass _DataType)
        {
            bool Complete = false;

            if (PrimaryVariables.MasterLiveDataMode.State)
            {
                Tag MyTag = new Tag();

                try
                {
                    /////////////////////////////////////////
                    // set Tag properties
                    MyTag.Name = _address;
                    MyTag.Active = true;
                    MyTag = FindDataType(MyTag, _DataType);

                    MyTag.Value = _value;

                    ///////////////////////////////
                    /// write tag and check for results
                    /// 
                    if (AB_PLCs[_channel].WriteTag(MyTag) == ResultCode.E_SUCCESS)
                        Complete = true;

                    if (ResultCode.QUAL_GOOD != MyTag.QualityCode)
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to write Data.");
                        PrimaryVariables.ABDataWriteErrors += 1;
                    }
                }
                catch (Exception ex)
                {
                    DebugTools.UpdateActionList("AB Driver, "+ex.Message);
                    PrimaryVariables.ABDataWriteErrors += 1;
                }
            }
            return Complete;
        }


        /// <summary>
        /// Initialize Data Read From PLC on defined Channel at an address
        /// </summary>
        /// <param name="_channel">The _channel.</param>
        /// <param name="_address">The address.</param>
        /// <param name="_DataType">Type of the _ data.</param>
        /// <returns>
        /// String value of the read data.
        /// </returns>
        public static string PLC_Read(int _channel, string _address, ABTagClass _DataType)
        {
            string Value = "";

            if (PrimaryVariables.MasterLiveDataMode.State)
            {
                Tag MyTag = new Tag();

                MyTag = FindDataType(MyTag, _DataType);

                MyTag.Name = _address;
                MyTag.Length = 1;

                // Check Tag Read Stats
                int ReadResponse = AB_PLCs[_channel].ReadTag(MyTag);
                if (ReadResponse != ResultCode.E_SUCCESS)
                {
                    DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                    PrimaryVariables.ABDataReadErrors += 1;
                }

                if (ResultCode.QUAL_GOOD == MyTag.QualityCode)
                    Value = MyTag.Value.ToString();
                else
                {
                    DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                    PrimaryVariables.ABDataReadErrors += 1;
                }
            }
            return Value;
        }

        /// <summary>
        /// PLCs the read specific.
        /// </summary>
        /// <param name="_address">The address.</param>
        /// <param name="_channelNum">The channel number.</param>
        /// <param name="_value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static bool PLC_Read_Specific(string _address, int _channelNum, out bool _value)
        {
            _value = false;
            bool xSuccess = false;

            if (PrimaryVariables.MasterLiveDataMode.State)
            {
                Tag MyTag = new Tag();

                string MyAddress = Core.FindTagAddress(_address);
                //ABTagClass rawTagClass = Core.FindABTagClass(_address);
                if ((_channelNum != 99) && (MyAddress != "NotFound")/* && (rawTagClass != ABTagClass.NotFound)*/)
                {
                    MyTag.NetType = typeof(System.Boolean); 

                    MyTag.Name = MyAddress;
                    MyTag.Length = 1;

                    // Check Tag Read Stats

                    int ReadResponse = AB_PLCs[_channelNum].ReadTag(MyTag);
                    if (ReadResponse != ResultCode.E_SUCCESS)
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                        PrimaryVariables.ABDataReadErrors += 1;
                        if (PrimaryVariables.ABDataReadErrors > 3)
                            PrimaryVariables.MasterLiveDataMode.State = false;
                    }
                    if (ResultCode.QUAL_GOOD == MyTag.QualityCode)
                    {
                        _value = Convert.ToBoolean(MyTag.Value);
                        xSuccess = true;
                    }
                    else
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                        PrimaryVariables.ABDataReadErrors += 1;
                        if (PrimaryVariables.ABDataReadErrors > 3)
                            PrimaryVariables.MasterLiveDataMode.State = false;
                    }
                }
                else
                {
                    DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                    PrimaryVariables.ABDataReadErrors += 1;
                }
            }
            return xSuccess;

        }
        /// <summary>
        /// PLCs the read specific.
        /// </summary>
        /// <param name="_address">The address.</param>
        /// <param name="_channelNum">The channel number.</param>
        /// <param name="_value">The value.</param>
        /// <returns></returns>
        public static bool PLC_Read_Specific(string _address, int _channelNum, out string _value)
        {
            _value = "";
            bool xSuccess = false;

            if (PrimaryVariables.MasterLiveDataMode.State)
            {
                Tag MyTag = new Tag();


                string MyAddress = Core.FindTagAddress(_address);
              //  ABTagClass rawTagClass = Core.FindABTagClass(_address);
                if ((_channelNum != 99) && (MyAddress != "NotFound")/* && (rawTagClass != ABTagClass.NotFound)*/)
                {
                    MyTag.NetType = typeof(System.String);
                 //   MyTag = FindDataType(MyTag, rawTagClass);

                    MyTag.Name = MyAddress;
                    MyTag.Length = 1;

                    // Check Tag Read Stats

                    int ReadResponse = AB_PLCs[_channelNum].ReadTag(MyTag);
                    if (ReadResponse != ResultCode.E_SUCCESS)
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                        PrimaryVariables.ABDataReadErrors += 1;
                        if (PrimaryVariables.ABDataReadErrors > 3)
                            PrimaryVariables.MasterLiveDataMode.State = false;
                    }
                    if (ResultCode.QUAL_GOOD == MyTag.QualityCode)
                    {
                        _value = MyTag.Value.ToString();
                        xSuccess = true;
                    }
                    else
                    {
                        _value = "Unable to Read From PLC";
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                        PrimaryVariables.ABDataReadErrors += 1;
                        if (PrimaryVariables.ABDataReadErrors > 3)
                            PrimaryVariables.MasterLiveDataMode.State = false;
                    }
                }
                else
                {
                    _value = "Configuration Issue";
                    DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                }
            }
            else
            {
                _value = "PLC Offline ?";
            }
            return xSuccess;

        }
        /// <summary>
        /// PLCs the read specific.
        /// </summary>
        /// <param name="_address">The address.</param>
        /// <param name="_channelNum">The channel number.</param>
        /// <param name="_value">The value.</param>
        /// <returns></returns>
        public static bool PLC_Read_Specific(string _address, int _channelNum, out Int32 _value)
        {
            _value = 0;
            bool xSuccess = false;

            if (PrimaryVariables.MasterLiveDataMode.State)
            {
                Tag MyTag = new Tag();


                string MyAddress = Core.FindTagAddress(_address);
           //     ABTagClass rawTagClass = Core.FindABTagClass(_address);
                if ((_channelNum != 99) && (MyAddress != "NotFound") /*&& (rawTagClass != ABTagClass.NotFound)*/)
                {
                    MyTag.NetType = typeof(System.Int32);
                 //   MyTag = FindDataType(MyTag, rawTagClass);

                    MyTag.Name = MyAddress;
                    MyTag.Length = 1;

                    // Check Tag Read Stats

                    int ReadResponse = AB_PLCs[_channelNum].ReadTag(MyTag);
                    if (ReadResponse != ResultCode.E_SUCCESS)
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                        PrimaryVariables.ABDataReadErrors += 1;
                        if (PrimaryVariables.ABDataReadErrors > 3)
                            PrimaryVariables.MasterLiveDataMode.State = false;
                    }
                    if (ResultCode.QUAL_GOOD == MyTag.QualityCode)
                    {
                        _value = Convert.ToInt32(MyTag.Value);
                        xSuccess = true;
                    }
                    else
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                        PrimaryVariables.ABDataReadErrors += 1;
                        if (PrimaryVariables.ABDataReadErrors > 3)
                            PrimaryVariables.MasterLiveDataMode.State = false;
                    }
                }
                else
                {
                    DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                }
            }
            return xSuccess;

        }
        /// <summary>
        /// PLCs the read specific.
        /// </summary>
        /// <param name="_address">The address.</param>
        /// <param name="_channelNum">The channel number.</param>
        /// <param name="_value">The value.</param>
        /// <returns></returns>
        public static bool PLC_Read_Specific(string _address, int _channelNum, out double _value)
        {
            _value = 0;
            bool xSuccess = false;

            if (PrimaryVariables.MasterLiveDataMode.State)
            {
                Tag MyTag = new Tag();


                string MyAddress = Core.FindTagAddress(_address);
            //    ABTagClass rawTagClass = Core.FindABTagClass(_address);
                if ((_channelNum != 99) && (MyAddress != "NotFound")/* && (rawTagClass != ABTagClass.NotFound)*/)
                {
                    MyTag.NetType = typeof(System.Single);
                 //   MyTag = FindDataType(MyTag, rawTagClass);

                    MyTag.Name = MyAddress;
                    MyTag.Length = 1;

                    // Check Tag Read Stats

                    int ReadResponse = AB_PLCs[_channelNum].ReadTag(MyTag);
                    if (ReadResponse != ResultCode.E_SUCCESS)
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                        PrimaryVariables.ABDataReadErrors += 1;
                        if (PrimaryVariables.ABDataReadErrors > 3)
                            PrimaryVariables.MasterLiveDataMode.State = false;
                    }
                    if (ResultCode.QUAL_GOOD == MyTag.QualityCode)
                    {
                        //_value = Convert.ToDouble(MyTag.Value);
                        _value = double.Parse(MyTag.Value.ToString());
                        xSuccess = true;
                    }
                    else
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                        PrimaryVariables.ABDataReadErrors += 1;
                        if (PrimaryVariables.ABDataReadErrors > 3)
                            PrimaryVariables.MasterLiveDataMode.State = false;
                    }
                }
                else
                {
                    DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                }
            }
            return xSuccess;

        }

        /// <summary>
        /// PLCs the write specific.
        /// </summary>
        /// <param name="_address">The address.</param>
        /// <param name="_channelNum">The channel number.</param>
        /// <param name="_value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static bool PLC_Write_Specific(string _address, int _channelNum, bool _value)
        {
            bool Complete = false;

            if (PrimaryVariables.MasterLiveDataMode.State)
            {
                Tag MyTag = new Tag();

                string MyAddress = Core.FindTagAddress(_address);
           //     ABTagClass rawTagClass = Core.FindABTagClass(_address);

                if ((_channelNum != 99) && (MyAddress != "NotFound") /*&& (rawTagClass != ABTagClass.NotFound)*/)
                {


                    try
                    {
                        /////////////////////////////////////////
                        // set Tag properties
                        MyTag.Name = MyAddress;
                        MyTag.Active = true;
                        MyTag.NetType = typeof(System.Boolean);
                   //     MyTag = FindDataType(MyTag, rawTagClass);

                        MyTag.Value = _value;

                        ///////////////////////////////
                        /// write tag and check for results
                        /// 

                        if (AB_PLCs[_channelNum].WriteTag(MyTag) == ResultCode.E_SUCCESS)
                            Complete = true;

                        if (ResultCode.QUAL_GOOD != MyTag.QualityCode)
                        {
                            DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                            PrimaryVariables.ABDataWriteErrors += 1;
                            if (PrimaryVariables.ABDataReadErrors > 3)
                                PrimaryVariables.MasterLiveDataMode.State = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data, "+ex.Message);
                        PrimaryVariables.ABDataWriteErrors += 1;
                        if (PrimaryVariables.ABDataReadErrors > 3)
                            PrimaryVariables.MasterLiveDataMode.State = false;
                    }
                }
                else
                {
                    DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                    PrimaryVariables.ABDataWriteErrors += 1;
                    if (PrimaryVariables.ABDataReadErrors > 3)
                        PrimaryVariables.MasterLiveDataMode.State = false;
                }
            }
            return Complete;

        }
        /// <summary>
        /// PLCs the write specific.
        /// </summary>
        /// <param name="_address">The address.</param>
        /// <param name="_channelNum">The channel number.</param>
        /// <param name="_value">The value.</param>
        /// <returns></returns>
        public static bool PLC_Write_Specific(string _address, int _channelNum, string _value)
        {
            bool Complete = false;

            if (PrimaryVariables.MasterLiveDataMode.State)
            {
                Tag MyTag = new Tag();

                string MyAddress = Core.FindTagAddress(_address);
            //    ABTagClass rawTagClass = Core.FindABTagClass(_address);

                if ((_channelNum != 99) && (MyAddress != "NotFound") /*&& (rawTagClass != ABTagClass.NotFound)*/)
                {


                    try
                    {
                        /////////////////////////////////////////
                        // set Tag properties
                        MyTag.Name = MyAddress;
                        MyTag.Active = true;
                        MyTag.NetType = typeof(System.String);
               //         MyTag = FindDataType(MyTag, rawTagClass);

                        MyTag.Value = _value.ToString();

                        ///////////////////////////////
                        /// write tag and check for results
                        /// 

                        if (AB_PLCs[_channelNum].WriteTag(MyTag) == ResultCode.E_SUCCESS)
                            Complete = true;

                        if (ResultCode.QUAL_GOOD != MyTag.QualityCode)
                        {
                            DebugTools.UpdateActionList("AB Driver, Unable to Write Data");
                            PrimaryVariables.ABDataWriteErrors += 1;
                            if (PrimaryVariables.ABDataReadErrors > 3)
                                PrimaryVariables.MasterLiveDataMode.State = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Write Data, " + ex.Message);
                        PrimaryVariables.ABDataWriteErrors += 1;
                        if (PrimaryVariables.ABDataReadErrors > 3)
                            PrimaryVariables.MasterLiveDataMode.State = false;
                    }
                }
                else
                {
                    DebugTools.UpdateActionList("AB Driver, Unable to Write Data");
                    PrimaryVariables.ABDataWriteErrors += 1;
                    if (PrimaryVariables.ABDataReadErrors > 3)
                        PrimaryVariables.MasterLiveDataMode.State = false;
                }
            }
            return Complete;

        }
        /// <summary>
        /// PLCs the write specific.
        /// </summary>
        /// <param name="_address">The address.</param>
        /// <param name="_channelNum">The channel number.</param>
        /// <param name="_value">The value.</param>
        /// <returns></returns>
        public static bool PLC_Write_Specific(string _address, int _channelNum, Int32 _value)
        {
            bool Complete = false;

            if (PrimaryVariables.MasterLiveDataMode.State)
            {
                Tag MyTag = new Tag();

                string MyAddress = Core.FindTagAddress(_address);
             //   ABTagClass rawTagClass = Core.FindABTagClass(_address);

                if ((_channelNum != 99) && (MyAddress != "NotFound") /*&& (rawTagClass != ABTagClass.NotFound)*/)
                {


                    try
                    {
                        /////////////////////////////////////////
                        // set Tag properties
                        MyTag.Name = MyAddress;
                        MyTag.Active = true;
                        MyTag.NetType = typeof(System.Int32);
                    //    MyTag = FindDataType(MyTag, rawTagClass);

                        MyTag.Value = _value.ToString();

                        ///////////////////////////////
                        /// write tag and check for results
                        /// 

                        if (AB_PLCs[_channelNum].WriteTag(MyTag) == ResultCode.E_SUCCESS)
                            Complete = true;

                        if (ResultCode.QUAL_GOOD != MyTag.QualityCode)
                        {
                            DebugTools.UpdateActionList("AB Driver, Unable to Write Data");
                            PrimaryVariables.ABDataWriteErrors += 1;
                            if (PrimaryVariables.ABDataReadErrors > 3)
                                PrimaryVariables.MasterLiveDataMode.State = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Write Data, " + ex.Message);
                        PrimaryVariables.ABDataWriteErrors += 1;
                        if (PrimaryVariables.ABDataReadErrors > 3)
                            PrimaryVariables.MasterLiveDataMode.State = false;
                    }
                }
                else
                {
                    DebugTools.UpdateActionList("AB Driver, Unable to Write Data");
                    PrimaryVariables.ABDataWriteErrors += 1;
                    if (PrimaryVariables.ABDataReadErrors > 3)
                        PrimaryVariables.MasterLiveDataMode.State = false;
                }
            }
            return Complete;

        }
        /// <summary>
        /// PLCs the write specific.
        /// </summary>
        /// <param name="_address">The address.</param>
        /// <param name="_channelNum">The channel number.</param>
        /// <param name="_value">The value.</param>
        /// <returns></returns>
        public static bool PLC_Write_Specific(string _address, int _channelNum, double _value)
        {
            bool Complete = false;

            if (PrimaryVariables.MasterLiveDataMode.State)
            {
                Tag MyTag = new Tag();

                string MyAddress = Core.FindTagAddress(_address);
             //   ABTagClass rawTagClass = Core.FindABTagClass(_address);

                if ((_channelNum != 99) && (MyAddress != "NotFound") /*&& (rawTagClass != ABTagClass.NotFound)*/)
                {


                    try
                    {
                        /////////////////////////////////////////
                        // set Tag properties
                        MyTag.Name = MyAddress;
                        MyTag.Active = true;
                        MyTag.NetType = typeof(System.Single);
                     //   MyTag = FindDataType(MyTag, rawTagClass);

                        MyTag.Value = _value.ToString();

                        ///////////////////////////////
                        /// write tag and check for results
                        /// 

                        if (AB_PLCs[_channelNum].WriteTag(MyTag) == ResultCode.E_SUCCESS)
                            Complete = true;

                        if (ResultCode.QUAL_GOOD != MyTag.QualityCode)
                        {
                            DebugTools.UpdateActionList("AB Driver, Unable to Write Data");
                            PrimaryVariables.ABDataWriteErrors += 1;
                            if (PrimaryVariables.ABDataReadErrors > 3)
                                PrimaryVariables.MasterLiveDataMode.State = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Write Data, " + ex.Message);
                        PrimaryVariables.ABDataWriteErrors += 1;
                        if (PrimaryVariables.ABDataReadErrors > 3)
                            PrimaryVariables.MasterLiveDataMode.State = false;
                    }
                }
                else
                {
                    DebugTools.UpdateActionList("AB Driver, Unable to Write Data");
                    PrimaryVariables.ABDataWriteErrors += 1;
                    if (PrimaryVariables.ABDataReadErrors > 3)
                        PrimaryVariables.MasterLiveDataMode.State = false;
                }
            }
            return Complete;

        }


        /// <summary>
        /// Initiates a command to reload programs and tags names/data types from the PLC
        /// </summary>
        /// <param name="_channel">The channel.</param>
        /// <returns>
        /// bool. True if read is successful.
        /// </returns>
        public static bool PLC_RefreshData(int _channel)
        {
            // upload tags from PLC
            bool xSuccess = false;
            DebugTools.UpdateActionList("AB Driver, Reading Tags from PLC");
            int UploadStatus = AB_PLCs[_channel].UploadTags();

            if (UploadStatus != ResultCode.E_SUCCESS)
            {
                DebugTools.UpdateActionList("AB Driver, Unable to Read Tags");
                TagsLoaded[_channel] = true;
            }
            else
            {
                xSuccess = true;
            }
            DebugTools.UpdateActionList("AB Driver, Tag Read Complete");


            return xSuccess;
        }


        /// <summary>
        /// PLCs the get program names.
        /// </summary>
        /// <param name="_channel">The channel.</param>
        /// <returns></returns>
        public static List<string> PLC_GetProgramNames(int _channel)
        {
            DebugTools.UpdateActionList("AB Driver, Fetch PLC Program Names.");
            List<string> ReturnProgramList = new List<string>();
            foreach (Logix.Program PLCProgram in AB_PLCs[_channel].ProgramList)
                ReturnProgramList.Add(PLCProgram.Name);
            DebugTools.UpdateActionList("AB Driver, Program Name Fetch Complete");
            return ReturnProgramList;
        }


        /// <summary>
        /// Builds the address information for the start of the address.
        /// </summary>
        /// <param name="_channelName">The communication channel the PLC is connected to internally</param>
        /// <param name="_tagClass">The uploaded class of the tag.</param>
        /// <returns></returns>
        private static string BuildABAddressInfo(string _channelName, ABTagClass _tagClass)
        {
              return PrimaryVariables.ChannelHeader + _channelName + PrimaryVariables.ChannelFooter /*+ ProgramConstants.ABClassHeader + (ABTagClass)(int)_tagClass + ProgramConstants.ABClassFooter*/ + PrimaryVariables.AddressHeader;
        }


        /// <summary>
        /// PLCs the get variable names.
        /// </summary>
        /// <param name="_channelName">Name of the channel.</param>
        /// <param name="_channelNumber">The channel number.</param>
        /// <param name="_program">The program.</param>
        /// <param name="_ScanAllPrograms">if set to <c>true</c> [scan all programs].</param>
        /// <param name="_GlobalEnabled">if set to <c>true</c> [global enabled].</param>
        /// <param name="_ProgramEnabled">if set to <c>true</c> [program enabled].</param>
        /// <param name="_NameFilter">The name filter.</param>
        /// <param name="_ClassFilter">The class filter.</param>
        /// <returns></returns>
        public static List<string> PLC_GetVariableNames(string _channelName, int _channelNumber, string _program, bool _ScanAllPrograms, bool _GlobalEnabled, bool _ProgramEnabled, string _NameFilter, string _ClassFilter)
        {
            DebugTools.UpdateActionList("AB Driver, Fetching Variable Names.");
            List<string> PLCVariableNames = new List<string>();
            string nodeName = "";
            foreach (Logix.Program PLCProgram in AB_PLCs[_channelNumber].ProgramList)
            {
                if ((PLCProgram.Name == _program) || _ScanAllPrograms)
                {
                    if (PLCProgram.Name == "")
                    {
                        nodeName = "Global";
                        if (false == _GlobalEnabled)
                            continue;
                    }
                    else
                    {
                        nodeName = PLCProgram.Name;
                        if (false == _ProgramEnabled)
                            continue;
                    }
                    ReadOnlyCollection<TagTemplate> TagListInProgram = PLCProgram.TagItems(_NameFilter, _ClassFilter);
                    bool xIgnoreTag = false;
                    foreach (TagTemplate item in TagListInProgram)
                    {
                        xIgnoreTag = item.Name.StartsWith("_");
                        if (xIgnoreTag == false)
                        {
                            ABTagClass MyTagClass = new ABTagClass();
                            string search = item.TypeName.ToUpper();
                            if (search.Contains("BOOL"))
                                MyTagClass = ABTagClass.BOOL;
                            else if (search.Contains("SINT"))
                                MyTagClass = ABTagClass.SINT;
                            else if (search.Contains("INT"))
                                MyTagClass = ABTagClass.DINT;
                            else if (search.Contains("DINT"))
                                MyTagClass = ABTagClass.DINT;
                            else if (search.Contains("REAL"))
                                MyTagClass = ABTagClass.REAL;
                            else if (search.Contains("STRING"))
                                MyTagClass = ABTagClass.STRING;
                            else if (search.Contains("LINT"))
                                MyTagClass = ABTagClass.SINT;
                            else if (search.Contains("OBJECT"))
                                MyTagClass = ABTagClass.SINT;
                            else
                                MyTagClass = ABTagClass.NotFound;
                            string sAddress = BuildABAddressInfo(_channelName, MyTagClass) + item.Name + PrimaryVariables.AddressFooter;

                            PLCVariableNames.Add(sAddress);
                        }
                    }

                }
            }
            DebugTools.UpdateActionList("AB Driver, Fetching Variable Names Complete.");
            return PLCVariableNames;
        }

        #endregion

        #region Misc PLC Functions


        /// <summary>
        /// Clears memory in use.
        /// </summary>
        /// <param name="_channel">The channel.</param>
        /// <param name="_disposing">if set to dumps all memory <c>true</c> [_disposing].</param>
        public virtual void Dispose(int _channel, bool _disposing)
        {

            if (_disposing && (components != null))
            {
                CommGroup[_channel].Dispose();
                components.Dispose();
            }
            _disposing = true;
        }

        /// <summary>
        /// Stops the scanning of all data. (to be used on shutdown.
        /// </summary>
        /// <param name="_channel">The channel.</param>
        public static void StopScanning(int _channel)
        {
            CommGroup[_channel].ScanStop();
        }

        /// <summary>
        /// Starts the scanning of all data.
        /// </summary>
        /// <param name="_channel">The channel.</param>
        public static void StartScanning(int _channel)
        {
            CommGroup[_channel].ScanStart();
        }


        /// <summary>
        /// Disconnects the PLC from the system
        /// </summary>
        /// <param name="_channel">The channel.</param>
        private static void Disconnect(int _channel)
        {
            CommGroup[_channel].ScanStop();
            AB_PLCs[_channel].Disconnect();
        }

        /// <summary>
        /// Shudowns this instance.
        /// </summary>
        public static void Shudown()
        {
            int x = 0;
            foreach (Controller myController in AB_PLCs)
            {
                // StopScanning(x);
                if (!(myController == null))
                    Disconnect(x);
                x += 1;
            }
        }


        #endregion

        #region Type Declarations

        #endregion



    }
}
