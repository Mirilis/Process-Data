using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Data
{
    public static class XMLSerializer
    {
        public static object DeserializeXML(this string S)
        {
            object O;
            XmlSerializer xs = new XmlSerializer(typeof(object));
            using (TextReader reader = new StringReader(S))
            {
                O = (object)xs.Deserialize(reader);    
            }
            return O;
        }

        public static string ToXMLString(this object O)
        {

            string xml = null;
            using (StringWriter sw = new StringWriter())
            {

                XmlSerializer xs = new XmlSerializer(typeof(object));
                xs.Serialize(sw, O);
                try
                {
                    xml = sw.ToString();

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return xml;
        }
    }
    
}
