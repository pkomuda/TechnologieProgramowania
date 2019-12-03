using System;
using System.IO;
using Newtonsoft.Json;

namespace SerializationLibrary
{
    public class JSONSerialization<T>
    {
        private string FileName;
        private T ObjectToSerialize;
        
        public JSONSerialization(string fileName, T objectToSerialize)
        {
            this.FileName = fileName;
            this.ObjectToSerialize = objectToSerialize;
        }
        
        public void serialize()
        {
            File.Delete(FileName);
            string json = JsonConvert.SerializeObject(this.ObjectToSerialize,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.All,
                    TypeNameHandling = TypeNameHandling.All
                });
            File.WriteAllText(this.FileName, json);
        }

        public T deserialize()
        {
            T deserializedObject;
            string json = File.ReadAllText(this.FileName);
            deserializedObject = (T) JsonConvert.DeserializeObject<T>(json,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.All,
                    TypeNameHandling = TypeNameHandling.All
                });
            return deserializedObject;
        }
    }
}
