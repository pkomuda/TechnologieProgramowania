using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace SerializationLibrary
{
    class CSVFormatter : Formatter
    {
        public override SerializationBinder Binder { get; set; }
        public override StreamingContext Context { get; set; }
        public override ISurrogateSelector SurrogateSelector { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObjectIDGenerator IDGenerator { get; set; }


        public CSVFormatter()
        {
            IDGenerator = new ObjectIDGenerator();
            Binder = new CSVBinder();
            Context = new StreamingContext(StreamingContextStates.File);
        }

        public override object Deserialize(Stream serializationStream)
        {
            object deserializedObject = null;
            Dictionary<long, object> deserializedObjects = new Dictionary<long, object>();
            Dictionary<object, SerializationInfo> objectSerializationInfo = new Dictionary<object, SerializationInfo>();
            Dictionary<SerializationInfo, List<Tuple<string, Type, long>>> waitingObjects = new Dictionary<SerializationInfo, List<Tuple<string, Type, long>>>();


            using (StreamReader streamReader = new StreamReader(serializationStream))
            {
                string[] objectValues;
                Type objectType;
                SerializationInfo sInfo;
                object tempObject;

                while (!streamReader.EndOfStream)
                {
                    objectValues = streamReader.ReadLine().Split(';');
                    objectType = Binder.BindToType(objectValues[0], objectValues[1]);
                    sInfo = new SerializationInfo(objectType, new FormatterConverter());
                    tempObject = FormatterServices.GetUninitializedObject(objectType);

                    if (deserializedObject == null)
                        deserializedObject = tempObject;

                    objectSerializationInfo.Add(tempObject, sInfo);
                    deserializedObjects.Add(long.Parse(objectValues[2]), tempObject);
                    char[] a = { ';' };
                    string[] properties = streamReader.ReadLine().Split(a, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string property in properties)
                    {
                        string[] tmp = property.Split('|');
                        if (tmp[2].StartsWith("ref"))
                        {
                            if (!waitingObjects.ContainsKey(sInfo))
                            {
                                waitingObjects.Add(sInfo, new List<Tuple<string, Type, long>>());
                            }
                            if (tmp[2] == "ref0")
                            {
                                sInfo.AddValue(tmp[1], null, Type.GetType(tmp[0]));
                            }
                            else
                            {
                                waitingObjects[sInfo].Add(new Tuple<string, Type, long>(tmp[1], Type.GetType(tmp[0]), long.Parse(tmp[2].Substring(3))));
                            }
                        }
                        else
                        {
                            sInfo.AddValue(tmp[1], Convert.ChangeType(tmp[2], Type.GetType(tmp[0]), CultureInfo.InvariantCulture), Type.GetType(tmp[0]));
                        }
                    }

                }
                foreach (KeyValuePair<SerializationInfo, List<Tuple<string, Type, long>>> keyValuePairWaitingObjects in waitingObjects)
                {
                    SerializationInfo serializationInfo = keyValuePairWaitingObjects.Key;
                    foreach (Tuple<string, Type, long> tuple in keyValuePairWaitingObjects.Value)
                    {
                        serializationInfo.AddValue(tuple.Item1, deserializedObjects[tuple.Item3], tuple.Item2);
                    }
                }

                foreach (KeyValuePair<object, SerializationInfo> keyValuePair in objectSerializationInfo)
                {
                    keyValuePair.Key.GetType().GetConstructor(
                                                                 new Type[] { typeof(SerializationInfo), typeof(StreamingContext) }
                                                             ).Invoke(keyValuePair.Key, new object[] { keyValuePair.Value, Context });
                }
            }

            return deserializedObject;
        }

        private string Data = "";
        private List<string> SaveIT = new List<string>();
        public override void Serialize(Stream serializationStream, object graph)
        {
            if (graph is ISerializable data)
            {
                SerializationInfo info = new SerializationInfo(graph.GetType(), new FormatterConverter());
                Binder.BindToName(graph.GetType(), out string assemblyName, out string typeName);
                Data += assemblyName + ";" + typeName + ";" + this.IDGenerator.GetId(graph, out bool firstTime) + ";\n";
                data.GetObjectData(info, Context);

                foreach (SerializationEntry item in info)
                {
                    WriteMember(item.Name, item.Value);
                }

                SaveIT.Add(Data + "\n");
                Data = "";
                while (this.m_objectQueue.Count != 0) //Contains a Queue of the objects left to serialize.
                {
                    this.Serialize(null, this.m_objectQueue.Dequeue());
                }

                if (serializationStream != null)
                {
                    using (StreamWriter _stream = new StreamWriter(serializationStream))
                    {
                        foreach (string s in SaveIT)
                        {
                            byte[] _content = Encoding.ASCII.GetBytes(s);
                            _stream.Write(s, 0, _content.Length);
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException("Serialization is not possible. (not ISerializable)");
            }
        }

        protected override void WriteArray(object obj, string name, Type memberType)
        {
            throw new NotImplementedException();
        }

        protected override void WriteBoolean(bool val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteByte(byte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteChar(char val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDateTime(DateTime val, string name)
        {
            Data += val.GetType() + "|" + name + "|" + val.ToString("d", DateTimeFormatInfo.InvariantInfo) + ";";
        }

        protected override void WriteDecimal(decimal val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDouble(double val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt16(short val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt32(int val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt64(long val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteObjectRef(object obj, string name, Type memberType)
        {
            if (memberType.Equals(typeof(String)))
            {
                Data += obj.GetType() + "|" + name + "|" + (string)obj + ";";
            }
            else
            {
                if (null != obj)
                {
                    Data += obj.GetType() + "|" + name + "|ref" + IDGenerator.GetId(obj, out bool firstTime).ToString() + ";";
                    if (firstTime)
                    {
                        this.m_objectQueue.Enqueue(obj);
                    }
                }
                else
                {
                    Data += memberType + "|" + name + "|ref0;";
                }
            }
        }

        protected override void WriteSByte(sbyte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteSingle(float val, string name)
        {
            this.Data += val.GetType() + "|" + name + "|" + val.ToString(CultureInfo.InvariantCulture) + ";";
        }

        protected override void WriteTimeSpan(TimeSpan val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt16(ushort val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt32(uint val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt64(ulong val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteValueType(object obj, string name, Type memberType)
        {
            throw new NotImplementedException();
        }
    }
}
