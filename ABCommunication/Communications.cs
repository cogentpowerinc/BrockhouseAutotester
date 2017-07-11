
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ABDriver;

using Common;

namespace PLC
{

    /// <summary>
    /// Used for the communication requests
    /// </summary>
    public static class CommManager
    {


        /// <summary>
        /// The current channels
        /// </summary>
        public static List<String> CurrentChannels = new List<string>();
        /// <summary>
        /// My ab programs
        /// </summary>
        public static List<string>[] MyABPrograms = new List<string>[10];
        /// <summary>
        /// The opc data errors
        /// </summary>
        public static int OPCDataErrors = 0;
        /// <summary>
        /// The ab data errors
        /// </summary>
        public static int ABDataErrors = 0;

        /// <summary>
        /// The comms active
        /// </summary>
        public static bool CommsActive = false;


        #region External Requests

        /// <summary>
        /// Finds the full address.
        /// </summary>
        /// <param name="_addressFragment">The _address fragment.</param>
        /// <returns>
        /// Returns the Entire address from the provided address fragment
        /// </returns>
        public static string FindFullAddress(string _addressFragment)
        {
            string found = "";
            foreach (Channel channel in Channels.MyChannelsList)
            {
                found = channel.CurrentPaths.Find(Raw => Raw.Contains(_addressFragment));
                if (!(found == "")) { break; }

            }
            return found;
        }

        #region Get PLC Data
        /// <summary>
        /// Gets the PLC value.
        /// </summary>
        /// <param name="_PLCPath">The PLC path.</param>
        /// <param name="_value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static bool GetPLCValue(string _PLCPath, out bool _value)
        {
            string ChannelName = Core.FindChannelName(_PLCPath);
            return Channels.GetChannel(ChannelName).CommObject.GetValue(_PLCPath, ChannelName, out _value);
        }
        /// <summary>
        /// Gets the PLC value.
        /// </summary>
        /// <param name="_PLCPath">The PLC path.</param>
        /// <param name="_value">The value.</param>
        /// <returns></returns>
        public static bool GetPLCValue(string _PLCPath, out string _value)
        {
            string ChannelName = Core.FindChannelName(_PLCPath);
            return Channels.GetChannel(ChannelName).CommObject.GetValue(_PLCPath, ChannelName, out _value);
        }
        /// <summary>
        /// Gets the PLC value.
        /// </summary>
        /// <param name="_PLCPath">The PLC path.</param>
        /// <param name="_value">The value.</param>
        /// <returns></returns>
        public static bool GetPLCValue(string _PLCPath, out Int32 _value)
        {
            string ChannelName = Core.FindChannelName(_PLCPath);
            return Channels.GetChannel(ChannelName).CommObject.GetValue(_PLCPath, ChannelName, out _value);
        }
        /// <summary>
        /// Gets the PLC value.
        /// </summary>
        /// <param name="_PLCPath">The PLC path.</param>
        /// <param name="_value">The value.</param>
        /// <returns></returns>
        public static bool GetPLCValue(string _PLCPath, out double _value)
        {
            string ChannelName = Core.FindChannelName(_PLCPath);
            return Channels.GetChannel(ChannelName).CommObject.GetValue(_PLCPath, ChannelName, out _value);
        }
        /// <summary>
        /// Gets the PLC value bool.
        /// </summary>
        /// <param name="_PLCPath">The PLC path.</param>
        /// <param name="Success">if set to <c>true</c> [success].</param>
        /// <returns></returns>
        public static bool GetPLCValue_BOOL(string _PLCPath, out bool Success)
        {
            bool _value = false;
            string ChannelName = Core.FindChannelName(_PLCPath);
            Success = Channels.GetChannel(ChannelName).CommObject.GetValue(_PLCPath, ChannelName, out _value);
            return _value;
        }
        /// <summary>
        /// Gets the PLC value string.
        /// </summary>
        /// <param name="_PLCPath">The PLC path.</param>
        /// <param name="Success">if set to <c>true</c> [success].</param>
        /// <returns></returns>
        public static string GetPLCValue_STRING(string _PLCPath, out bool Success)
        {
            string _value = "";
            string ChannelName = Core.FindChannelName(_PLCPath);
            Success = Channels.GetChannel(ChannelName).CommObject.GetValue(_PLCPath, ChannelName, out _value);
            return _value;
        }
        /// <summary>
        /// Gets the PLC value in T32.
        /// </summary>
        /// <param name="_PLCPath">The PLC path.</param>
        /// <param name="Success">if set to <c>true</c> [success].</param>
        /// <returns></returns>
        public static Int32 GetPLCValue_INT32(string _PLCPath, out bool Success)
        {
            Int32 _value = 0;
            String ChannelName = Core.FindChannelName(_PLCPath);
            Success = Channels.GetChannel(ChannelName).CommObject.GetValue(_PLCPath, ChannelName, out _value);
            return _value;
        }
        /// <summary>
        /// Gets the PLC value double.
        /// </summary>
        /// <param name="_PLCPath">The PLC path.</param>
        /// <param name="Success">if set to <c>true</c> [success].</param>
        /// <returns></returns>
        public static double GetPLCValue_DOUBLE(string _PLCPath, out bool Success)
        {
            double _value = 0;
            String ChannelName = Core.FindChannelName(_PLCPath);
            Success = Channels.GetChannel(ChannelName).CommObject.GetValue(_PLCPath, ChannelName, out _value);
            return _value;
        }
        #endregion
        #region Set PLC Data
        /// <summary>
        /// Set a value in the PLC, the system checks if the supplied channel is correct
        /// </summary>
        /// <param name="_PLCPath">The path to set</param>
        /// <param name="_newValue">The value to set</param>
        /// <returns>
        /// True if success
        /// </returns>
        public static bool SetPLCValue(string _PLCPath, bool _newValue)
        {
            string ChannelName = Core.FindChannelName(_PLCPath);
            return Channels.GetChannel(ChannelName).CommObject.SetValue(_PLCPath, ChannelName, _newValue);
        }
        /// <summary>
        /// Set a value in the PLC, the system checks if the supplied channel is correct
        /// </summary>
        /// <param name="_PLCPath">The path to set</param>
        /// <param name="_newValue">The value to set</param>
        /// <returns>
        /// True if success
        /// </returns>
        public static bool SetPLCValue(string _PLCPath, string _newValue)
        {
            string ChannelName = Core.FindChannelName(_PLCPath);
            return Channels.GetChannel(ChannelName).CommObject.SetValue(_PLCPath, ChannelName, _newValue);
        }
        /// <summary>
        /// Set a value in the PLC, the system checks if the supplied channel is correct
        /// </summary>
        /// <param name="_PLCPath">The path to set</param>
        /// <param name="_newValue">The value to set</param>
        /// <returns>
        /// True if success
        /// </returns>
        public static bool SetPLCValue(string _PLCPath, Int32 _newValue)
        {
            string ChannelName = Core.FindChannelName(_PLCPath);
            return Channels.GetChannel(ChannelName).CommObject.SetValue(_PLCPath, ChannelName, _newValue);
        }
        /// <summary>
        /// Set a value in the PLC, the system checks if the supplied channel is correct
        /// </summary>
        /// <param name="_PLCPath">The path to set</param>
        /// <param name="_newValue">The value to set</param>
        /// <returns>
        /// True if success
        /// </returns>
        public static bool SetPLCValue(string _PLCPath, double _newValue)
        {
            string ChannelName = Core.FindChannelName(_PLCPath);
            return Channels.GetChannel(ChannelName).CommObject.SetValue(_PLCPath, ChannelName, _newValue);
        }



