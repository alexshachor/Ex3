using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    public class FileManager<T>
    {
        public bool SaveData(string fileName, T dataCollection)
        {
            bool hasDataSaved = false;

            //TODO: serialize the list, so it will be saved in the file.

            return hasDataSaved;
        }

        public T LoadData(string fileName)
        {
            T dataCollection;

            //TODO: deserialize the file, so we will get the data


            return dataCollection;
        }
    }
}