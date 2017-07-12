using System;
using System.IO;
using System.Xml.Serialization;



namespace Common
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class XMLTools
    {

        public static void ToXML<T>(T _obj, string _sConfigFilePath, Type[] _extraTypes = null)
        {
            XmlSerializer myXML = default(XmlSerializer);
            if (_extraTypes == null)
            {
                myXML = new XmlSerializer(typeof(T));
            }
            else
            {
                myXML = new XmlSerializer(typeof(T), _extraTypes);
            }
            System.Xml.XmlWriterSettings MySettings = new System.Xml.XmlWriterSettings();
            MySettings.Indent = true;
            MySettings.CloseOutput = true;
            System.Xml.XmlWriter myWriter = System.Xml.XmlWriter.Create(_sConfigFilePath, MySettings);
            myXML.Serialize(myWriter, _obj);
            myWriter.Flush();
            myWriter.Close();

        }

        public static T FromXML<T>(string _sConfigFilePath, Type[] _extraTypes = null)
        {
            XmlSerializer myXML = default(XmlSerializer);
            if (_extraTypes == null)
                myXML = new XmlSerializer(typeof(T));
            else
                myXML = new XmlSerializer(typeof(T), _extraTypes);
            FileStream fs = new FileStream(_sConfigFilePath, FileMode.Open);
            System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(fs);

            if (myXML.CanDeserialize(reader))
            {
                object tempObject = (T)myXML.Deserialize(reader);
                reader.Close();
                return (T)tempObject;
            }
            else
            {
                return default(T);
            }
        }



    }
}