        #endregion
        /// <summary>
        /// Gets all PLC paths.
        /// </summary>
        /// <returns></returns>
        public static bool GetAllPLCPaths()
        {
            bool xSuccess = true;
            bool xTempSuccess = false;
            foreach (string MyChannel in CurrentChannels)
            {
                xTempSuccess = Channels.GetChannel(MyChannel).CommObject.GetAllPaths(MyChannel);
                if (!xTempSuccess)
                    xSuccess = false;
            }

            return xSuccess;
        }

        /// <summary>
        /// Channels the enable.
        /// </summary>
        /// <param name="_ChannelName">Name of the channel.</param>
        /// <returns></returns>
        public static bool ChannelEnable(String _ChannelName)
        {
            bool EnabledStatus = false;
            try
            {
                int Channel = Channels.GetChannel(_ChannelName).ChannelNumber;
                foreach (Channel MyChannel in Channels.ChannelsList)
                    if (MyChannel.ChannelNumber == Channel)
                    {
                        EnabledStatus = MyChannel.Enabled;
                        break;
                    }
            }
            catch
            {
                // Channel not Found
            }
            return EnabledStatus;
        }

        ///// <summary>
        ///// Finds the opc servers.
        ///// Must be passed either "DA_Server" or "UA_Server"
        ///// </summary>
        ///// <param name="_serverType">Type of the _server.</param>
        ///// <returns></returns>
        //public static List<string> FindOPCServers(string _serverType)
        //{
        //    return OPCCommunication.FindChannelNames(_serverType);
        //}
        /// <summary>
        /// Shuts down ab driver.
        /// </summary>
        public static void ShutDownABDriver()
        {
            CLX_Communications.Shudown();
        }

