Imports System.Collections.Generic
Imports System.Xml.Serialization
Imports ABDriver

Imports Common

Namespace PLC

    ''' <summary>
    ''' Used for the communication requests
    ''' </summary>
    Public NotInheritable Class CommManager
        Private Sub New()
        End Sub


        ''' <summary>
        ''' The current channels
        ''' </summary>
        Public Shared CurrentChannels As List(Of [String]) = New List(Of String)()
        ''' <summary>
        ''' My ab programs
        ''' </summary>
        Public Shared MyABPrograms As List(Of String)() = New List(Of String)(9) {}
        ''' <summary>
        ''' The opc data errors
        ''' </summary>
        Public Shared OPCDataErrors As Integer = 0
        ''' <summary>
        ''' The ab data errors
        ''' </summary>
        Public Shared ABDataErrors As Integer = 0

        ''' <summary>
        ''' The comms active
        ''' </summary>
        Public Shared CommsActive As Boolean = False


#Region "External Requests"

        ''' <summary>
        ''' Finds the full address.
        ''' </summary>
        ''' <param name="_addressFragment">The _address fragment.</param>
        ''' <returns>
        ''' Returns the Entire address from the provided address fragment
        ''' </returns>
        Public Shared Function FindFullAddress(_addressFragment As String) As String
            Dim found As String = ""
            For Each channel As Channel In Channels.MyChannelsList
                found = channel.CurrentPaths.Find(Function(Raw) Raw.Contains(_addressFragment))
                If Not (found = "") Then
                    Exit For

                End If
            Next
            Return found
        End Function

#Region "Get PLC Data"
        ''' <summary>
        ''' Gets the PLC value.
        ''' </summary>
        ''' <param name="_PLCPath">The PLC path.</param>
        ''' <param name="_value">if set to <c>true</c> [value].</param>
        ''' <returns></returns>
        Public Shared Function GetPLCValue(_PLCPath As String, ByRef _value As Boolean) As Boolean
            Dim ChannelName As String = ABCore.FindChannelName(_PLCPath)
            Return Channels.GetChannel(ChannelName).CommObject.GetValue(_PLCPath, ChannelName, _value)
        End Function
        ''' <summary>
        ''' Gets the PLC value.
        ''' </summary>
        ''' <param name="_PLCPath">The PLC path.</param>
        ''' <param name="_value">The value.</param>
        ''' <returns></returns>
        Public Shared Function GetPLCValue(_PLCPath As String, ByRef _value As String) As Boolean
            Dim ChannelName As String = ABCore.FindChannelName(_PLCPath)
            Return Channels.GetChannel(ChannelName).CommObject.GetValue(_PLCPath, ChannelName, _value)
        End Function
        ''' <summary>
        ''' Gets the PLC value.
        ''' </summary>
        ''' <param name="_PLCPath">The PLC path.</param>
        ''' <param name="_value">The value.</param>
        ''' <returns></returns>
        Public Shared Function GetPLCValue(_PLCPath As String, ByRef _value As Int32) As Boolean
            Dim ChannelName As String = ABCore.FindChannelName(_PLCPath)
            Return Channels.GetChannel(ChannelName).CommObject.GetValue(_PLCPath, ChannelName, _value)
        End Function
        ''' <summary>
        ''' Gets the PLC value.
        ''' </summary>
        ''' <param name="_PLCPath">The PLC path.</param>
        ''' <param name="_value">The value.</param>
        ''' <returns></returns>
        Public Shared Function GetPLCValue(_PLCPath As String, ByRef _value As Double) As Boolean
            Dim ChannelName As String = ABCore.FindChannelName(_PLCPath)
            Return Channels.GetChannel(ChannelName).CommObject.GetValue(_PLCPath, ChannelName, _value)
        End Function
        ''' <summary>
        ''' Gets the PLC value bool.
        ''' </summary>
        ''' <param name="_PLCPath">The PLC path.</param>
        ''' <param name="Success">if set to <c>true</c> [success].</param>
        ''' <returns></returns>
        Public Shared Function GetPLCValue_BOOL(_PLCPath As String, ByRef Success As Boolean) As Boolean
            Dim _value As Boolean = False
            Dim ChannelName As String = ABCore.FindChannelName(_PLCPath)
            Success = Channels.GetChannel(ChannelName).CommObject.GetValue(_PLCPath, ChannelName, _value)
            Return _value
        End Function
        ''' <summary>
        ''' Gets the PLC value string.
        ''' </summary>
        ''' <param name="_PLCPath">The PLC path.</param>
        ''' <param name="Success">if set to <c>true</c> [success].</param>
        ''' <returns></returns>
        Public Shared Function GetPLCValue_STRING(_PLCPath As String, ByRef Success As Boolean) As String
            Dim _value As String = ""
            Dim ChannelName As String = ABCore.FindChannelName(_PLCPath)
            Success = Channels.GetChannel(ChannelName).CommObject.GetValue(_PLCPath, ChannelName, _value)
            Return _value
        End Function
        ''' <summary>
        ''' Gets the PLC value in T32.
        ''' </summary>
        ''' <param name="_PLCPath">The PLC path.</param>
        ''' <param name="Success">if set to <c>true</c> [success].</param>
        ''' <returns></returns>
        Public Shared Function GetPLCValue_INT32(_PLCPath As String, ByRef Success As Boolean) As Int32
            Dim _value As Int32 = 0
            Dim ChannelName As [String] = ABCore.FindChannelName(_PLCPath)
            Success = Channels.GetChannel(ChannelName).CommObject.GetValue(_PLCPath, ChannelName, _value)
            Return _value
        End Function
        ''' <summary>
        ''' Gets the PLC value double.
        ''' </summary>
        ''' <param name="_PLCPath">The PLC path.</param>
        ''' <param name="Success">if set to <c>true</c> [success].</param>
        ''' <returns></returns>
        Public Shared Function GetPLCValue_DOUBLE(_PLCPath As String, ByRef Success As Boolean) As Double
            Dim _value As Double = 0
            Dim ChannelName As [String] = ABCore.FindChannelName(_PLCPath)
            Success = Channels.GetChannel(ChannelName).CommObject.GetValue(_PLCPath, ChannelName, _value)
            Return _value
        End Function
#End Region
#Region "Set PLC Data"
        ''' <summary>
        ''' Set a value in the PLC, the system checks if the supplied channel is correct
        ''' </summary>
        ''' <param name="_PLCPath">The path to set</param>
        ''' <param name="_newValue">The value to set</param>
        ''' <returns>
        ''' True if success
        ''' </returns>
        Public Shared Function SetPLCValue(_PLCPath As String, _newValue As Boolean) As Boolean
            Dim ChannelName As String = ABCore.FindChannelName(_PLCPath)
            Return Channels.GetChannel(ChannelName).CommObject.SetValue(_PLCPath, ChannelName, _newValue)
        End Function
        ''' <summary>
        ''' Set a value in the PLC, the system checks if the supplied channel is correct
        ''' </summary>
        ''' <param name="_PLCPath">The path to set</param>
        ''' <param name="_newValue">The value to set</param>
        ''' <returns>
        ''' True if success
        ''' </returns>
        Public Shared Function SetPLCValue(_PLCPath As String, _newValue As String) As Boolean
            Dim ChannelName As String = ABCore.FindChannelName(_PLCPath)
            Return Channels.GetChannel(ChannelName).CommObject.SetValue(_PLCPath, ChannelName, _newValue)
        End Function
        ''' <summary>
        ''' Set a value in the PLC, the system checks if the supplied channel is correct
        ''' </summary>
        ''' <param name="_PLCPath">The path to set</param>
        ''' <param name="_newValue">The value to set</param>
        ''' <returns>
        ''' True if success
        ''' </returns>
        Public Shared Function SetPLCValue(_PLCPath As String, _newValue As Int32) As Boolean
            Dim ChannelName As String = ABCore.FindChannelName(_PLCPath)
            Return Channels.GetChannel(ChannelName).CommObject.SetValue(_PLCPath, ChannelName, _newValue)
        End Function
        ''' <summary>
        ''' Set a value in the PLC, the system checks if the supplied channel is correct
        ''' </summary>
        ''' <param name="_PLCPath">The path to set</param>
        ''' <param name="_newValue">The value to set</param>
        ''' <returns>
        ''' True if success
        ''' </returns>
        Public Shared Function SetPLCValue(_PLCPath As String, _newValue As Double) As Boolean
            Dim ChannelName As String = ABCore.FindChannelName(_PLCPath)
            Return Channels.GetChannel(ChannelName).CommObject.SetValue(_PLCPath, ChannelName, _newValue)
        End Function



#End Region
        ''' <summary>
        ''' Gets all PLC paths.
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function GetAllPLCPaths() As Boolean
            Dim xSuccess As Boolean = True
            Dim xTempSuccess As Boolean = False
            For Each MyChannel As String In CurrentChannels
                xTempSuccess = Channels.GetChannel(MyChannel).CommObject.GetAllPaths(MyChannel)
                If Not xTempSuccess Then
                    xSuccess = False
                End If
            Next

            Return xSuccess
        End Function

        ''' <summary>
        ''' Channels the enable.
        ''' </summary>
        ''' <param name="_ChannelName">Name of the channel.</param>
        ''' <returns></returns>
        Public Shared Function ChannelEnable(_ChannelName As [String]) As Boolean
            Dim EnabledStatus As Boolean = False
            Try
                Dim Channel As Integer = Channels.GetChannel(_ChannelName).ChannelNumber
                For Each MyChannel As Channel In Channels.ChannelsList
                    If MyChannel.ChannelNumber = Channel Then
                        EnabledStatus = MyChannel.Enabled
                        Exit For
                    End If
                Next
                ' Channel not Found
            Catch
            End Try
            Return EnabledStatus
        End Function

        '''// <summary>
        '''// Finds the opc servers.
        '''// Must be passed either "DA_Server" or "UA_Server"
        '''// </summary>
        '''// <param name="_serverType">Type of the _server.</param>
        '''// <returns></returns>
        'public static List<string> FindOPCServers(string _serverType)
        '{
        '    return OPCCommunication.FindChannelNames(_serverType);
        '}
        ''' <summary>
        ''' Shuts down ab driver.
        ''' </summary>
        Public Shared Sub ShutDownABDriver()
            CLX_Communications.Shudown()
        End Sub

#End Region
#Region "Channel Definition"
        ''' <summary>
        ''' Holds all information about all channels,
        ''' </summary>
        Public NotInheritable Class Channels
            Private Sub New()
            End Sub
            ''' sealed class can not be inherited
            ''' <summary>
            ''' The channels list
            ''' </summary>
            Public Shared ChannelsList As New List(Of Channel)()
            ''' <summary>
            ''' Initializes the <see cref="Channels" /> class.
            ''' </summary>
            Shared Sub New()
                ' Initialization at startup
                ChannelsList = New List(Of Channel)()
            End Sub
            ''' <summary>
            ''' Gets or sets my channels list.
            ''' </summary>
            ''' <value>
            ''' My channels list.
            ''' </value>
            Public Shared Property MyChannelsList() As List(Of Channel)
                Get
                    Return ChannelsList
                End Get
                Set
                    ChannelsList = Value
                End Set
            End Property

            ''' <summary>
            ''' Add a channel to the system
            ''' </summary>
            ''' <param name="_myChannel">My channel.</param>
            Public Shared Sub AddChannel(_myChannel As Channel)
                ChannelsList.Add(_myChannel)
                If ChannelsList.Count = 0 Then
                    CurrentChannels = New List(Of String)()
                End If
                CurrentChannels.Add(_myChannel.Name)
            End Sub

            ''' <summary>
            ''' Removes the channel.
            ''' </summary>
            ''' <param name="_byName">Name of the _by.</param>
            ''' <returns></returns>
            Public Shared Function RemoveChannel(_byName As String) As [Boolean]
                Dim index As Integer = ChannelsList.FindIndex(Function(F) F.Name.Equals(_byName, StringComparison.Ordinal))
                If index <> -1 Then
                    ChannelsList.RemoveAt(index)
                    CurrentChannels.Remove(_byName)
                    Return True
                End If
                Return False
            End Function

            ''' <summary>
            ''' Gets a specified channel
            ''' </summary>
            ''' <param name="_byName">The name of the channel</param>
            ''' <returns>
            ''' The channel requested
            ''' </returns>
            Public Shared Function GetChannel(_byName As String) As Channel
                Dim returnChannel As Channel = Nothing
                returnChannel = ChannelsList.Find(Function(F) F.Name.Equals(_byName, StringComparison.Ordinal))
                If returnChannel IsNot Nothing Then
                    Return returnChannel
                Else
                    Return returnChannel
                End If
            End Function
        End Class

        ''' <summary>
        ''' Hold the information needed to connect to the PLC
        ''' </summary>
        Public Class Channel

            ''' <summary>
            ''' The name
            ''' </summary>
            <XmlAttribute>
            Public Name As String = ""
            ''' <summary>
            ''' The channel number
            ''' </summary>
            <XmlAttribute>
            Public ChannelNumber As Integer
            ''' <summary>
            ''' The enabled
            ''' </summary>
            <XmlAttribute>
            Public Enabled As [Boolean]
            ''' <summary>
            ''' The initialized
            ''' </summary>
            Public Initialized As [Boolean]
            ''' <summary>
            ''' The ip adress
            ''' </summary>
            <XmlAttribute>
            Public IPAdress As String = ""
            ''' <summary>
            ''' The slot
            ''' </summary>
            <XmlAttribute>
            Public Slot As String = "0"

            ' Added for SLCMLC
            Public CPUType As Int32 = 4
            'PLC Type: 1=PLC-5, 2=SLC, 3=MLC, 4=CLX MemMap "


            ''' <summary>
            ''' The port number
            ''' </summary>
            <XmlAttribute>
            Public PortNumber As Integer
            ''' <summary>
            ''' The opc server type
            ''' </summary>
            <XmlAttribute>
            Public OPCServerType As String = "No Type Set"
            ''' <summary>
            ''' The time out time
            ''' </summary>
            <XmlAttribute>
            Public TimeOutTime As Integer
            ' in ms
            ''' <summary>
            ''' The comm check address
            ''' </summary>
            <XmlAttribute>
            Public CommCheckAddress As String = ""
            ''' <summary>
            ''' The comm events
            ''' </summary>
            Public CommEvents As New List(Of Int32)()
            ''' <summary>
            ''' The status
            ''' </summary>
            Public Status As New CommStatus()
            ''' <summary>
            ''' The login data
            ''' </summary>
            Public LoginData As UserLogin
            ''' <summary>
            ''' The driver type
            ''' </summary>
            <XmlAttribute>
            Public DriverType As PLCDriverType
            ''' <summary>
            ''' The comm object
            ''' </summary>
            <System.Xml.Serialization.XmlIgnore>
            Friend CommObject As CommType
            ' This is the object that will be used to connect to the PLC
            ''' <summary>
            ''' The current paths
            ''' </summary>
            Public CurrentPaths As List(Of [String]) = New List(Of String)()
            ''' <summary>
            ''' Empty constructor
            ''' </summary>
            Public Sub New()
                ' this is only used by the serializer 
                InitilizeCommDriver()
            End Sub
            ''' <summary>
            ''' Constructor for adding new channels (normally u use this one for adding an channel to channels
            ''' </summary>
            ''' <param name="_myDriverType">Type of my driver.</param>
            ''' <param name="_myName">My name.</param>
            ''' <param name="_IsEnabled">if set to <c>true</c> [is enabled].</param>
            ''' <param name="_myChannelNumber">My channel number.</param>
            ''' <param name="_myOPCServerType">Type of my opc server.</param>
            ''' <param name="_myIPAdress">My ip adress.</param>
            ''' <param name="_mySlot">My slot.</param>
            ''' <param name="_myTimeOut">My time out.</param>
            ''' <param name="_myCommCheckAddress">My comm check address.</param>
            Public Sub New(_myDriverType As PLCDriverType, _myName As String, _myIPAdress As String, _mySlot As String, _myTimeOut As Integer, _myCPUTType As Int32)
                DriverType = _myDriverType
                CPUType = _myCPUTType
                Name = _myName
                Enabled = True
                IPAdress = _myIPAdress
                ' some bastard resisted testing would be in place here
                Slot = _mySlot
                TimeOutTime = _myTimeOut


                ' add the other Channel members here
                ' keep this at the end
                ChannelNumber = InitilizeCommDriver()
            End Sub

            ''' <summary>
            ''' Initialize the requested driver
            ''' </summary>
            Private Function InitilizeCommDriver() As Int32
                If DriverType = PLCDriverType.AllenBradley_CLX Then
                    CommObject = New CommAllenBradley_CLX_Obj()
                    Return CLX_Communications.CommInit(IPAdress, Slot, TimeOutTime)
                End If
                'if (DriverType == eCommDriver.OPC)
                '    CommObject = new CommOPC_Obj();
                If DriverType = PLCDriverType.AllenBradley_SLCMLC Then
                    CommObject = New CommAllenBradley_SLCMLC_Obj()
                    Return SLCMLC_Communications.CommInit(IPAdress, Slot, TimeOutTime, CPUType)
                End If
                Return -1
            End Function

            ''' <summary>
            ''' Structure for login info
            ''' </summary>
            Public Structure UserLogin
                ''' <summary>
                ''' The is required
                ''' </summary>
                <XmlAttribute>
                Public IsRequired As [Boolean]
                ' is login required yes/no
                ''' <summary>
                ''' The user name
                ''' </summary>
                <XmlAttribute>
                Public userName As [String]
                ''' <summary>
                ''' The password
                ''' </summary>
                <XmlAttribute>
                Public password As [String]
            End Structure
        End Class
