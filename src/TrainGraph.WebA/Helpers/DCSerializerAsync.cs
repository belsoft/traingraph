using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace TISServiceHelper
{
    public class DCSerializerAsync
    {
        public static Task<T> DeserializeObjectAsync<T>(string serialized)
        {
            return Task.Run(() =>
            {
                DataContractSerializer serializer =
                           new DataContractSerializer(typeof(T));
                var tempByte = Convert.FromBase64String(serialized);
                string tempXml = System.Text.Encoding.UTF8.GetString(tempByte);
                using (StringReader reader = new StringReader(tempXml))
                {
                    using (XmlReader xmlReader = XmlReader.Create(reader))
                    {
                       
                        T theObject = (T)serializer.ReadObject(xmlReader);
                        return theObject;
                    }
                }
            });
            // return Task.Run(() => {
               
            //    if (serialized == null)
            //    {
            //        throw new ArgumentNullException("serialized");
            //    }
            //    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            //    using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(serialized)))
            //    {
            //        T theObject = (T)serializer.ReadObject(stream);
            //        return theObject;
            //    }
            //});
        }
    }
}