        #endregion
        #region Channel Definition
        /// <summary>
        /// Holds all information about all channels,
        /// </summary>
        public static class Channels   /// sealed class can not be inherited
        {
            /// <summary>
            /// The channels list
            /// </summary>
            public static List<Channel> ChannelsList = new List<Channel>();
            /// <summary>
            /// Initializes the <see cref="Channels" /> class.
            /// </summary>
            static Channels() // Initialization at startup
            {
                ChannelsList = new List<Channel>();
            }
            /// <summary>
            /// Gets or sets my channels list.
            /// </summary>
            /// <value>
            /// My channels list.
            /// </value>
            public static List<Channel> MyChannelsList
            {
                get { return ChannelsList; }
                set { ChannelsList = value; }
            }

            /// <summary>
            /// Add a channel to the system
            /// </summary>
            /// <param name="_myChannel">My channel.</param>
            public static void AddChannel(Channel _myChannel)
            {
                ChannelsList.Add(_myChannel);
                if (ChannelsList.Count == 0)
                    CurrentChannels = new List<string>();
                CurrentChannels.Add(_myChannel.Name);
            }

            /// <summary>
            /// Removes the channel.
            /// </summary>
            /// <param name="_byName">Name of the _by.</param>
            /// <returns></returns>
            public static Boolean RemoveChannel(string _byName)
            {
                int index = ChannelsList.FindIndex(F => F.Name.Equals(_byName, StringComparison.Ordinal));
                if (index != -1)
                {
                    ChannelsList.RemoveAt(index);
                    CurrentChannels.Remove(_byName);
                    return true;
                }
                return false;
            }

            /// <summary>
            /// Gets a specified channel
            /// </summary>
            /// <param name="_byName">The name of the channel</param>
            /// <returns>
            /// The channel requested
            /// </returns>
            public static Channel GetChannel(string _byName)
            {
                Channel returnChannel = null;
                returnChannel = ChannelsList.Find(F => F.Name.Equals(_byName, StringComparison.Ordinal));  
                if (returnChannel != null)
                    return returnChannel;
                else
                    return returnChannel;
            }
        }

        /// <summary>
        /// Hold the information needed to connect to the PLC
        /// </summary>
        public class Channel
        {
           
            /// <summary>
            /// The name
            /// </summary>
            [XmlAttribute()]
            public string Name = "";
            /// <summary>
            /// The channel number
            /// </summary>
            [XmlAttribute()]
            public int ChannelNumber;
            /// <summary>
            /// The enabled
            /// </summary>
            [XmlAttribute()]
            public Boolean Enabled;
            /// <summary>
            /// The initialized
            /// </summary>
            public Boolean Initialized;
            /// <summary>
            /// The ip adress
            /// </summary>
            [XmlAttribute()]
            public string IPAdress = "";  
            /// <summary>
            /// The slot
            /// </summary>
            [XmlAttribute()]
            public string Slot = "0";

            // Added for SLCMLC
            public Int32 CPUType = 4; //PLC Type: 1=PLC-5, 2=SLC, 3=MLC, 4=CLX MemMap "
            

            
            /// <summary>
            /// The port number
            /// </summary>
            [XmlAttribute()]
            public int PortNumber;
            /// <summary>
            /// The opc server type
            /// </summary>
            [XmlAttribute()]
            public string OPCServerType = "No Type Set";
            /// <summary>
            /// The time out time
            /// </summary>
            [XmlAttribute()]
            public int TimeOutTime; // in ms
            /// <summary>
            /// The comm check address
            /// </summary>
            [XmlAttribute()]
            public string CommCheckAddress = "";
            /// <summary>
            /// The comm events
            /// </summary>
            public List<Int32> CommEvents = new List<Int32>();
            /// <summary>
            /// The status
            /// </summary>
            public CommStatus Status = new CommStatus();
            /// <summary>
            /// The login data
            /// </summary>
            public UserLogin LoginData;
            /// <summary>
            /// The driver type
            /// </summary>
            [XmlAttribute()]
            public PLCDriverType DriverType;
            /// <summary>
            /// The comm object
            /// </summary>
            [System.Xml.Serialization.XmlIgnore()]
            internal CommType CommObject;     // This is the object that will be used to connect to the PLC
            /// <summary>
            /// The current paths
            /// </summary>
            public List<String> CurrentPaths = new List<string>();
            /// <summary>
            /// Empty constructor
            /// </summary>
            public Channel()
            {
                InitilizeCommDriver();  // this is only used by the serializer 
            }
            /// <summary>
            /// Constructor for adding new channels (normally u use this one for adding an channel to channels
            /// </summary>
            /// <param name="_myDriverType">Type of my driver.</param>
            /// <param name="_myName">My name.</param>
            /// <param name="_IsEnabled">if set to <c>true</c> [is enabled].</param>
            /// <param name="_myChannelNumber">My channel number.</param>
            /// <param name="_myOPCServerType">Type of my opc server.</param>
            /// <param name="_myIPAdress">My ip adress.</param>
            /// <param name="_mySlot">My slot.</param>
            /// <param name="_myTimeOut">My time out.</param>
            /// <param name="_myCommCheckAddress">My comm check address.</param>
            public Channel(PLCDriverType _myDriverType, string _myName, string _myIPAdress, string _mySlot, int _myTimeOut, Int32 _myCPUTType)
            {
                DriverType = _myDriverType;
                CPUType = _myCPUTType;
                Name = _myName;
                Enabled = true;
                IPAdress = _myIPAdress;  // some bastard resisted testing would be in place here
                Slot = _mySlot;
                TimeOutTime = _myTimeOut;


                // add the other Channel members here
                ChannelNumber = InitilizeCommDriver(); // keep this at the end
            }

