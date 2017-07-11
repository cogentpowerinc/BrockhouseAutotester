using System;
using System.Net;
using System.Net.NetworkInformation;
using static PLC.CommManager;

namespace Common
{
    public class Core
    {
        public static string FindChannelName(string _address)
        {
            string channel = "";
            int Start, End;
            if (_address.Contains(ProgramConstants.ChannelHeader) && _address.Contains(ProgramConstants.ChannelFooter))
            {
                Start = _address.IndexOf(ProgramConstants.ChannelHeader, 0) + ProgramConstants.ChannelHeader.Length;
                End = _address.IndexOf(ProgramConstants.ChannelFooter, Start);
                channel = _address.Substring(Start, End - Start);
            }
            else
                channel = "NULL";
            return channel;
        }



       
        public static string FindTagAddress(string _address)
        {
            string tagAddress = "";
            int Start, End;
            if (_address.Contains(ProgramConstants.AddressHeader) && _address.Contains(ProgramConstants.AddressFooter))
            {
                Start = _address.IndexOf(ProgramConstants.AddressHeader, 0) + ProgramConstants.AddressHeader.Length;
                End = _address.IndexOf(ProgramConstants.AddressFooter, Start);
                tagAddress = _address.Substring(Start, End - Start);
            }
            else
            {
                tagAddress = "NotFound";
            }

            return tagAddress;
        }



        public static string DateTimeToString()
        {
            return DateTime.Now.ToString("d") + " " + DateTime.Now.ToString("T");
        }


        public static string PingTestResults = "";
        public static bool PingTest_byIPAddress(string _ipAddress)
        {
            PingTestResults = "";
            Ping pingSender = new Ping();
            PingReply myIPPingReply = null;

            myIPPingReply = pingSender.Send(_ipAddress);

            if (myIPPingReply.Status == IPStatus.Success)
            {
                PingTestResults += "Address: " + myIPPingReply.Address.ToString() + Environment.NewLine;
                PingTestResults += "RoundTrip time: " + myIPPingReply.RoundtripTime + Environment.NewLine;
                PingTestResults += "Time to live: " + myIPPingReply.Options.Ttl + Environment.NewLine;
                PingTestResults += "Don't fragment: " + myIPPingReply.Options.DontFragment + Environment.NewLine;
                PingTestResults += "Buffer size: " + myIPPingReply.Buffer.Length + Environment.NewLine;
                return true;
            }
            else
            {
                PingTestResults = myIPPingReply.Status.ToString();
                return false;
            }
        }
        public static bool PingTest_byChannel(string ChannelName)
        {
            string _ipAddress = Channels.GetChannel(ChannelName).IPAdress;
            return PingTest_byIPAddress(_ipAddress);
        }

        public static string IPAddressFromComputerName(string _Name)
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(_Name);
            IPAddress[] ipAddress = ipEntry.AddressList;
            return ipAddress[0].ToString();
        }

        
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public object Tag { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