#End Region

#Region "Comm Interface Definition"
        ''' <summary>
        ''' Enum for the comm types
        ''' </summary>
        Public Enum PLCDriverType
            ''' <summary>
            ''' The allen bradley
            ''' </summary>
            AllenBradley_CLX
            ''' <summary>
            ''' The opc
            ''' </summary>
            OPC
            AllenBradley_SLCMLC
        End Enum

        ''' <summary>
        ''' Interface of the communication objects
        ''' </summary>
        Friend Interface CommType
            ''' <summary>
            ''' Gets the value.
            ''' </summary>
            ''' <param name="PLCPath">The PLC path.</param>
            ''' <param name="_channelName">Name of the channel.</param>
            ''' <param name="_value">if set to <c>true</c> [value].</param>
            ''' <returns></returns>
            Function GetValue(PLCPath As String, _channelName As String, ByRef _value As Boolean) As Boolean
            ''' <summary>
            ''' Gets the value.
            ''' </summary>
            ''' <param name="PLCPath">The PLC path.</param>
            ''' <param name="_channelName">Name of the channel.</param>
            ''' <param name="_value">The value.</param>
            ''' <returns></returns>
            Function GetValue(PLCPath As String, _channelName As String, ByRef _value As String) As Boolean
            ''' <summary>
            ''' Gets the value.
            ''' </summary>
            ''' <param name="PLCPath">The PLC path.</param>
            ''' <param name="_channelName">Name of the channel.</param>
            ''' <param name="_value">The value.</param>
            ''' <returns></returns>
            Function GetValue(PLCPath As String, _channelName As String, ByRef _value As Int32) As Boolean
            ''' <summary>
            ''' Gets the value.
            ''' </summary>
            ''' <param name="PLCPath">The PLC path.</param>
            ''' <param name="_channelName">Name of the channel.</param>
            ''' <param name="_value">The value.</param>
            ''' <returns></returns>
            Function GetValue(PLCPath As String, _channelName As String, ByRef _value As Double) As Boolean
            ''' <summary>
            ''' Sets the value.
            ''' </summary>
            ''' <param name="PLCPath">The PLC path.</param>
            ''' <param name="_channelName">Name of the channel.</param>
            ''' <param name="newValue">if set to <c>true</c> [new value].</param>
            ''' <returns></returns>
            Function SetValue(PLCPath As String, _channelName As String, newValue As Boolean) As Boolean
            ''' <summary>
            ''' Sets the value.
            ''' </summary>
            ''' <param name="PLCPath">The PLC path.</param>
            ''' <param name="_channelName">Name of the channel.</param>
            ''' <param name="newValue">The new value.</param>
            ''' <returns></returns>
            Function SetValue(PLCPath As String, _channelName As String, newValue As String) As Boolean
            ''' <summary>
            ''' Sets the value.
            ''' </summary>
            ''' <param name="PLCPath">The PLC path.</param>
            ''' <param name="_channelName">Name of the channel.</param>
            ''' <param name="newValue">The new value.</param>
            ''' <returns></returns>
            Function SetValue(PLCPath As String, _channelName As String, newValue As Int32) As Boolean
            ''' <summary>
            ''' Sets the value.
            ''' </summary>
            ''' <param name="PLCPath">The PLC path.</param>
            ''' <param name="_channelName">Name of the channel.</param>
            ''' <param name="newValue">The new value.</param>
            ''' <returns></returns>
            Function SetValue(PLCPath As String, _channelName As String, newValue As Double) As Boolean
            ''' <summary>
            ''' Gets all paths.
            ''' </summary>
            ''' <param name="channelName">Name of the channel.</param>
            ''' <returns></returns>
            Function GetAllPaths(channelName As String) As Boolean

        End Interface

#Region "AB Definition CLX"
        ''' <summary>
        ''' The object that controls the Allen Bradley communication
        ''' </summary>
        ''' <seealso cref="PLC.CommManager.CommType" />
        Friend Class CommAllenBradley_CLX_Obj
            Inherits CommType
            ''' <summary>
            ''' My PLC type
            ''' </summary>
            Private myPLC_Type As PLCDriverType
            ''' <summary>
            ''' Constructor
            ''' </summary>
            Public Sub New()
                myPLC_Type = PLCDriverType.AllenBradley_CLX
            End Sub
            ''' <summary>
            ''' Gets the value.
            ''' </summary>
            ''' <param name="_PLCPath">The _ PLC path.</param>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <param name="_value">if set to <c>true</c> [_value].</param>
            ''' <returns></returns>
            Private Function CommType_GetValue(_PLCPath As String, _channelName As String, ByRef _value As Boolean) As Boolean Implements CommType.GetValue
                ' Underneath is only for testing purpose
                _value = False
                Dim ChannelNum As Integer = Channels.GetChannel(_channelName).ChannelNumber
                Return CLX_Communications.PLC_Read_Specific(_PLCPath, ChannelNum, _value)
            End Function
            ''' <summary>
            ''' Gets the value.
            ''' </summary>
            ''' <param name="_PLCPath">The _ PLC path.</param>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <param name="_value">The _value.</param>
            ''' <returns></returns>
            Private Function CommType_GetValue(_PLCPath As String, _channelName As String, ByRef _value As String) As Boolean Implements CommType.GetValue
                ' Underneath is only for testing purpose
                _value = ""
                Dim ChannelNum As Integer = Channels.GetChannel(_channelName).ChannelNumber
                Return CLX_Communications.PLC_Read_Specific(_PLCPath, ChannelNum, _value)
            End Function
            ''' <summary>
            ''' Gets the value.
            ''' </summary>
            ''' <param name="_PLCPath">The _ PLC path.</param>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <param name="_value">The _value.</param>
            ''' <returns></returns>
            Private Function CommType_GetValue(_PLCPath As String, _channelName As String, ByRef _value As Int32) As Boolean Implements CommType.GetValue
                ' Underneath is only for testing purpose
                _value = 0
                Dim ChannelNum As Integer = Channels.GetChannel(_channelName).ChannelNumber
                Return CLX_Communications.PLC_Read_Specific(_PLCPath, ChannelNum, _value)
            End Function
            ''' <summary>
            ''' Gets the value.
            ''' </summary>
            ''' <param name="_PLCPath">The _ PLC path.</param>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <param name="_value">The _value.</param>
            ''' <returns></returns>
            Private Function CommType_GetValue(_PLCPath As String, _channelName As String, ByRef _value As Double) As Boolean Implements CommType.GetValue
                ' Underneath is only for testing purpose
                _value = 0
                Dim ChannelNum As Integer = Channels.GetChannel(_channelName).ChannelNumber
                Return CLX_Communications.PLC_Read_Specific(_PLCPath, ChannelNum, _value)
            End Function

            ''' <summary>
            ''' Set a value in the PLC, the system checks if the supplied channel is correct
            ''' </summary>
            ''' <param name="_PLCPath">The path to set</param>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <param name="_newValue">The value to set</param>
            ''' <returns>
            ''' True if success
            ''' </returns>
            Private Function CommType_SetValue(_PLCPath As String, _channelName As String, _newValue As Boolean) As Boolean Implements CommType.SetValue
                Dim ChannelNum As Integer = Channels.GetChannel(_channelName).ChannelNumber
                Return CLX_Communications.PLC_Write_Specific(_PLCPath, ChannelNum, _newValue)
            End Function
            ''' <summary>
            ''' Sets the value.
            ''' </summary>
            ''' <param name="_PLCPath">The _ PLC path.</param>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <param name="_newValue">The _new value.</param>
            ''' <returns></returns>
            Private Function CommType_SetValue(_PLCPath As String, _channelName As String, _newValue As String) As Boolean Implements CommType.SetValue
                Dim ChannelNum As Integer = Channels.GetChannel(_channelName).ChannelNumber
                Return CLX_Communications.PLC_Write_Specific(_PLCPath, ChannelNum, _newValue)
            End Function
            ''' <summary>
            ''' Sets the value.
            ''' </summary>
            ''' <param name="_PLCPath">The _ PLC path.</param>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <param name="_newValue">The _new value.</param>
            ''' <returns></returns>
            Private Function CommType_SetValue(_PLCPath As String, _channelName As String, _newValue As Int32) As Boolean Implements CommType.SetValue
                Dim ChannelNum As Integer = Channels.GetChannel(_channelName).ChannelNumber
                Return CLX_Communications.PLC_Write_Specific(_PLCPath, ChannelNum, _newValue)
            End Function
            ''' <summary>
            ''' Sets the value.
            ''' </summary>
            ''' <param name="_PLCPath">The _ PLC path.</param>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <param name="_newValue">The _new value.</param>
            ''' <returns></returns>
            Private Function CommType_SetValue(_PLCPath As String, _channelName As String, _newValue As Double) As Boolean Implements CommType.SetValue
                Dim ChannelNum As Integer = Channels.GetChannel(_channelName).ChannelNumber
                Return CLX_Communications.PLC_Write_Specific(_PLCPath, ChannelNum, _newValue)
            End Function
            ''' <summary>
            ''' returns all values that are pressed in the supplied channel
            ''' </summary>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <returns>
            ''' a string list with all PLC paths
            ''' </returns>
            Private Function CommType_GetAllPaths(_channelName As String) As Boolean Implements CommType.GetAllPaths
                Dim myChannel As Channel = Channels.GetChannel(_channelName)
                CLX_Communications.PLC_RefreshData(myChannel.ChannelNumber)
                MyABPrograms(myChannel.ChannelNumber) = CLX_Communications.PLC_GetProgramNames(myChannel.ChannelNumber)


                myChannel.CurrentPaths = New List(Of String)()
                myChannel.CurrentPaths.AddRange(CLX_Communications.PLC_GetVariableNames(_channelName, myChannel.ChannelNumber, "", True, True, True,
                    "", ""))
                ' PLCCommunications.PLC_GetVariableNames(_channel, MyPrograms[0], true, true, "", "");
                Return True
            End Function
        End Class