            /// <summary>
            /// Initialize the requested driver
            /// </summary>
            private Int32 InitilizeCommDriver()
            {
                if (DriverType == PLCDriverType.AllenBradley_CLX)
                {
                    CommObject = new CommAllenBradley_CLX_Obj();
                    return CLX_Communications.CommInit(IPAdress, Slot, TimeOutTime);
                }
                //if (DriverType == eCommDriver.OPC)
                //    CommObject = new CommOPC_Obj();
                if (DriverType == PLCDriverType.AllenBradley_SLCMLC)
                {
                    CommObject = new CommAllenBradley_SLCMLC_Obj();
                    return SLCMLC_Communications.CommInit(IPAdress, Slot, TimeOutTime, CPUType);
                }
                return -1;
            }

            /// <summary>
            /// Structure for login info
            /// </summary>
            public struct UserLogin
            {
                /// <summary>
                /// The is required
                /// </summary>
                [XmlAttribute()]
                public Boolean IsRequired;  // is login required yes/no
                /// <summary>
                /// The user name
                /// </summary>
                [XmlAttribute()]
                public String userName;
                /// <summary>
                /// The password
                /// </summary>
                [XmlAttribute()]
                public String password;
            }
        }
        #endregion

        #region Comm Interface Definition
        /// <summary>
        /// Enum for the comm types
        /// </summary>
        public enum PLCDriverType
        {
            /// <summary>
            /// The allen bradley
            /// </summary>
            AllenBradley_CLX,
            /// <summary>
            /// The opc
            /// </summary>
            OPC,
            AllenBradley_SLCMLC,
        }

        /// <summary>
        /// Interface of the communication objects
        /// </summary>
        internal interface CommType
        {
            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <param name="PLCPath">The PLC path.</param>
            /// <param name="_channelName">Name of the channel.</param>
            /// <param name="_value">if set to <c>true</c> [value].</param>
            /// <returns></returns>
            bool GetValue(string PLCPath, string _channelName, out bool _value);
            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <param name="PLCPath">The PLC path.</param>
            /// <param name="_channelName">Name of the channel.</param>
            /// <param name="_value">The value.</param>
            /// <returns></returns>
            bool GetValue(string PLCPath, string _channelName, out string _value);
            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <param name="PLCPath">The PLC path.</param>
            /// <param name="_channelName">Name of the channel.</param>
            /// <param name="_value">The value.</param>
            /// <returns></returns>
            bool GetValue(string PLCPath, string _channelName, out Int32 _value);
            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <param name="PLCPath">The PLC path.</param>
            /// <param name="_channelName">Name of the channel.</param>
            /// <param name="_value">The value.</param>
            /// <returns></returns>
            bool GetValue(string PLCPath, string _channelName, out double _value);
            /// <summary>
            /// Sets the value.
            /// </summary>
            /// <param name="PLCPath">The PLC path.</param>
            /// <param name="_channelName">Name of the channel.</param>
            /// <param name="newValue">if set to <c>true</c> [new value].</param>
            /// <returns></returns>
            bool SetValue(string PLCPath, string _channelName, bool newValue);
            /// <summary>
            /// Sets the value.
            /// </summary>
            /// <param name="PLCPath">The PLC path.</param>
            /// <param name="_channelName">Name of the channel.</param>
            /// <param name="newValue">The new value.</param>
            /// <returns></returns>
            bool SetValue(string PLCPath, string _channelName, string newValue);
            /// <summary>
            /// Sets the value.
            /// </summary>
            /// <param name="PLCPath">The PLC path.</param>
            /// <param name="_channelName">Name of the channel.</param>
            /// <param name="newValue">The new value.</param>
            /// <returns></returns>
            bool SetValue(string PLCPath, string _channelName, Int32 newValue);
            /// <summary>
            /// Sets the value.
            /// </summary>
            /// <param name="PLCPath">The PLC path.</param>
            /// <param name="_channelName">Name of the channel.</param>
            /// <param name="newValue">The new value.</param>
            /// <returns></returns>
            bool SetValue(string PLCPath, string _channelName, double newValue);
            /// <summary>
            /// Gets all paths.
            /// </summary>
            /// <param name="channelName">Name of the channel.</param>
            /// <returns></returns>
            bool GetAllPaths(string channelName);

        }

