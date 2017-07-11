using System;
using System.Collections.Generic;
using ABLink;
using Common;
using System.Collections.ObjectModel;

namespace ABDriver
{
    class SLCMLC_Communications
    {
        //static void Main(string[] args)
        //{
        //    Controller MyPLC = new Controller();
        //    Tag MyTag = new Tag();
        //    MyPLC.Timeout = 3000;
        //    int plcType = 0;
        //    int driverType = 0;
        //    string ipAddress = "";
        //    string tagName = "";
        //    int doAgain = 0;

        //    System.Console.WriteLine("NET.ABLINK 6.0 Console Example");
        //    System.Console.WriteLine("");

        //    ////////////////////////////////
        //    // select a PLC Type
        //    LABEL_ENTER_PLCTYPE:
        //    System.Console.Write("PLC Type: 1=PLC-5, 2=SLC, 3=MLC, 4=CLX MemMap, 0=Exit  ");
        //    try
        //    {
        //        plcType = Convert.ToInt32(System.Console.ReadLine());
        //    }
        //    catch
        //    {
        //        goto LABEL_ENTER_PLCTYPE;
        //    }
        //    switch (plcType)
        //    {
        //        case 0:
        //            return;
        //        case 1: // PLC-5
        //            MyPLC.CPUType = Controller.CPU.PLC;
        //            break;
        //        case 2: // SLC 5
        //            MyPLC.CPUType = Controller.CPU.SLC;
        //            break;
        //        case 3: // MicroLogix 1000, 1100, 1200, 1400, 1500
        //            MyPLC.CPUType = Controller.CPU.MLC;
        //            break;
        //        case 4: // use ML1100 for ControlLogix PLC/SLC Memory Mapped tags
        //            MyPLC.CPUType = Controller.CPU.ML1100;
        //            break;
        //        default:
        //            goto LABEL_ENTER_PLCTYPE;
        //    }

        //    System.Console.WriteLine();

        //    ////////////////////////////////////////
        //    // select driver type
        //    LABEL_ENTER_DRIVER:
        //    System.Console.Write("Driver Type: 1=ENET, 2=ENETIP, 3=ENETNI, 0=Exit  ");
        //    try
        //    {
        //        driverType = Convert.ToInt32(System.Console.ReadLine());
        //    }
        //    catch
        //    {
        //        goto LABEL_ENTER_DRIVER;
        //    }
        //    switch (driverType)
        //    {
        //        case 0:
        //            return;
        //        case 1: // ENET
        //            MyPLC.DriverType = Controller.Driver.ENET;
        //            break;
        //        case 2:
        //            MyPLC.DriverType = Controller.Driver.ENETIP;
        //            break;
        //        case 3:
        //            MyPLC.DriverType = Controller.Driver.NETENI;
        //            break;

        //        default:
        //            return;
        //    }
        //    System.Console.WriteLine();

        //    ////////////////////////////////
        //    //enter IP address
        //    LABEL_ENTER_IPADDRESS:
        //    System.Console.Write("Enter IP Address: ");
        //    try
        //    {
        //        ipAddress = System.Console.ReadLine();
        //    }
        //    catch
        //    {
        //        goto LABEL_ENTER_IPADDRESS;
        //    }
        //    if (null == ipAddress)
        //        return;
        //    if (0 == ipAddress.Length)
        //        goto LABEL_ENTER_IPADDRESS;
        //    MyPLC.IPAddress = ipAddress;

        //    System.Console.WriteLine();
        //    ////////////////////////////////
        //    //enter Tag Name
        //    LABEL_ENTER_TAGNAME:
        //    System.Console.Write("Data Table Address: ");
        //    try
        //    {
        //        tagName = System.Console.ReadLine();
        //    }
        //    catch
        //    {
        //        goto LABEL_ENTER_TAGNAME;
        //    }
        //    if (null == tagName)
        //        return;
        //    if (0 == tagName.Length)
        //        goto LABEL_ENTER_TAGNAME;
        //    MyTag.Name = tagName;

