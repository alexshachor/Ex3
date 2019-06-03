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

            try
            {
                using (writer = File.OpenWrite(fileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(writer, dataCollection);
                }
            }
            catch (IOException)
            {
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    hasDataSaved = true;
                }
            }

            return hasDataSaved;
        }

        public T LoadData(string fileName)
        {
            T dataCollection;
            Stream reader = null;

            try
            {
                using (reader = File.OpenRead(fileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    dataCollection = (T)serializer.Deserialize(reader);
                }
            }
            catch (IOException)
            {
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return dataCollection;
        }
    }
}