        #region AB Definition CLX
        /// <summary>
        /// The object that controls the Allen Bradley communication
        /// </summary>
        /// <seealso cref="PLC.CommManager.CommType" />
        internal class CommAllenBradley_CLX_Obj : CommType
        {
            /// <summary>
            /// My PLC type
            /// </summary>
            PLCDriverType myPLC_Type ;
            /// <summary>
            /// Constructor
            /// </summary>
            public CommAllenBradley_CLX_Obj()
            {
                myPLC_Type = PLCDriverType.AllenBradley_CLX;
            }
            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <param name="_PLCPath">The _ PLC path.</param>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <param name="_value">if set to <c>true</c> [_value].</param>
            /// <returns></returns>
            bool CommType.GetValue(string _PLCPath, string _channelName, out bool _value)
            {
                // Underneath is only for testing purpose
                _value = false;
                int ChannelNum = Channels.GetChannel(_channelName).ChannelNumber;
                return CLX_Communications.PLC_Read_Specific(_PLCPath, ChannelNum, out _value);
            }
            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <param name="_PLCPath">The _ PLC path.</param>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <param name="_value">The _value.</param>
            /// <returns></returns>
            bool CommType.GetValue(string _PLCPath, string _channelName, out string _value)
            {
                // Underneath is only for testing purpose
                _value = "";
                int ChannelNum = Channels.GetChannel(_channelName).ChannelNumber;
                return CLX_Communications.PLC_Read_Specific(_PLCPath, ChannelNum, out _value);
            }
            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <param name="_PLCPath">The _ PLC path.</param>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <param name="_value">The _value.</param>
            /// <returns></returns>
            bool CommType.GetValue(string _PLCPath, string _channelName, out Int32 _value)
            {
                // Underneath is only for testing purpose
                _value = 0;
                int ChannelNum = Channels.GetChannel(_channelName).ChannelNumber;
                return CLX_Communications.PLC_Read_Specific(_PLCPath, ChannelNum, out _value);
            }
            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <param name="_PLCPath">The _ PLC path.</param>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <param name="_value">The _value.</param>
            /// <returns></returns>
            bool CommType.GetValue(string _PLCPath, string _channelName, out double _value)
            {
                // Underneath is only for testing purpose
                _value = 0;
                int ChannelNum = Channels.GetChannel(_channelName).ChannelNumber;
                return CLX_Communications.PLC_Read_Specific(_PLCPath, ChannelNum, out _value);
            }

