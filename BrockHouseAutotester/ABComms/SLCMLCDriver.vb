﻿Imports System.Collections.Generic
Imports ABLink
Imports Common
Imports System.Collections.ObjectModel

Namespace ABDriver
    Class SLCMLC_Communications
        'static void Main(string[] args)
        '{
        '    Controller MyPLC = new Controller();
        '    Tag MyTag = new Tag();
        '    MyPLC.Timeout = 3000;
        '    int plcType = 0;
        '    int driverType = 0;
        '    string ipAddress = "";
        '    string tagName = "";
        '    int doAgain = 0;

        '    System.Console.WriteLine("NET.ABLINK 6.0 Console Example");
        '    System.Console.WriteLine("");

        '    ////////////////////////////////
        '    // select a PLC Type
        '    LABEL_ENTER_PLCTYPE:
        '    System.Console.Write("PLC Type: 1=PLC-5, 2=SLC, 3=MLC, 4=CLX MemMap, 0=Exit  ");
        '    try
        '    {
        '        plcType = Convert.ToInt32(System.Console.ReadLine());
        '    }
        '    catch
        '    {
        '        goto LABEL_ENTER_PLCTYPE;
        '    }
        '    switch (plcType)
        '    {
        '        case 0:
        '            return;
        '        case 1: // PLC-5
        '            MyPLC.CPUType = Controller.CPU.PLC;
        '            break;
        '        case 2: // SLC 5
        '            MyPLC.CPUType = Controller.CPU.SLC;
        '            break;
        '        case 3: // MicroLogix 1000, 1100, 1200, 1400, 1500
        '            MyPLC.CPUType = Controller.CPU.MLC;
        '            break;
        '        case 4: // use ML1100 for ControlLogix PLC/SLC Memory Mapped tags
        '            MyPLC.CPUType = Controller.CPU.ML1100;
        '            break;
        '        default:
        '            goto LABEL_ENTER_PLCTYPE;
        '    }

        '    System.Console.WriteLine();

        '    ////////////////////////////////////////
        '    // select driver type
        '    LABEL_ENTER_DRIVER:
        '    System.Console.Write("Driver Type: 1=ENET, 2=ENETIP, 3=ENETNI, 0=Exit  ");
        '    try
        '    {
        '        driverType = Convert.ToInt32(System.Console.ReadLine());
        '    }
        '    catch
        '    {
        '        goto LABEL_ENTER_DRIVER;
        '    }
        '    switch (driverType)
        '    {
        '        case 0:
        '            return;
        '        case 1: // ENET
        '            MyPLC.DriverType = Controller.Driver.ENET;
        '            break;
        '        case 2:
        '            MyPLC.DriverType = Controller.Driver.ENETIP;
        '            break;
        '        case 3:
        '            MyPLC.DriverType = Controller.Driver.NETENI;
        '            break;

        '        default:
        '            return;
        '    }
        '    System.Console.WriteLine();

        '    ////////////////////////////////
        '    //enter IP address
        '    LABEL_ENTER_IPADDRESS:
        '    System.Console.Write("Enter IP Address: ");
        '    try
        '    {
        '        ipAddress = System.Console.ReadLine();
        '    }
        '    catch
        '    {
        '        goto LABEL_ENTER_IPADDRESS;
        '    }
        '    if (null == ipAddress)
        '        return;
        '    if (0 == ipAddress.Length)
        '        goto LABEL_ENTER_IPADDRESS;
        '    MyPLC.IPAddress = ipAddress;

        '    System.Console.WriteLine();
        '    ////////////////////////////////
        '    //enter Tag Name
        '    LABEL_ENTER_TAGNAME:
        '    System.Console.Write("Data Table Address: ");
        '    try
        '    {
        '        tagName = System.Console.ReadLine();
        '    }
        '    catch
        '    {
        '        goto LABEL_ENTER_TAGNAME;
        '    }
        '    if (null == tagName)
        '        return;
        '    if (0 == tagName.Length)
        '        goto LABEL_ENTER_TAGNAME;
        '    MyTag.Name = tagName;

        '    //////////////////////////////
        '    // connect to the PLC
        '    if (ResultCode.E_SUCCESS != MyPLC.Connect())
        '    {
        '        System.Console.WriteLine(MyPLC.ErrorString + ":" + MyPLC.ErrorCode.ToString());
        '        System.Console.ReadLine();
        '        return;
        '    }

        '    try
        '    {
        '        /////////////////////////////
        '        // read the tag
        '        MyPLC.ReadTag(MyTag);

        '        ///////////////////////////////
        '        // check read results
        '        if (ResultCode.QUAL_GOOD == MyTag.QualityCode)
        '        {
        '            System.Console.Write("Tag Value: = ");
        '            System.Console.WriteLine(MyTag.Value.ToString());
        '            System.Console.Write("TimeStamp: = ");
        '            System.Console.WriteLine(MyTag.TimeStamp.ToString());
        '        }

        '        //////////////////////////////
        '        // display quality code
        '        System.Console.Write("Tag Quality: = ");
        '        System.Console.WriteLine(MyTag.QualityString);

        '        //////////////////////////////
        '        // display errors
        '        System.Console.Write("Error: = ");
        '        System.Console.WriteLine(MyTag.ErrorString + " " + MyTag.ErrorCode.ToString());
        '    }
        '    catch (System.Exception ex)
        '    {
        '        System.Console.WriteLine(ex.Message);
        '    }
        '    System.Console.WriteLine();

        '    //////////////////////////////
        '    // read again?
        '    System.Console.Write("Continue 1=Yes, 0=Exit  ");
        '    try
        '    {
        '        doAgain = Convert.ToInt32(System.Console.ReadLine());
        '    }
        '    catch
        '    {
        '        return;
        '    }
        '    if (0 != doAgain)
        '        goto LABEL_ENTER_PLCTYPE;

        '}