#End Region


#Region "AB Definition SLCMLC"
        ''' <summary>
        ''' The object that controls the Allen Bradley communication
        ''' </summary>
        ''' <seealso cref="PLC.CommManager.CommType" />
        Friend Class CommAllenBradley_SLCMLC_Obj
            Inherits CommType
            ''' <summary>
            ''' My PLC type
            ''' </summary>
            Private myPLC_Type As PLCDriverType
            ''' <summary>
            ''' Constructor
            ''' </summary>
            Public Sub New()
                myPLC_Type = PLCDriverType.AllenBradley_SLCMLC
            End Sub
            ''' <summary>
            ''' Gets the value.
            ''' </summary>
            ''' <param name="_PLCPath">The _ PLC path.</param>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <param name="_value">if set to <c>true</c> [_value].</param>
            ''' <returns></returns>
            Private Function CommType_GetValue(_PLCPath As String, _channelName As String, ByRef _value As Boolean) As Boolean Implements CommType.GetValue
                ' Underneath is only for testing purpose
                _value = False
                Dim ChannelNum As Integer = Channels.GetChannel(_channelName).ChannelNumber
                Return SLCMLC_Communications.PLC_Read_Specific(_PLCPath, ChannelNum, _value)
            End Function
            ''' <summary>
            ''' Gets the value.
            ''' </summary>
            ''' <param name="_PLCPath">The _ PLC path.</param>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <param name="_value">The _value.</param>
            ''' <returns></returns>
            Private Function CommType_GetValue(_PLCPath As String, _channelName As String, ByRef _value As String) As Boolean Implements CommType.GetValue
                ' Underneath is only for testing purpose
                _value = ""
                Dim ChannelNum As Integer = Channels.GetChannel(_channelName).ChannelNumber
                Return SLCMLC_Communications.PLC_Read_Specific(_PLCPath, ChannelNum, _value)
            End Function
            ''' <summary>
            ''' Gets the value.
            ''' </summary>
            ''' <param name="_PLCPath">The _ PLC path.</param>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <param name="_value">The _value.</param>
            ''' <returns></returns>
            Private Function CommType_GetValue(_PLCPath As String, _channelName As String, ByRef _value As Int32) As Boolean Implements CommType.GetValue
                ' Underneath is only for testing purpose
                _value = 0
                Dim ChannelNum As Integer = Channels.GetChannel(_channelName).ChannelNumber
                Return SLCMLC_Communications.PLC_Read_Specific(_PLCPath, ChannelNum, _value)
            End Function
            ''' <summary>
            ''' Gets the value.
            ''' </summary>
            ''' <param name="_PLCPath">The _ PLC path.</param>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <param name="_value">The _value.</param>
            ''' <returns></returns>
            Private Function CommType_GetValue(_PLCPath As String, _channelName As String, ByRef _value As Double) As Boolean Implements CommType.GetValue
                ' Underneath is only for testing purpose
                _value = 0
                Dim ChannelNum As Integer = Channels.GetChannel(_channelName).ChannelNumber
                Return SLCMLC_Communications.PLC_Read_Specific(_PLCPath, ChannelNum, _value)
            End Function

            ''' <summary>
            ''' Set a value in the PLC, the system checks if the supplied channel is correct
            ''' </summary>
            ''' <param name="_PLCPath">The path to set</param>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <param name="_newValue">The value to set</param>
            ''' <returns>
            ''' True if success
            ''' </returns>
            Private Function CommType_SetValue(_PLCPath As String, _channelName As String, _newValue As Boolean) As Boolean Implements CommType.SetValue
                Dim ChannelNum As Integer = Channels.GetChannel(_channelName).ChannelNumber
                Return SLCMLC_Communications.PLC_Write_Specific(_PLCPath, ChannelNum, _newValue)
            End Function
            ''' <summary>
            ''' Sets the value.
            ''' </summary>
            ''' <param name="_PLCPath">The _ PLC path.</param>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <param name="_newValue">The _new value.</param>
            ''' <returns></returns>
            Private Function CommType_SetValue(_PLCPath As String, _channelName As String, _newValue As String) As Boolean Implements CommType.SetValue
                Dim ChannelNum As Integer = Channels.GetChannel(_channelName).ChannelNumber
                Return SLCMLC_Communications.PLC_Write_Specific(_PLCPath, ChannelNum, _newValue)
            End Function
            ''' <summary>
            ''' Sets the value.
            ''' </summary>
            ''' <param name="_PLCPath">The _ PLC path.</param>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <param name="_newValue">The _new value.</param>
            ''' <returns></returns>
            Private Function CommType_SetValue(_PLCPath As String, _channelName As String, _newValue As Int32) As Boolean Implements CommType.SetValue
                Dim ChannelNum As Integer = Channels.GetChannel(_channelName).ChannelNumber
                Return SLCMLC_Communications.PLC_Write_Specific(_PLCPath, ChannelNum, _newValue)
            End Function
            ''' <summary>
            ''' Sets the value.
            ''' </summary>
            ''' <param name="_PLCPath">The _ PLC path.</param>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <param name="_newValue">The _new value.</param>
            ''' <returns></returns>
            Private Function CommType_SetValue(_PLCPath As String, _channelName As String, _newValue As Double) As Boolean Implements CommType.SetValue
                Dim ChannelNum As Integer = Channels.GetChannel(_channelName).ChannelNumber
                Return SLCMLC_Communications.PLC_Write_Specific(_PLCPath, ChannelNum, _newValue)
            End Function
            ''' <summary>
            ''' returns all values that are pressed in the supplied channel
            ''' </summary>
            ''' <param name="_channelName">Name of the _channel.</param>
            ''' <returns>
            ''' a string list with all PLC paths
            ''' </returns>
            Private Function CommType_GetAllPaths(_channelName As String) As Boolean Implements CommType.GetAllPaths
                Dim myChannel As Channel = Channels.GetChannel(_channelName)
                SLCMLC_Communications.PLC_RefreshData(myChannel.ChannelNumber)
                MyABPrograms(myChannel.ChannelNumber) = SLCMLC_Communications.PLC_GetProgramNames(myChannel.ChannelNumber)


                myChannel.CurrentPaths = New List(Of String)()
                myChannel.CurrentPaths.AddRange(SLCMLC_Communications.PLC_GetVariableNames(_channelName, myChannel.ChannelNumber, "", True, True, True,
                    "", ""))
                ' PLCCommunications.PLC_GetVariableNames(_channel, MyPrograms[0], true, true, "", "");
                Return True
            End Function
        End Class
#End Region
#End Region
    End Class

End Namespace
#Region "Type Declarations"


''' <summary>
''' 
''' </summary>
Public Enum CommStatus
    ''' <summary>
    ''' The initialting
    ''' </summary>
    Initialting
    ''' <summary>
    ''' The established
    ''' </summary>
    Established
    ''' <summary>
    ''' The slow
    ''' </summary>
    Slow
    ''' <summary>
    ''' The failed
    ''' </summary>
    Failed
    ''' <summary>
    ''' The offline
    ''' </summary>
    OFFLINE
    ''' <summary>
    ''' The unknown
    ''' </summary>
    Unknown
End Enum

#End Region