        //    //////////////////////////////
        //    // connect to the PLC
        //    if (ResultCode.E_SUCCESS != MyPLC.Connect())
        //    {
        //        System.Console.WriteLine(MyPLC.ErrorString + ":" + MyPLC.ErrorCode.ToString());
        //        System.Console.ReadLine();
        //        return;
        //    }

        //    try
        //    {
        //        /////////////////////////////
        //        // read the tag
        //        MyPLC.ReadTag(MyTag);

        //        ///////////////////////////////
        //        // check read results
        //        if (ResultCode.QUAL_GOOD == MyTag.QualityCode)
        //        {
        //            System.Console.Write("Tag Value: = ");
        //            System.Console.WriteLine(MyTag.Value.ToString());
        //            System.Console.Write("TimeStamp: = ");
        //            System.Console.WriteLine(MyTag.TimeStamp.ToString());
        //        }

        //        //////////////////////////////
        //        // display quality code
        //        System.Console.Write("Tag Quality: = ");
        //        System.Console.WriteLine(MyTag.QualityString);

        //        //////////////////////////////
        //        // display errors
        //        System.Console.Write("Error: = ");
        //        System.Console.WriteLine(MyTag.ErrorString + " " + MyTag.ErrorCode.ToString());
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Console.WriteLine(ex.Message);
        //    }
        //    System.Console.WriteLine();

        //    //////////////////////////////
        //    // read again?
        //    System.Console.Write("Continue 1=Yes, 0=Exit  ");
        //    try
        //    {
        //        doAgain = Convert.ToInt32(System.Console.ReadLine());
        //    }
        //    catch
        //    {
        //        return;
        //    }
        //    if (0 != doAgain)
        //        goto LABEL_ENTER_PLCTYPE;

        //}

        #region Global Variables


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

        private static Controller.CPU GetCPUType(Int32 CPUType)
        {
            switch (CPUType)
            {

                case 1: // PLC-5
                    return Controller.CPU.PLC;
                    
                case 2: // SLC 5
                    return Controller.CPU.SLC;
                    
                case 3: // MicroLogix 1000, 1100, 1200, 1400, 1500
                    return Controller.CPU.MLC;
                    
                case 4: // use ML1100 for ControlLogix PLC/SLC Memory Mapped tags
                    return Controller.CPU.ML1100;
                    
                default:
                    return Controller.CPU.ML1100;
            }
        }


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
        public static Int32 CommInit( string _IPAddress, string _slot, int _timeout, Int32 CPUTType)
        {
            AB_PLCs.Add(new Controller());
           
            Int32 _channel = AB_PLCs.Count - 1;
            
            AB_PLCs[_channel].IPAddress = _IPAddress;
            AB_PLCs[_channel].Path = _slot;
            AB_PLCs[_channel].Timeout = _timeout;
            AB_PLCs[_channel].CPUType = GetCPUType(CPUTType);
            AB_PLCs[_channel].DriverType = Controller.Driver.ENETIP;
            //      if (ResultCode.E_SUCCESS == AB_PLCs[_channel].Connect())
            //       {
            //CommGroup[_channel].Controller = AB_PLCs[_channel];
            //CommGroup[_channel].ScanStart();
       //     }
       //     else
        //    {
       //         DebugTools.UpdateActionList("AB Driver, Unable to Establish Comms.");
        //    }
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

            if (ProgramConstants.MasterLiveDataMode.State)
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
                        ProgramConstants.ABDataWriteErrors += 1;
                    }
                }
                catch (Exception ex)
                {
                    DebugTools.UpdateActionList("AB Driver, " + ex.Message);
                    ProgramConstants.ABDataWriteErrors += 1;
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

            if (ProgramConstants.MasterLiveDataMode.State)
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
                    ProgramConstants.ABDataReadErrors += 1;
                }