#Region "Global Variables"


        ''' <summary>
        ''' The ab pl cs
        ''' </summary>
        Private Shared AB_PLCs As New List(Of Controller)()
        ''' <summary>
        ''' The tags loaded
        ''' </summary>
        Private Shared TagsLoaded As Boolean() = New Boolean() {False, False, False, False, False, False,
            False, False, False, False}
        ''' <summary>
        ''' The components
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing



#End Region

#Region "Public Functions"

        Private Shared Function GetCPUType(CPUType As Int32) As Controller.CPU
            Select Case CPUType

                Case 1
                    ' PLC-5
                    Return Controller.CPU.PLC

                Case 2
                    ' SLC 5
                    Return Controller.CPU.SLC

                Case 3
                    ' MicroLogix 1000, 1100, 1200, 1400, 1500
                    Return Controller.CPU.MLC

                Case 4
                    ' use ML1100 for ControlLogix PLC/SLC Memory Mapped tags
                    Return Controller.CPU.ML1100
                Case Else

                    Return Controller.CPU.ML1100
            End Select
        End Function


        ''' <summary>
        ''' Finds the type of the data.
        ''' </summary>
        ''' <param name="_tag">The tag.</param>
        ''' <param name="_DataType">Type of the data.</param>
        ''' <returns></returns>
        Private Shared Function FindDataType(_tag As Tag, _DataType As ABTagClass) As Tag

            Select Case _DataType
                Case ABTagClass.BOOL
                    _tag.NetType = GetType(System.Boolean)
                    Exit Select
                Case ABTagClass.SINT
                    _tag.NetType = GetType(System.SByte)
                    Exit Select
                Case ABTagClass.INT
                    _tag.NetType = GetType(System.Int16)
                    Exit Select
                Case ABTagClass.DINT
                    _tag.NetType = GetType(System.Int32)
                    Exit Select
                Case ABTagClass.REAL
                    _tag.NetType = GetType(System.Single)
                    Exit Select
                Case ABTagClass.[STRING]
                    _tag.NetType = GetType(System.String)
                    Exit Select
                Case ABTagClass.LINT
                    _tag.NetType = GetType(System.Int64)
                    Exit Select
                Case ABTagClass.[OBJECT]
                    _tag.NetType = GetType(System.Object)
                    Exit Select
                Case Else
                    If True Then
                        DebugTools.UpdateActionList("AB Driver, Unable to decode Tag Type.")
                    End If
                    Exit Select
            End Select
            Return _tag
        End Function

        ''' <summary>
        ''' Initialize Communication to PLC on defined Channel
        ''' </summary>
        ''' <param name="_channel">The channel.</param>
        ''' <param name="_IPAddress">The ip address.</param>
        ''' <param name="_slot">The slot.</param>
        ''' <param name="_timeout">The timeout.</param>
        ''' <returns>
        ''' bool. True if Connection is made.
        ''' </returns>
        Public Shared Function CommInit(_IPAddress As String, _slot As String, _timeout As Integer, CPUTType As Int32) As Int32
            AB_PLCs.Add(New Controller())

            Dim _channel As Int32 = AB_PLCs.Count - 1

            AB_PLCs(_channel).IPAddress = _IPAddress
            AB_PLCs(_channel).Path = _slot
            AB_PLCs(_channel).Timeout = _timeout
            AB_PLCs(_channel).CPUType = GetCPUType(CPUTType)
            AB_PLCs(_channel).DriverType = Controller.Driver.ENETIP
            '      if (ResultCode.E_SUCCESS == AB_PLCs[_channel].Connect())
            '       {
            'CommGroup[_channel].Controller = AB_PLCs[_channel];
            'CommGroup[_channel].ScanStart();
            '     }
            '     else
            '    {
            '         DebugTools.UpdateActionList("AB Driver, Unable to Establish Comms.");
            '    }
            Return _channel

        End Function

        ''' <summary>
        ''' Initialize Data Write to PLC on defined Channel at an address
        ''' </summary>
        ''' <param name="_channel">The channel.</param>
        ''' <param name="_address">The address.</param>
        ''' <param name="_value">The value.</param>
        ''' <param name="_DataType">Type of the data.</param>
        ''' <returns>
        ''' bool. True if write is successful.
        ''' </returns>
        Public Shared Function PLC_Write(_channel As Integer, _address As String, _value As String, _DataType As ABTagClass) As Boolean
            Dim Complete As Boolean = False

            If ProgramConstants.MasterLiveDataMode.State Then
                Dim MyTag As New Tag()

                Try
                    '''//////////////////////////////////////
                    ' set Tag properties
                    MyTag.Name = _address
                    MyTag.Active = True
                    MyTag = FindDataType(MyTag, _DataType)

                    MyTag.Value = _value

                    '''////////////////////////////
                    ''' write tag and check for results
                    ''' 
                    If AB_PLCs(_channel).WriteTag(MyTag) = ResultCode.E_SUCCESS Then
                        Complete = True
                    End If

                    If ResultCode.QUAL_GOOD <> MyTag.QualityCode Then
                        DebugTools.UpdateActionList("AB Driver, Unable to write Data.")
                        ProgramConstants.ABDataWriteErrors += 1
                    End If
                Catch ex As Exception
                    DebugTools.UpdateActionList("AB Driver, " + ex.Message)
                    ProgramConstants.ABDataWriteErrors += 1
                End Try
            End If
            Return Complete
        End Function


        ''' <summary>
        ''' Initialize Data Read From PLC on defined Channel at an address
        ''' </summary>
        ''' <param name="_channel">The _channel.</param>
        ''' <param name="_address">The address.</param>
        ''' <param name="_DataType">Type of the _ data.</param>
        ''' <returns>
        ''' String value of the read data.
        ''' </returns>
        Public Shared Function PLC_Read(_channel As Integer, _address As String, _DataType As ABTagClass) As String
            Dim Value As String = ""

            If ProgramConstants.MasterLiveDataMode.State Then
                Dim MyTag As New Tag()

                MyTag = FindDataType(MyTag, _DataType)

                MyTag.Name = _address
                MyTag.Length = 1

                ' Check Tag Read Stats
                Dim ReadResponse As Integer = AB_PLCs(_channel).ReadTag(MyTag)
                If ReadResponse <> ResultCode.E_SUCCESS Then
                    DebugTools.UpdateActionList("AB Driver, Unable to Read Data")
                    ProgramConstants.ABDataReadErrors += 1
                End If

                If ResultCode.QUAL_GOOD = MyTag.QualityCode Then
                    Value = MyTag.Value.ToString()
                Else
                    DebugTools.UpdateActionList("AB Driver, Unable to Read Data")
                    ProgramConstants.ABDataReadErrors += 1
                End If
            End If
            Return Value
        End Function

        ''' <summary>
        ''' PLCs the read specific.
        ''' </summary>
        ''' <param name="_address">The address.</param>
        ''' <param name="_channelNum">The channel number.</param>
        ''' <param name="_value">if set to <c>true</c> [value].</param>
        ''' <returns></returns>
        Public Shared Function PLC_Read_Specific(_address As String, _channelNum As Integer, ByRef _value As Boolean) As Boolean
            _value = False
            Dim xSuccess As Boolean = False

            If ProgramConstants.MasterLiveDataMode.State Then
                Dim MyTag As New Tag()

                Dim MyAddress As String = ABCore.FindTagAddress(_address)
                '   ABTagClass rawTagClass = ABCore.FindABTagClass(_address);
                If (_channelNum <> 99) AndAlso (MyAddress <> "NotFound") Then
                    ' && (rawTagClass != ABTagClass.NotFound)
                    MyTag.NetType = GetType(System.Boolean)
                    '       MyTag = FindDataType(MyTag, rawTagClass);

                    MyTag.Name = MyAddress
                    MyTag.Length = 1

                    ' Check Tag Read Stats

                    Dim ReadResponse As Integer = AB_PLCs(_channelNum).ReadTag(MyTag)
                    If ReadResponse <> ResultCode.E_SUCCESS Then
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data")
                        ProgramConstants.ABDataReadErrors += 1
                        If ProgramConstants.ABDataReadErrors > 3 Then
                            ProgramConstants.MasterLiveDataMode.State = False
                        End If
                    End If
                    If ResultCode.QUAL_GOOD = MyTag.QualityCode Then
                        _value = Convert.ToBoolean(MyTag.Value)
                        xSuccess = True
                    Else
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data")
                        ProgramConstants.ABDataReadErrors += 1
                        If ProgramConstants.ABDataReadErrors > 3 Then
                            ProgramConstants.MasterLiveDataMode.State = False
                        End If
                    End If
                Else
                    DebugTools.UpdateActionList("AB Driver, Unable to Read Data")
                    ProgramConstants.ABDataReadErrors += 1
                End If
            End If
            Return xSuccess

        End Function
        ''' <summary>
        ''' PLCs the read specific.
        ''' </summary>
        ''' <param name="_address">The address.</param>
        ''' <param name="_channelNum">The channel number.</param>
        ''' <param name="_value">The value.</param>
        ''' <returns></returns>
        Public Shared Function PLC_Read_Specific(_address As String, _channelNum As Integer, ByRef _value As String) As Boolean
            _value = ""
            Dim xSuccess As Boolean = False

            If ProgramConstants.MasterLiveDataMode.State Then
                Dim MyTag As New Tag()


                Dim MyAddress As String = ABCore.FindTagAddress(_address)
                '    ABTagClass rawTagClass = ABCore.FindABTagClass(_address);
                If (_channelNum <> 99) AndAlso (MyAddress <> "NotFound") Then
                    ' && (rawTagClass != ABTagClass.NotFound)
                    MyTag.NetType = GetType(System.String)
                    ' MyTag = FindDataType(MyTag, rawTagClass);

                    MyTag.Name = MyAddress
                    MyTag.Length = 1

                    ' Check Tag Read Stats

                    Dim ReadResponse As Integer = AB_PLCs(_channelNum).ReadTag(MyTag)
                    If ReadResponse <> ResultCode.E_SUCCESS Then
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data")
                        ProgramConstants.ABDataReadErrors += 1
                        If ProgramConstants.ABDataReadErrors > 3 Then
                            ProgramConstants.MasterLiveDataMode.State = False
                        End If
                    End If
                    If ResultCode.QUAL_GOOD = MyTag.QualityCode Then
                        _value = MyTag.Value.ToString()
                        xSuccess = True
                    Else
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data")
                        ProgramConstants.ABDataReadErrors += 1
                        If ProgramConstants.ABDataReadErrors > 3 Then
                            ProgramConstants.MasterLiveDataMode.State = False
                        End If
                    End If
                Else
                    DebugTools.UpdateActionList("AB Driver, Unable to Read Data")
                End If
            End If
            Return xSuccess

        End Function
        ''' <summary>
        ''' PLCs the read specific.
        ''' </summary>
        ''' <param name="_address">The address.</param>
        ''' <param name="_channelNum">The channel number.</param>
        ''' <param name="_value">The value.</param>
        ''' <returns></returns>
        Public Shared Function PLC_Read_Specific(_address As String, _channelNum As Integer, ByRef _value As Int32) As Boolean
            _value = 0
            Dim xSuccess As Boolean = False

            If ProgramConstants.MasterLiveDataMode.State Then
                Dim MyTag As New Tag()


                Dim MyAddress As String = ABCore.FindTagAddress(_address)
                '  ABTagClass rawTagClass = ABCore.FindABTagClass(_address);
                If (_channelNum <> 99) AndAlso (MyAddress <> "NotFound") Then
                    '&& (rawTagClass != ABTagClass.NotFound)
                    MyTag.NetType = GetType(System.Int32)
                    '  MyTag = FindDataType(MyTag, rawTagClass);

                    MyTag.Name = MyAddress
                    MyTag.Length = 1

                    ' Check Tag Read Stats

                    Dim ReadResponse As Integer = AB_PLCs(_channelNum).ReadTag(MyTag)
                    If ReadResponse <> ResultCode.E_SUCCESS Then
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data")
                        ProgramConstants.ABDataReadErrors += 1
                        If ProgramConstants.ABDataReadErrors > 3 Then
                            ProgramConstants.MasterLiveDataMode.State = False
                        End If
                    End If
                    If ResultCode.QUAL_GOOD = MyTag.QualityCode Then
                        _value = Convert.ToInt32(MyTag.Value)
                        xSuccess = True
                    Else
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data")
                        ProgramConstants.ABDataReadErrors += 1
                        If ProgramConstants.ABDataReadErrors > 3 Then
                            ProgramConstants.MasterLiveDataMode.State = False
                        End If
                    End If
                Else
                    DebugTools.UpdateActionList("AB Driver, Unable to Read Data")
                End If
            End If
            Return xSuccess

        End Function
        ''' <summary>
        ''' PLCs the read specific.
        ''' </summary>
        ''' <param name="_address">The address.</param>
        ''' <param name="_channelNum">The channel number.</param>
        ''' <param name="_value">The value.</param>
        ''' <returns></returns>
        Public Shared Function PLC_Read_Specific(_address As String, _channelNum As Integer, ByRef _value As Double) As Boolean
            _value = 0
            Dim xSuccess As Boolean = False

            If ProgramConstants.MasterLiveDataMode.State Then
                Dim MyTag As New Tag()


                Dim MyAddress As String = ABCore.FindTagAddress(_address)
                '    ABTagClass rawTagClass = ABCore.FindABTagClass(_address);
                If (_channelNum <> 99) AndAlso (MyAddress <> "NotFound") Then
                    ' && (rawTagClass != ABTagClass.NotFound)
                    MyTag.NetType = GetType(System.Single)
                    '       MyTag = FindDataType(MyTag, rawTagClass);

                    MyTag.Name = MyAddress
                    MyTag.Length = 1

                    ' Check Tag Read Stats

                    Dim ReadResponse As Integer = AB_PLCs(_channelNum).ReadTag(MyTag)
                    If ReadResponse <> ResultCode.E_SUCCESS Then
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data")
                        ProgramConstants.ABDataReadErrors += 1
                        If ProgramConstants.ABDataReadErrors > 3 Then
                            ProgramConstants.MasterLiveDataMode.State = False
                        End If
                    End If
                    If ResultCode.QUAL_GOOD = MyTag.QualityCode Then
                        '_value = Convert.ToDouble(MyTag.Value);
                        _value = Double.Parse(MyTag.Value.ToString())
                        xSuccess = True
                    Else
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data")
                        ProgramConstants.ABDataReadErrors += 1
                        If ProgramConstants.ABDataReadErrors > 3 Then
                            ProgramConstants.MasterLiveDataMode.State = False
                        End If
                    End If
                Else
                    DebugTools.UpdateActionList("AB Driver, Unable to Read Data")
                End If
            End If
            Return xSuccess

        End Function

        ''' <summary>
        ''' PLCs the write specific.
        ''' </summary>
        ''' <param name="_address">The address.</param>
        ''' <param name="_channelNum">The channel number.</param>
        ''' <param name="_value">if set to <c>true</c> [value].</param>
        ''' <returns></returns>
        Public Shared Function PLC_Write_Specific(_address As String, _channelNum As Integer, _value As Boolean) As Boolean
            Dim Complete As Boolean = False

            If ProgramConstants.MasterLiveDataMode.State Then
                Dim MyTag As New Tag()

                Dim MyAddress As String = ABCore.FindTagAddress(_address)
                '  ABTagClass rawTagClass = ABCore.FindABTagClass(_address);

                If (_channelNum <> 99) AndAlso (MyAddress <> "NotFound") Then
                    '&& (rawTagClass != ABTagClass.NotFound)


                    Try
                        '''//////////////////////////////////////
                        ' set Tag properties
                        MyTag.Name = MyAddress
                        MyTag.Active = True
                        MyTag.NetType = GetType(System.Boolean)
                        '    MyTag = FindDataType(MyTag, rawTagClass);

                        MyTag.Value = _value

                        '''////////////////////////////
                        ''' write tag and check for results
                        ''' 

                        If AB_PLCs(_channelNum).WriteTag(MyTag) = ResultCode.E_SUCCESS Then
                            Complete = True
                        End If

                        If ResultCode.QUAL_GOOD <> MyTag.QualityCode Then
                            DebugTools.UpdateActionList("AB Driver, Unable to Read Data")
                            ProgramConstants.ABDataWriteErrors += 1
                            If ProgramConstants.ABDataReadErrors > 3 Then
                                ProgramConstants.MasterLiveDataMode.State = False
                            End If

                        End If
                    Catch ex As Exception
                        DebugTools.UpdateActionList("AB Driver, Unable to Read Data, " + ex.Message)
                        ProgramConstants.ABDataWriteErrors += 1
                        If ProgramConstants.ABDataReadErrors > 3 Then
                            ProgramConstants.MasterLiveDataMode.State = False
                        End If
                    End Try
                Else
                    DebugTools.UpdateActionList("AB Driver, Unable to Read Data")
                    ProgramConstants.ABDataWriteErrors += 1
                    If ProgramConstants.ABDataReadErrors > 3 Then
                        ProgramConstants.MasterLiveDataMode.State = False
                    End If
                End If
            End If
            Return Complete

        End Function
        ''' <summary>
        ''' PLCs the write specific.
        ''' </summary>
        ''' <param name="_address">The address.</param>
        ''' <param name="_channelNum">The channel number.</param>
        ''' <param name="_value">The value.</param>
        ''' <returns></returns>
        Public Shared Function PLC_Write_Specific(_address As String, _channelNum As Integer, _value As String) As Boolean
            Dim Complete As Boolean = False

            If ProgramConstants.MasterLiveDataMode.State Then
                Dim MyTag As New Tag()

                Dim MyAddress As String = ABCore.FindTagAddress(_address)
                '   ABTagClass rawTagClass = ABCore.FindABTagClass(_address);

                If (_channelNum <> 99) AndAlso (MyAddress <> "NotFound") Then
                    '&& (rawTagClass != ABTagClass.NotFound)


                    Try
                        '''//////////////////////////////////////
                        ' set Tag properties
                        MyTag.Name = MyAddress
                        MyTag.Active = True
                        MyTag.NetType = GetType(System.String)
                        '       MyTag = FindDataType(MyTag, rawTagClass);

                        MyTag.Value = _value.ToString()

                        '''////////////////////////////
                        ''' write tag and check for results
                        ''' 

                        If AB_PLCs(_channelNum).WriteTag(MyTag) = ResultCode.E_SUCCESS Then
                            Complete = True
                        End If

                        If ResultCode.QUAL_GOOD <> MyTag.QualityCode Then
                            DebugTools.UpdateActionList("AB Driver, Unable to Write Data")
                            ProgramConstants.ABDataWriteErrors += 1
                            If ProgramConstants.ABDataReadErrors > 3 Then
                                ProgramConstants.MasterLiveDataMode.State = False
                            End If

                        End If
                    Catch ex As Exception
                        DebugTools.UpdateActionList("AB Driver, Unable to Write Data, " + ex.Message)
                        ProgramConstants.ABDataWriteErrors += 1
                        If ProgramConstants.ABDataReadErrors > 3 Then
                            ProgramConstants.MasterLiveDataMode.State = False
                        End If
                    End Try
                Else
                    DebugTools.UpdateActionList("AB Driver, Unable to Write Data")
                    ProgramConstants.ABDataWriteErrors += 1
                    If ProgramConstants.ABDataReadErrors > 3 Then
                        ProgramConstants.MasterLiveDataMode.State = False
                    End If
                End If
            End If
            Return Complete

        End Function
        ''' <summary>
        ''' PLCs the write specific.
        ''' </summary>
        ''' <param name="_address">The address.</param>
        ''' <param name="_channelNum">The channel number.</param>
        ''' <param name="_value">The value.</param>
        ''' <returns></returns>
        Public Shared Function PLC_Write_Specific(_address As String, _channelNum As Integer, _value As Int32) As Boolean
            Dim Complete As Boolean = False

            If ProgramConstants.MasterLiveDataMode.State Then
                Dim MyTag As New Tag()

                Dim MyAddress As String = ABCore.FindTagAddress(_address)
                '     ABTagClass rawTagClass = ABCore.FindABTagClass(_address);

                If (_channelNum <> 99) AndAlso (MyAddress <> "NotFound") Then
                    '&& (rawTagClass != ABTagClass.NotFound)


                    Try
                        '''//////////////////////////////////////
                        ' set Tag properties
                        MyTag.Name = MyAddress
                        MyTag.Active = True
                        MyTag.NetType = GetType(System.Int32)
                        '  MyTag = FindDataType(MyTag, rawTagClass);

                        MyTag.Value = _value.ToString()

                        '''////////////////////////////
                        ''' write tag and check for results
                        ''' 

                        If AB_PLCs(_channelNum).WriteTag(MyTag) = ResultCode.E_SUCCESS Then
                            Complete = True
                        End If

                        If ResultCode.QUAL_GOOD <> MyTag.QualityCode Then
                            DebugTools.UpdateActionList("AB Driver, Unable to Write Data")
                            ProgramConstants.ABDataWriteErrors += 1
                            If ProgramConstants.ABDataReadErrors > 3 Then
                                ProgramConstants.MasterLiveDataMode.State = False
                            End If

                        End If
                    Catch ex As Exception
                        DebugTools.UpdateActionList("AB Driver, Unable to Write Data, " + ex.Message)
                        ProgramConstants.ABDataWriteErrors += 1
                        If ProgramConstants.ABDataReadErrors > 3 Then
                            ProgramConstants.MasterLiveDataMode.State = False
                        End If
                    End Try
                Else
                    DebugTools.UpdateActionList("AB Driver, Unable to Write Data")
                    ProgramConstants.ABDataWriteErrors += 1
                    If ProgramConstants.ABDataReadErrors > 3 Then
                        ProgramConstants.MasterLiveDataMode.State = False
                    End If
                End If
            End If
            Return Complete

        End Function
        ''' <summary>
        ''' PLCs the write specific.
        ''' </summary>
        ''' <param name="_address">The address.</param>
        ''' <param name="_channelNum">The channel number.</param>
        ''' <param name="_value">The value.</param>
        ''' <returns></returns>
        Public Shared Function PLC_Write_Specific(_address As String, _channelNum As Integer, _value As Double) As Boolean
            Dim Complete As Boolean = False

            If ProgramConstants.MasterLiveDataMode.State Then
                Dim MyTag As New Tag()

                Dim MyAddress As String = ABCore.FindTagAddress(_address)
                '          ABTagClass rawTagClass = ABCore.FindABTagClass(_address);

                If (_channelNum <> 99) AndAlso (MyAddress <> "NotFound") Then
                    ' && (rawTagClass != ABTagClass.NotFound)


                    Try
                        '''//////////////////////////////////////
                        ' set Tag properties
                        MyTag.Name = MyAddress
                        MyTag.Active = True
                        MyTag.NetType = GetType(System.Single)
                        '   MyTag = FindDataType(MyTag, rawTagClass);

                        MyTag.Value = _value.ToString()

                        '''////////////////////////////
                        ''' write tag and check for results
                        ''' 

                        If AB_PLCs(_channelNum).WriteTag(MyTag) = ResultCode.E_SUCCESS Then
                            Complete = True
                        End If

                        If ResultCode.QUAL_GOOD <> MyTag.QualityCode Then
                            DebugTools.UpdateActionList("AB Driver, Unable to Write Data")
                            ProgramConstants.ABDataWriteErrors += 1
                            If ProgramConstants.ABDataReadErrors > 3 Then
                                ProgramConstants.MasterLiveDataMode.State = False
                            End If

                        End If
                    Catch ex As Exception
                        DebugTools.UpdateActionList("AB Driver, Unable to Write Data, " + ex.Message)
                        ProgramConstants.ABDataWriteErrors += 1
                        If ProgramConstants.ABDataReadErrors > 3 Then
                            ProgramConstants.MasterLiveDataMode.State = False
                        End If
                    End Try
                Else
                    DebugTools.UpdateActionList("AB Driver, Unable to Write Data")
                    ProgramConstants.ABDataWriteErrors += 1
                    If ProgramConstants.ABDataReadErrors > 3 Then
                        ProgramConstants.MasterLiveDataMode.State = False
                    End If
                End If
            End If
            Return Complete

        End Function


        ''' <summary>
        ''' Initiates a command to reload programs and tags names/data types from the PLC
        ''' </summary>
        ''' <param name="_channel">The channel.</param>
        ''' <returns>
        ''' bool. True if read is successful.
        ''' </returns>
        Public Shared Function PLC_RefreshData(_channel As Integer) As Boolean
            ' upload tags from PLC
            Dim xSuccess As Boolean = False
            DebugTools.UpdateActionList("AB Driver, Reading Tags from PLC")
            Dim UploadStatus As Integer = AB_PLCs(_channel).UploadTags()

            If UploadStatus <> ResultCode.E_SUCCESS Then
                DebugTools.UpdateActionList("AB Driver, Unable to Read Tags")
                TagsLoaded(_channel) = True
            Else
                xSuccess = True
            End If
            DebugTools.UpdateActionList("AB Driver, Tag Read Complete")


            Return xSuccess
        End Function


        ''' <summary>
        ''' PLCs the get program names.
        ''' </summary>
        ''' <param name="_channel">The channel.</param>
        ''' <returns></returns>
        Public Shared Function PLC_GetProgramNames(_channel As Integer) As List(Of String)
            DebugTools.UpdateActionList("AB Driver, Fetch PLC Program Names.")
            Dim ReturnProgramList As New List(Of String)()
            For Each PLCProgram As ABLink.Program In AB_PLCs(_channel).ProgramList
                ReturnProgramList.Add(PLCProgram.Name)
            Next
            DebugTools.UpdateActionList("AB Driver, Program Name Fetch Complete")
            Return ReturnProgramList
        End Function


        ''' <summary>
        ''' Builds the address information for the start of the address.
        ''' </summary>
        ''' <param name="_channelName">The communication channel the PLC is connected to internally</param>
        ''' <param name="_tagClass">The uploaded class of the tag.</param>
        ''' <returns></returns>
        Private Shared Function BuildABAddressInfo(_channelName As String, _tagClass As ABTagClass) As String
            '+ ProgramConstants.ABClassHeader + (ABTagClass)(int)_tagClass + ProgramConstants.ABClassFooter
            Return ProgramConstants.ChannelHeader + _channelName + ProgramConstants.ChannelFooter + ProgramConstants.AddressHeader
        End Function


        ''' <summary>
        ''' PLCs the get variable names.
        ''' </summary>
        ''' <param name="_channelName">Name of the channel.</param>
        ''' <param name="_channelNumber">The channel number.</param>
        ''' <param name="_program">The program.</param>
        ''' <param name="_ScanAllPrograms">if set to <c>true</c> [scan all programs].</param>
        ''' <param name="_GlobalEnabled">if set to <c>true</c> [global enabled].</param>
        ''' <param name="_ProgramEnabled">if set to <c>true</c> [program enabled].</param>
        ''' <param name="_NameFilter">The name filter.</param>
        ''' <param name="_ClassFilter">The class filter.</param>
        ''' <returns></returns>
        Public Shared Function PLC_GetVariableNames(_channelName As String, _channelNumber As Integer, _program As String, _ScanAllPrograms As Boolean, _GlobalEnabled As Boolean, _ProgramEnabled As Boolean,
            _NameFilter As String, _ClassFilter As String) As List(Of String)
            DebugTools.UpdateActionList("AB Driver, Fetching Variable Names.")
            Dim PLCVariableNames As New List(Of String)()
            Dim nodeName As String = ""
            For Each PLCProgram As ABLink.Program In AB_PLCs(_channelNumber).ProgramList
                If (PLCProgram.Name = _program) OrElse _ScanAllPrograms Then
                    If PLCProgram.Name = "" Then
                        nodeName = "Global"
                        If False = _GlobalEnabled Then
                            Continue For
                        End If
                    Else
                        nodeName = PLCProgram.Name
                        If False = _ProgramEnabled Then
                            Continue For
                        End If
                    End If
                    Dim TagListInProgram As ReadOnlyCollection(Of TagTemplate) = PLCProgram.TagItems(_NameFilter, _ClassFilter)
                    Dim xIgnoreTag As Boolean = False
                    For Each item As TagTemplate In TagListInProgram
                        xIgnoreTag = item.Name.StartsWith("_")
                        If xIgnoreTag = False Then
                            Dim MyTagClass As New ABTagClass()
                            Dim search As String = item.TypeName.ToUpper()
                            If search.Contains("BOOL") Then
                                MyTagClass = ABTagClass.BOOL
                            ElseIf search.Contains("SINT") Then
                                MyTagClass = ABTagClass.SINT
                            ElseIf search.Contains("INT") Then
                                MyTagClass = ABTagClass.DINT
                            ElseIf search.Contains("DINT") Then
                                MyTagClass = ABTagClass.DINT
                            ElseIf search.Contains("REAL") Then
                                MyTagClass = ABTagClass.REAL
                            ElseIf search.Contains("STRING") Then
                                MyTagClass = ABTagClass.[STRING]
                            ElseIf search.Contains("LINT") Then
                                MyTagClass = ABTagClass.SINT
                            ElseIf search.Contains("OBJECT") Then
                                MyTagClass = ABTagClass.SINT
                            Else
                                MyTagClass = ABTagClass.NotFound
                            End If
                            Dim sAddress As String = BuildABAddressInfo(_channelName, MyTagClass) + item.Name + ProgramConstants.AddressFooter

                            PLCVariableNames.Add(sAddress)
                        End If

                    Next
                End If
            Next
            DebugTools.UpdateActionList("AB Driver, Fetching Variable Names Complete.")
            Return PLCVariableNames
        End Function

#End Region

#Region "Misc PLC Functions"


        ''' <summary>
        ''' Clears memory in use.
        ''' </summary>
        ''' <param name="_channel">The channel.</param>
        ''' <param name="_disposing">if set to dumps all memory <c>true</c> [_disposing].</param>
        Public Overridable Sub Dispose(_channel As Integer, _disposing As Boolean)

            If _disposing AndAlso (components IsNot Nothing) Then
                '  CommGroup[_channel].Dispose();
                components.Dispose()
            End If
            _disposing = True
        End Sub

        ''' <summary>
        ''' Stops the scanning of all data. (to be used on shutdown.
        ''' </summary>
        ''' <param name="_channel">The channel.</param>
        Public Shared Sub StopScanning(_channel As Integer)
            '   CommGroup[_channel].ScanStop();
        End Sub

        ''' <summary>
        ''' Starts the scanning of all data.
        ''' </summary>
        ''' <param name="_channel">The channel.</param>
        Public Shared Sub StartScanning(_channel As Integer)
            '  CommGroup[_channel].ScanStart();
        End Sub


        ''' <summary>
        ''' Disconnects the PLC from the system
        ''' </summary>
        ''' <param name="_channel">The channel.</param>
        Private Shared Sub Disconnect(_channel As Integer)
            '    CommGroup[_channel].ScanStop();
            AB_PLCs(_channel).Disconnect()
        End Sub

        ''' <summary>
        ''' Shudowns this instance.
        ''' </summary>
        Public Shared Sub Shudown()
            Dim x As Integer = 0
            For Each myController As Controller In AB_PLCs
                ' StopScanning(x);
                If Not (myController Is Nothing) Then
                    Disconnect(x)
                End If
                x += 1
            Next
        End Sub


#End Region

#Region "Type Declarations"

#End Region


    End Class
End Namespace
