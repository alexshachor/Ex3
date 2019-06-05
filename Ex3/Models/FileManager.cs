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

            string path = GetPath(fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (writer = File.OpenWrite(path))
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

            string path = GetPath(fileName);
            if (!File.Exists(path))
            {
                throw new Exception("file not found");
            }

            using (reader = File.OpenRead(path))
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