                if (ResultCode.QUAL_GOOD == MyTag.QualityCode)
                    Value = MyTag.Value.ToString();
                else
                {
                    DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                    ProgramConstants.ABDataReadErrors += 1;
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

            if (ProgramConstants.MasterLiveDataMode.State)
            {
                Tag MyTag = new Tag();

                string MyAddress = Core.FindTagAddress(_address);
             //   ABTagClass rawTagClass = Core.FindABTagClass(_address);
                if ((_channelNum != 99) && (MyAddress != "NotFound")/* && (rawTagClass != ABTagClass.NotFound)*/)
                {
                    MyTag.NetType = typeof(System.Boolean);
             //       MyTag = FindDataType(MyTag, rawTagClass);

                    MyTag.Name = MyAddress;
                    MyTag.Length = 1;

                    // Check Tag Read Stats

                    int ReadResponse = AB_PLCs[_channelNum].ReadTag(MyTag);
                    if (ReadResponse != ResultCode.E_SUCCESS)
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                        ProgramConstants.ABDataReadErrors += 1;
                        if (ProgramConstants.ABDataReadErrors > 3)
                            ProgramConstants.MasterLiveDataMode.State = false;
                    }
                    if (ResultCode.QUAL_GOOD == MyTag.QualityCode)
                    {
                        _value = Convert.ToBoolean(MyTag.Value);
                        xSuccess = true;
                    }
                    else
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                        ProgramConstants.ABDataReadErrors += 1;
                        if (ProgramConstants.ABDataReadErrors > 3)
                            ProgramConstants.MasterLiveDataMode.State = false;
                    }
                }
                else
                {
                    DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                    ProgramConstants.ABDataReadErrors += 1;
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

            if (ProgramConstants.MasterLiveDataMode.State)
            {
                Tag MyTag = new Tag();


                string MyAddress = Core.FindTagAddress(_address);
            //    ABTagClass rawTagClass = Core.FindABTagClass(_address);
                if ((_channelNum != 99) && (MyAddress != "NotFound")/* && (rawTagClass != ABTagClass.NotFound)*/)
                {
                    MyTag.NetType = typeof(System.String);
                   // MyTag = FindDataType(MyTag, rawTagClass);

                    MyTag.Name = MyAddress;
                    MyTag.Length = 1;

                    // Check Tag Read Stats

                    int ReadResponse = AB_PLCs[_channelNum].ReadTag(MyTag);
                    if (ReadResponse != ResultCode.E_SUCCESS)
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                        ProgramConstants.ABDataReadErrors += 1;
                        if (ProgramConstants.ABDataReadErrors > 3)
                            ProgramConstants.MasterLiveDataMode.State = false;
                    }
                    if (ResultCode.QUAL_GOOD == MyTag.QualityCode)
                    {
                        _value = MyTag.Value.ToString();
                        xSuccess = true;
                    }
                    else
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                        ProgramConstants.ABDataReadErrors += 1;
                        if (ProgramConstants.ABDataReadErrors > 3)
                            ProgramConstants.MasterLiveDataMode.State = false;
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
        public static bool PLC_Read_Specific(string _address, int _channelNum, out Int32 _value)
        {
            _value = 0;
            bool xSuccess = false;

            if (ProgramConstants.MasterLiveDataMode.State)
            {
                Tag MyTag = new Tag();


                string MyAddress = Core.FindTagAddress(_address);
              //  ABTagClass rawTagClass = Core.FindABTagClass(_address);
                if ((_channelNum != 99) && (MyAddress != "NotFound") /*&& (rawTagClass != ABTagClass.NotFound)*/)
                {
                    MyTag.NetType = typeof(System.Int32);
                  //  MyTag = FindDataType(MyTag, rawTagClass);

                    MyTag.Name = MyAddress;
                    MyTag.Length = 1;
                    
                    // Check Tag Read Stats

                    int ReadResponse = AB_PLCs[_channelNum].ReadTag(MyTag);
                    if (ReadResponse != ResultCode.E_SUCCESS)
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                        ProgramConstants.ABDataReadErrors += 1;
                        if (ProgramConstants.ABDataReadErrors > 3)
                            ProgramConstants.MasterLiveDataMode.State = false;
                    }
                    if (ResultCode.QUAL_GOOD == MyTag.QualityCode)
                    {
                        _value = Convert.ToInt32(MyTag.Value);
                        xSuccess = true;
                    }
                    else
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                        ProgramConstants.ABDataReadErrors += 1;
                        if (ProgramConstants.ABDataReadErrors > 3)
                            ProgramConstants.MasterLiveDataMode.State = false;
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

            if (ProgramConstants.MasterLiveDataMode.State)
            {
                Tag MyTag = new Tag();


                string MyAddress = Core.FindTagAddress(_address);
            //    ABTagClass rawTagClass = Core.FindABTagClass(_address);
                if ((_channelNum != 99) && (MyAddress != "NotFound")/* && (rawTagClass != ABTagClass.NotFound)*/)
                {
                    MyTag.NetType = typeof(System.Single);
             //       MyTag = FindDataType(MyTag, rawTagClass);

                    MyTag.Name = MyAddress;
                    MyTag.Length = 1;

                    // Check Tag Read Stats

                    int ReadResponse = AB_PLCs[_channelNum].ReadTag(MyTag);
                    if (ReadResponse != ResultCode.E_SUCCESS)
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                        ProgramConstants.ABDataReadErrors += 1;
                        if (ProgramConstants.ABDataReadErrors > 3)
                            ProgramConstants.MasterLiveDataMode.State = false;
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
                        ProgramConstants.ABDataReadErrors += 1;
                        if (ProgramConstants.ABDataReadErrors > 3)
                            ProgramConstants.MasterLiveDataMode.State = false;
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

            if (ProgramConstants.MasterLiveDataMode.State)
            {
                Tag MyTag = new Tag();

                string MyAddress = Core.FindTagAddress(_address);
              //  ABTagClass rawTagClass = Core.FindABTagClass(_address);

                if ((_channelNum != 99) && (MyAddress != "NotFound") /*&& (rawTagClass != ABTagClass.NotFound)*/)
                {


                    try
                    {
                        /////////////////////////////////////////
                        // set Tag properties
                        MyTag.Name = MyAddress;
                        MyTag.Active = true;
                        MyTag.NetType = typeof(System.Boolean);
                    //    MyTag = FindDataType(MyTag, rawTagClass);

                        MyTag.Value = _value;

                        ///////////////////////////////
                        /// write tag and check for results
                        /// 

                        if (AB_PLCs[_channelNum].WriteTag(MyTag) == ResultCode.E_SUCCESS)
                            Complete = true;

                        if (ResultCode.QUAL_GOOD != MyTag.QualityCode)
                        {
                            DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                            ProgramConstants.ABDataWriteErrors += 1;
                            if (ProgramConstants.ABDataReadErrors > 3)
                                ProgramConstants.MasterLiveDataMode.State = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data, " + ex.Message);
                        ProgramConstants.ABDataWriteErrors += 1;
                        if (ProgramConstants.ABDataReadErrors > 3)
                            ProgramConstants.MasterLiveDataMode.State = false;
                    }
                }
                else
                {
                    DebugTools.UpdateActionList("AB Driver, Unable to Read Data");
                    ProgramConstants.ABDataWriteErrors += 1;
                    if (ProgramConstants.ABDataReadErrors > 3)
                        ProgramConstants.MasterLiveDataMode.State = false;
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

            if (ProgramConstants.MasterLiveDataMode.State)
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
                        MyTag.NetType = typeof(System.String);
                 //       MyTag = FindDataType(MyTag, rawTagClass);

                        MyTag.Value = _value.ToString();

                        ///////////////////////////////
                        /// write tag and check for results
                        /// 

                        if (AB_PLCs[_channelNum].WriteTag(MyTag) == ResultCode.E_SUCCESS)
                            Complete = true;

                        if (ResultCode.QUAL_GOOD != MyTag.QualityCode)
                        {
                            DebugTools.UpdateActionList("AB Driver, Unable to Write Data");
                            ProgramConstants.ABDataWriteErrors += 1;
                            if (ProgramConstants.ABDataReadErrors > 3)
                                ProgramConstants.MasterLiveDataMode.State = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Write Data, " + ex.Message);
                        ProgramConstants.ABDataWriteErrors += 1;
                        if (ProgramConstants.ABDataReadErrors > 3)
                            ProgramConstants.MasterLiveDataMode.State = false;
                    }
                }
                else
                {
                    DebugTools.UpdateActionList("AB Driver, Unable to Write Data");
                    ProgramConstants.ABDataWriteErrors += 1;
                    if (ProgramConstants.ABDataReadErrors > 3)
                        ProgramConstants.MasterLiveDataMode.State = false;
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

            if (ProgramConstants.MasterLiveDataMode.State)
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
                        MyTag.NetType = typeof(System.Int32);
                      //  MyTag = FindDataType(MyTag, rawTagClass);

                        MyTag.Value = _value.ToString();

                        ///////////////////////////////
                        /// write tag and check for results
                        /// 

                        if (AB_PLCs[_channelNum].WriteTag(MyTag) == ResultCode.E_SUCCESS)
                            Complete = true;

                        if (ResultCode.QUAL_GOOD != MyTag.QualityCode)
                        {
                            DebugTools.UpdateActionList("AB Driver, Unable to Write Data");
                            ProgramConstants.ABDataWriteErrors += 1;
                            if (ProgramConstants.ABDataReadErrors > 3)
                                ProgramConstants.MasterLiveDataMode.State = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Write Data, " + ex.Message);
                        ProgramConstants.ABDataWriteErrors += 1;
                        if (ProgramConstants.ABDataReadErrors > 3)
                            ProgramConstants.MasterLiveDataMode.State = false;
                    }
                }
                else
                {
                    DebugTools.UpdateActionList("AB Driver, Unable to Write Data");
                    ProgramConstants.ABDataWriteErrors += 1;
                    if (ProgramConstants.ABDataReadErrors > 3)
                        ProgramConstants.MasterLiveDataMode.State = false;
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

            if (ProgramConstants.MasterLiveDataMode.State)
            {
                Tag MyTag = new Tag();

                string MyAddress = Core.FindTagAddress(_address);
      //          ABTagClass rawTagClass = Core.FindABTagClass(_address);

                if ((_channelNum != 99) && (MyAddress != "NotFound")/* && (rawTagClass != ABTagClass.NotFound)*/)
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
                            ProgramConstants.ABDataWriteErrors += 1;
                            if (ProgramConstants.ABDataReadErrors > 3)
                                ProgramConstants.MasterLiveDataMode.State = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        DebugTools.UpdateActionList("AB Driver, Unable to Write Data, " + ex.Message);
                        ProgramConstants.ABDataWriteErrors += 1;
                        if (ProgramConstants.ABDataReadErrors > 3)
                            ProgramConstants.MasterLiveDataMode.State = false;
                    }
                }
                else
                {
                    DebugTools.UpdateActionList("AB Driver, Unable to Write Data");
                    ProgramConstants.ABDataWriteErrors += 1;
                    if (ProgramConstants.ABDataReadErrors > 3)
                        ProgramConstants.MasterLiveDataMode.State = false;
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
            foreach (ABLink.Program PLCProgram in AB_PLCs[_channel].ProgramList)
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
            return ProgramConstants.ChannelHeader + _channelName + ProgramConstants.ChannelFooter /*+ ProgramConstants.ABClassHeader + (ABTagClass)(int)_tagClass + ProgramConstants.ABClassFooter*/ + ProgramConstants.AddressHeader;
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
            foreach (ABLink.Program PLCProgram in AB_PLCs[_channelNumber].ProgramList)
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
                            string sAddress = BuildABAddressInfo(_channelName, MyTagClass) + item.Name + ProgramConstants.AddressFooter;

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
              //  CommGroup[_channel].Dispose();
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
         //   CommGroup[_channel].ScanStop();
        }

        /// <summary>
        /// Starts the scanning of all data.
        /// </summary>
        /// <param name="_channel">The channel.</param>
        public static void StartScanning(int _channel)
        {
          //  CommGroup[_channel].ScanStart();
        }


        /// <summary>
        /// Disconnects the PLC from the system
        /// </summary>
        /// <param name="_channel">The channel.</param>
        private static void Disconnect(int _channel)
        {
        //    CommGroup[_channel].ScanStop();
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