            /// <summary>
            /// Set a value in the PLC, the system checks if the supplied channel is correct
            /// </summary>
            /// <param name="_PLCPath">The path to set</param>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <param name="_newValue">The value to set</param>
            /// <returns>
            /// True if success
            /// </returns>
            bool CommType.SetValue(string _PLCPath, string _channelName, bool _newValue)
            {
                int ChannelNum = Channels.GetChannel(_channelName).ChannelNumber;
                return CLX_Communications.PLC_Write_Specific(_PLCPath, ChannelNum, _newValue);
            }
            /// <summary>
            /// Sets the value.
            /// </summary>
            /// <param name="_PLCPath">The _ PLC path.</param>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <param name="_newValue">The _new value.</param>
            /// <returns></returns>
            bool CommType.SetValue(string _PLCPath, string _channelName, string _newValue)
            {
                int ChannelNum = Channels.GetChannel(_channelName).ChannelNumber;
                return CLX_Communications.PLC_Write_Specific(_PLCPath, ChannelNum, _newValue);
            }
            /// <summary>
            /// Sets the value.
            /// </summary>
            /// <param name="_PLCPath">The _ PLC path.</param>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <param name="_newValue">The _new value.</param>
            /// <returns></returns>
            bool CommType.SetValue(string _PLCPath, string _channelName, Int32 _newValue)
            {
                int ChannelNum = Channels.GetChannel(_channelName).ChannelNumber;
                return CLX_Communications.PLC_Write_Specific(_PLCPath, ChannelNum, _newValue);
            }
            /// <summary>
            /// Sets the value.
            /// </summary>
            /// <param name="_PLCPath">The _ PLC path.</param>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <param name="_newValue">The _new value.</param>
            /// <returns></returns>
            bool CommType.SetValue(string _PLCPath, string _channelName, double _newValue)
            {
                int ChannelNum = Channels.GetChannel(_channelName).ChannelNumber;
                return CLX_Communications.PLC_Write_Specific(_PLCPath, ChannelNum, _newValue);
            }
            /// <summary>
            /// returns all values that are pressed in the supplied channel
            /// </summary>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <returns>
            /// a string list with all PLC paths
            /// </returns>
            bool CommType.GetAllPaths(string _channelName)
            {
                Channel myChannel = Channels.GetChannel(_channelName);
                CLX_Communications.PLC_RefreshData(myChannel.ChannelNumber);
                MyABPrograms[myChannel.ChannelNumber] = CLX_Communications.PLC_GetProgramNames(myChannel.ChannelNumber);


                myChannel.CurrentPaths = new List<string>();
                myChannel.CurrentPaths.AddRange(CLX_Communications.PLC_GetVariableNames(_channelName, myChannel.ChannelNumber, "", true, true, true, "", ""));
                // PLCCommunications.PLC_GetVariableNames(_channel, MyPrograms[0], true, true, "", "");
                return true;
            }
        }
        #endregion
        //#region OPC Definition
        ///// <summary>
        ///// The object that controls the code-sys communication
        ///// </summary>
        ///// <seealso cref="PLC.CommManager.iCommObj" />
        //internal class CommOPC_Obj : iCommObj
        //{
        //    /// <summary>
        //    /// My type
        //    /// </summary>
        //    eCommDriver myType;
        //    /// <summary>
        //    /// Constructor
        //    /// </summary>
        //    public CommOPC_Obj()
        //    {
        //        myType = eCommDriver.OPC;
        //    }
        //    /// <summary>
        //    /// Gets the value.
        //    /// </summary>
        //    /// <param name="_PLCPath">The _ PLC path.</param>
        //    /// <param name="_channelName">Name of the _channel.</param>
        //    /// <param name="_value">if set to <c>true</c> [_value].</param>
        //    /// <returns></returns>
        //    bool iCommObj.GetValue(string _PLCPath, string _channelName, out bool _value)
        //    {

        //        string ServerType = Channels.GetChannel(_channelName).OPCServerType;
        //        return OPCCommunication.OPC_ReadTagSpecific(_PLCPath, ServerType, out _value);
        //    }
        //    /// <summary>
        //    /// Gets the value.
        //    /// </summary>
        //    /// <param name="_PLCPath">The _ PLC path.</param>
        //    /// <param name="_channelName">Name of the _channel.</param>
        //    /// <param name="_value">The _value.</param>
        //    /// <returns></returns>
        //    bool iCommObj.GetValue(string _PLCPath, string _channelName, out string _value)
        //    {

        //        string ServerType = Channels.GetChannel(_channelName).OPCServerType;
        //        return OPCCommunication.OPC_ReadTagSpecific(_PLCPath, ServerType, out _value);
        //    }
        //    /// <summary>
        //    /// Gets the value.
        //    /// </summary>
        //    /// <param name="_PLCPath">The _ PLC path.</param>
        //    /// <param name="_channelName">Name of the _channel.</param>
        //    /// <param name="_value">The _value.</param>
        //    /// <returns></returns>
        //    bool iCommObj.GetValue(string _PLCPath, string _channelName, out Int32 _value)
        //    {

        //        string ServerType = Channels.GetChannel(_channelName).OPCServerType;
        //        return OPCCommunication.OPC_ReadTagSpecific(_PLCPath, ServerType, out _value);
        //    }
        //    /// <summary>
        //    /// Gets the value.
        //    /// </summary>
        //    /// <param name="_PLCPath">The _ PLC path.</param>
        //    /// <param name="_channelName">Name of the _channel.</param>
        //    /// <param name="_value">The _value.</param>
        //    /// <returns></returns>
        //    bool iCommObj.GetValue(string _PLCPath, string _channelName, out double _value)
        //    {

        //        string ServerType = Channels.GetChannel(_channelName).OPCServerType;
        //        return OPCCommunication.OPC_ReadTagSpecific(_PLCPath, ServerType, out _value);
        //    }

