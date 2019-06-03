using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Ex3.Models
{
    public class FileManager<T> where T : ISerializable
    {
        public bool SaveData(string fileName, T dataCollection)
        {
            bool hasDataSaved = false;
            Stream writer = null;

            using (writer = File.OpenWrite(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, dataCollection);
                hasDataSaved = true;
            }

            return hasDataSaved;
        }

        public T LoadData(string fileName)
        {
            Stream reader = null;
            
            using (reader = File.OpenRead(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}