Imports System.Net
Imports System.Net.NetworkInformation

Namespace Common
    Public Class ABCore
        Public Shared Function FindChannelName(_address As String) As String
            Dim channel As String = ""
            Dim Start As Integer, [End] As Integer
            If _address.Contains(ProgramConstants.ChannelHeader) AndAlso _address.Contains(ProgramConstants.ChannelFooter) Then
                Start = _address.IndexOf(ProgramConstants.ChannelHeader, 0) + ProgramConstants.ChannelHeader.Length
                [End] = _address.IndexOf(ProgramConstants.ChannelFooter, Start)
                channel = _address.Substring(Start, [End] - Start)
            Else
                channel = "NULL"
            End If
            Return channel
        End Function



        'public static ABTagClass FindABTagClass(string _address)
        '{
        '    ABTagClass tagClass = new ABTagClass();
        '    int Start, End;

        '    if (_address.Contains(ProgramConstants.ABClassHeader) && _address.Contains(ProgramConstants.ABClassFooter))
        '    {
        '        Start = _address.IndexOf(ProgramConstants.ABClassHeader, 0) + ProgramConstants.ABClassHeader.Length;
        '        End = _address.IndexOf(ProgramConstants.ABClassFooter, Start);
        '        tagClass = (ABTagClass)Enum.Parse(typeof(ABTagClass), _address.Substring(Start, End - Start));
        '    }
        '    else
        '    {
        '        tagClass = ABTagClass.NotFound;
        '    }

        '    return tagClass;
        '}

        Public Shared Function FindTagAddress(_address As String) As String
            Dim tagAddress As String = ""
            Dim Start As Integer, [End] As Integer
            If _address.Contains(ProgramConstants.AddressHeader) AndAlso _address.Contains(ProgramConstants.AddressFooter) Then
                Start = _address.IndexOf(ProgramConstants.AddressHeader, 0) + ProgramConstants.AddressHeader.Length
                [End] = _address.IndexOf(ProgramConstants.AddressFooter, Start)
                tagAddress = _address.Substring(Start, [End] - Start)
            Else
                tagAddress = "NotFound"
            End If

            Return tagAddress
        End Function



        Public Shared Function DateTimeToString() As String
            Return DateTime.Now.ToString("d") + " " + DateTime.Now.ToString("T")
        End Function


        Public Shared PingTestResults As String = ""
        Public Shared Function PingTest_byIPAddress(_ipAddress As String) As Boolean
            PingTestResults = ""
            Dim pingSender As New Ping()
            Dim myIPPingReply As PingReply = Nothing

            myIPPingReply = pingSender.Send(_ipAddress)

            If myIPPingReply.Status = IPStatus.Success Then
                PingTestResults += "Address: " + myIPPingReply.Address.ToString() + Environment.NewLine
                PingTestResults += "RoundTrip time: " + myIPPingReply.RoundtripTime + Environment.NewLine
                PingTestResults += "Time to live: " + myIPPingReply.Options.Ttl + Environment.NewLine
                PingTestResults += "Don't fragment: " + myIPPingReply.Options.DontFragment + Environment.NewLine
                PingTestResults += "Buffer size: " + myIPPingReply.Buffer.Length + Environment.NewLine
                Return True
            Else
                PingTestResults = myIPPingReply.Status.ToString()
                Return False
            End If
        End Function
        Public Shared Function PingTest_byChannel(ChannelName As String) As Boolean
            Dim _ipAddress As String = Channels.GetChannel(ChannelName).IPAdress
            Return PingTest_byIPAddress(_ipAddress)
        End Function

        Public Shared Function IPAddressFromComputerName(_Name As String) As String
            Dim ipEntry As IPHostEntry = Dns.GetHostEntry(_Name)
            Dim ipAddress As IPAddress() = ipEntry.AddressList
            Return ipAddress(0).ToString()
        End Function


    End Class

    Public Class ComboboxItem
        Public Property Text() As String
            Get
                Return m_Text
            End Get
            Set
                m_Text = Value
            End Set
        End Property
        Private m_Text As String
        Public Property Value() As Object
            Get
                Return m_Value
            End Get
            Set
                m_Value = Value
            End Set
        End Property
        Private m_Value As Object
        Public Property Tag() As Object
            Get
                Return m_Tag
            End Get
            Set
                m_Tag = Value
            End Set
        End Property
        Private m_Tag As Object

        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class
End Namespace