        //    /// <summary>
        //    /// Sets the value.
        //    /// </summary>
        //    /// <param name="_PLCPath">The _ PLC path.</param>
        //    /// <param name="_channelName">Name of the _channel.</param>
        //    /// <param name="_newValue">if set to <c>true</c> [_new value].</param>
        //    /// <returns></returns>
        //    bool iCommObj.SetValue(string _PLCPath, string _channelName, bool _newValue)
        //    {
        //        string ServerType = Channels.GetChannel(_channelName).OPCServerType;
        //        return OPCCommunication.OPC_WriteTagSpecific(_PLCPath, ServerType, _newValue);
        //    }
        //    /// <summary>
        //    /// Sets the value.
        //    /// </summary>
        //    /// <param name="_PLCPath">The _ PLC path.</param>
        //    /// <param name="_channelName">Name of the _channel.</param>
        //    /// <param name="_newValue">The _new value.</param>
        //    /// <returns></returns>
        //    bool iCommObj.SetValue(string _PLCPath, string _channelName, string _newValue)
        //    {
        //        string ServerType = Channels.GetChannel(_channelName).OPCServerType;
        //        return OPCCommunication.OPC_WriteTagSpecific(_PLCPath, ServerType, _newValue);
        //    }
        //    /// <summary>
        //    /// Sets the value.
        //    /// </summary>
        //    /// <param name="_PLCPath">The _ PLC path.</param>
        //    /// <param name="_channelName">Name of the _channel.</param>
        //    /// <param name="_newValue">The _new value.</param>
        //    /// <returns></returns>
        //    bool iCommObj.SetValue(string _PLCPath, string _channelName, Int32 _newValue)
        //    {
        //        string ServerType = Channels.GetChannel(_channelName).OPCServerType;
        //        return OPCCommunication.OPC_WriteTagSpecific(_PLCPath, ServerType, _newValue);
        //    }
        //    /// <summary>
        //    /// Sets the value.
        //    /// </summary>
        //    /// <param name="_PLCPath">The _ PLC path.</param>
        //    /// <param name="_channelName">Name of the _channel.</param>
        //    /// <param name="_newValue">The _new value.</param>
        //    /// <returns></returns>
        //    bool iCommObj.SetValue(string _PLCPath, string _channelName, double _newValue)
        //    {
        //        string ServerType = Channels.GetChannel(_channelName).OPCServerType;
        //        return OPCCommunication.OPC_WriteTagSpecific(_PLCPath, ServerType, _newValue);
        //    }
        //    /// <summary>
        //    /// Gets all paths.
        //    /// </summary>
        //    /// <param name="_channelName">Name of the _channel.</param>
        //    /// <returns></returns>
        //    bool iCommObj.GetAllPaths(string _channelName)
        //    {
        //        Channel myChannel = Channels.GetChannel(_channelName);

        //        string ServerType = Channels.GetChannel(_channelName).OPCServerType;

        //        myChannel.CurrentPaths = new List<string>();
        //        myChannel.CurrentPaths.AddRange(OPCCommunication.FindOPCTagNames(_channelName, ServerType));
        //        return true;
        //    }
        //}

        //#endregion

        #region AB Definition SLCMLC
        /// <summary>
        /// The object that controls the Allen Bradley communication
        /// </summary>
        /// <seealso cref="PLC.CommManager.CommType" />
        internal class CommAllenBradley_SLCMLC_Obj : CommType
        {
            /// <summary>
            /// My PLC type
            /// </summary>
            PLCDriverType myPLC_Type;
            /// <summary>
            /// Constructor
            /// </summary>
            public CommAllenBradley_SLCMLC_Obj()
            {
                myPLC_Type = PLCDriverType.AllenBradley_SLCMLC;
            }
            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <param name="_PLCPath">The _ PLC path.</param>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <param name="_value">if set to <c>true</c> [_value].</param>
            /// <returns></returns>
            bool CommType.GetValue(string _PLCPath, string _channelName, out bool _value)
            {
                // Underneath is only for testing purpose
                _value = false;
                int ChannelNum = Channels.GetChannel(_channelName).ChannelNumber;
                return SLCMLC_Communications.PLC_Read_Specific(_PLCPath, ChannelNum, out _value);
            }
            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <param name="_PLCPath">The _ PLC path.</param>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <param name="_value">The _value.</param>
            /// <returns></returns>
            bool CommType.GetValue(string _PLCPath, string _channelName, out string _value)
            {
                // Underneath is only for testing purpose
                _value = "";
                int ChannelNum = Channels.GetChannel(_channelName).ChannelNumber;
                return SLCMLC_Communications.PLC_Read_Specific(_PLCPath, ChannelNum, out _value);
            }
            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <param name="_PLCPath">The _ PLC path.</param>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <param name="_value">The _value.</param>
            /// <returns></returns>
            bool CommType.GetValue(string _PLCPath, string _channelName, out Int32 _value)
            {
                // Underneath is only for testing purpose
                _value = 0;
                int ChannelNum = Channels.GetChannel(_channelName).ChannelNumber;
                return SLCMLC_Communications.PLC_Read_Specific(_PLCPath, ChannelNum, out _value);
            }
            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <param name="_PLCPath">The _ PLC path.</param>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <param name="_value">The _value.</param>
            /// <returns></returns>
            bool CommType.GetValue(string _PLCPath, string _channelName, out double _value)
            {
                // Underneath is only for testing purpose
                _value = 0;
                int ChannelNum = Channels.GetChannel(_channelName).ChannelNumber;
                return SLCMLC_Communications.PLC_Read_Specific(_PLCPath, ChannelNum, out _value);
            }

