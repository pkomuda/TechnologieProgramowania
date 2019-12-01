using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace SerializationLibrary
{
    public class CSVSerialization<T>
    {
        private string FileName;
        private T ObjectToSerialize;

        public CSVSerialization(string fileName, T objectToSerialize)
        {
            this.FileName = fileName;
            this.ObjectToSerialize = objectToSerialize;
        }

        public void serialize()
        {
            CSVFormatter formatter = new CSVFormatter();
            File.Delete(FileName);
            using (Stream stream = File.Open(this.FileName, FileMode.Create, FileAccess.ReadWrite))
            {
                Console.WriteLine("CSVSerialization przed");
                formatter.Serialize(stream, this.ObjectToSerialize);
                Console.WriteLine("CSVSerialization po");
            }
        }

        //public T deserialize()
        //{
            //TO DO
        //    return null;
       // }
    }
}
