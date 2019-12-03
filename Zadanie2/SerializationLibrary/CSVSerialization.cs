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
        private CSVFormatter Formatter;

        public CSVSerialization(string fileName, T objectToSerialize)
        {
            this.FileName = fileName;
            this.ObjectToSerialize = objectToSerialize;
            this.Formatter = new CSVFormatter();
        }

        public void serialize()
        {
            File.Delete(FileName);
            using (Stream stream = File.Open(this.FileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                Formatter.Serialize(stream, this.ObjectToSerialize);
            }
        }

        public T deserialize()
        {
            T deserializedObject;
            using (Stream stream = File.Open(this.FileName, FileMode.Open, FileAccess.Read))
            {
                deserializedObject = (T)Formatter.Deserialize(stream);
            }
            return deserializedObject;
        }
    }
}