            /// <summary>
            /// Set a value in the PLC, the system checks if the supplied channel is correct
            /// </summary>
            /// <param name="_PLCPath">The path to set</param>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <param name="_newValue">The value to set</param>
            /// <returns>
            /// True if success
            /// </returns>
            bool CommType.SetValue(string _PLCPath, string _channelName, bool _newValue)
            {
                int ChannelNum = Channels.GetChannel(_channelName).ChannelNumber;
                return SLCMLC_Communications.PLC_Write_Specific(_PLCPath, ChannelNum, _newValue);
            }
            /// <summary>
            /// Sets the value.
            /// </summary>
            /// <param name="_PLCPath">The _ PLC path.</param>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <param name="_newValue">The _new value.</param>
            /// <returns></returns>
            bool CommType.SetValue(string _PLCPath, string _channelName, string _newValue)
            {
                int ChannelNum = Channels.GetChannel(_channelName).ChannelNumber;
                return SLCMLC_Communications.PLC_Write_Specific(_PLCPath, ChannelNum, _newValue);
            }
            /// <summary>
            /// Sets the value.
            /// </summary>
            /// <param name="_PLCPath">The _ PLC path.</param>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <param name="_newValue">The _new value.</param>
            /// <returns></returns>
            bool CommType.SetValue(string _PLCPath, string _channelName, Int32 _newValue)
            {
                int ChannelNum = Channels.GetChannel(_channelName).ChannelNumber;
                return SLCMLC_Communications.PLC_Write_Specific(_PLCPath, ChannelNum, _newValue);
            }
            /// <summary>
            /// Sets the value.
            /// </summary>
            /// <param name="_PLCPath">The _ PLC path.</param>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <param name="_newValue">The _new value.</param>
            /// <returns></returns>
            bool CommType.SetValue(string _PLCPath, string _channelName, double _newValue)
            {
                int ChannelNum = Channels.GetChannel(_channelName).ChannelNumber;
                return SLCMLC_Communications.PLC_Write_Specific(_PLCPath, ChannelNum, _newValue);
            }
            /// <summary>
            /// returns all values that are pressed in the supplied channel
            /// </summary>
            /// <param name="_channelName">Name of the _channel.</param>
            /// <returns>
            /// a string list with all PLC paths
            /// </returns>
            bool CommType.GetAllPaths(string _channelName)
            {
                Channel myChannel = Channels.GetChannel(_channelName);
                SLCMLC_Communications.PLC_RefreshData(myChannel.ChannelNumber);
                MyABPrograms[myChannel.ChannelNumber] = SLCMLC_Communications.PLC_GetProgramNames(myChannel.ChannelNumber);


                myChannel.CurrentPaths = new List<string>();
                myChannel.CurrentPaths.AddRange(SLCMLC_Communications.PLC_GetVariableNames(_channelName, myChannel.ChannelNumber, "", true, true, true, "", ""));
                // PLCCommunications.PLC_GetVariableNames(_channel, MyPrograms[0], true, true, "", "");
                return true;
            }
        }
        #endregion
        #endregion
    }

}
#region Type Declarations


/// <summary>
/// 
/// </summary>
public enum CommStatus
{
    /// <summary>
    /// The initialting
    /// </summary>
    Initialting,
    /// <summary>
    /// The established
    /// </summary>
    Established,
    /// <summary>
    /// The slow
    /// </summary>
    Slow,
    /// <summary>
    /// The failed
    /// </summary>
    Failed,
    /// <summary>
    /// The offline
    /// </summary>
    OFFLINE,
    /// <summary>
    /// The unknown
    /// </summary>
    Unknown
}

#endregion

