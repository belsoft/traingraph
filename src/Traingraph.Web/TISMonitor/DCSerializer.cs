using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Traingraph.Web.TISMonitor
{
    public class DCSerializer
    {
        public static object DeserializeWithDCS(Type type, string serialized)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (serialized == null)
            {
                throw new ArgumentNullException("serialized");
            }
            DataContractSerializer serializer = new DataContractSerializer(type);
            using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(serialized)))
            {
                return serializer.ReadObject(stream);
            }
        }

        public static object DeserializeWithDCSMS(Type type, byte[] data)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            DataContractSerializer serializer = new DataContractSerializer(type);
            using (MemoryStream stream = new MemoryStream(data))
            {
                return serializer.ReadObject(stream);
            }
        }

        public static string SerializeWithDCS(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                return Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Position);
            }
        }

        public static byte[] SerializeWithDCSMS(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                return stream.ToArray();
            }
        }
    }
}