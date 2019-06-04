using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace Ex3.Models
{
    public class FileManager<T>
    {
        public bool SaveData(string fileName, T dataCollection)
        {
            bool hasDataSaved = false;
            Stream writer = null;

            if (fileName == String.Empty)
            {
                return hasDataSaved;
            }

            using (writer = File.OpenWrite(GetPath(fileName)))
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

            using (reader = File.OpenRead(GetPath(fileName)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }

        public string GetPath(string fileName)
        {
            return HttpContext.Current.Server.MapPath(String.Format("~/App_Data/{0}.xml", fileName));
        }
    }
}