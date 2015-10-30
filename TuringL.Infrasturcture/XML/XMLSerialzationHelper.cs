using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace TuringL.Infrasturcture.XML
{
    public class XMLSerialzationHelper
    {
        private static object _lock=new object();

        public static object ReadXml<T>(string path)
        {
            lock(_lock)
            {
                if (File.Exists(path))
                {
                    using (StreamReader streamReader = new StreamReader(path))
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(T));
                        return xml.Deserialize(streamReader) as object;
                    }
                }
                return null;
            }
        }

        public static void WriteXML<T>(string path,T t)
        {
            lock (_lock)
            {
                if (File.Exists(path))
                {
                    using (StreamWriter streamWriter = new StreamWriter(path))
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(T));
                        xml.Serialize(streamWriter, t);
                    }
                }
            }
        }
    }
}
