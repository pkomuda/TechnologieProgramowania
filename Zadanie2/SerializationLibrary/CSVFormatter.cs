using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
            Dictionary<object, SerializationInfo> data = new Dictionary<object, SerializationInfo>();
            Dictionary<SerializationInfo, List<Tuple<string, Type, long>>> allObjects = new Dictionary<SerializationInfo, List<Tuple<string, Type, long>>>();

            using (StreamReader streamReader = new StreamReader(serializationStream))
            {
                string line;
                string[] splitValues;
                long refID;
                Type objectType;
                SerializationInfo info;
                Object tmpObject;
                string[] properties;

                while (!streamReader.EndOfStream)
                {
                    line = streamReader.ReadLine();
                    Console.WriteLine("line: " + line);
                    splitValues = line.Split(';');
                    for (int i = 0; i < 15; i++)
                        Console.WriteLine("splitValues[" + i + "]: " + splitValues[i]);

                    refID = long.Parse(splitValues[2]);
                    objectType = Binder.BindToType(splitValues[0], splitValues[1]);
                    info = new SerializationInfo(objectType, new FormatterConverter());
                    tmpObject = FormatterServices.GetUninitializedObject(objectType); //Creates a new instance of the specified object type.

                    if (deserializedObject == null)
                        deserializedObject = tmpObject;

                    data.Add(tmpObject, info);
                    deserializedObjects.Add(refID, tmpObject);

                    properties = streamReader.ReadLine().Split(';');
                    foreach (string property in properties)
                    {
                        string[] parts = property.Split('=');

                        Console.WriteLine("property: " + property);
                        Console.WriteLine("Parts:[0] " + parts[0]);
                        Console.WriteLine("Parts:[1] " + parts[1]);
                        Console.WriteLine("Parts:[2] " + parts[2]);
                        if (parts[2].StartsWith("&"))
                        {
                            /* if (!pendingObjects.ContainsKey(serializationInfo))
                             {
                                 pendingObjects.Add(serializationInfo, new List<Tuple<string, Type, long>>());
                             }
                             if (parts[2].StartsWith("&-1"))
                             {
                                 Type type = Type.GetType(parts[0]);
                                 serializationInfo.AddValue(parts[1], null, type);
                             }
                             else
                             {
                                 pendingObjects[serializationInfo].Add(new Tuple<string, Type, long>(parts[1], Type.GetType(parts[0]), long.Parse(parts[2].Substring(1))));
                             }*/
                        }
                        else
                        {
                            // Type type = Type.GetType(parts[0]);
                            // serializationInfo.AddValue(parts[1], Convert.ChangeType(parts[2], type), type);
                        }
                    }

                }

                /*  foreach (var pair in pendingObjects)
                  {
                      SerializationInfo serializationInfo = pair.Key;
                      foreach (var tuple in pair.Value)
                      {
                          serializationInfo.AddValue(tuple.Item1, deserializedObjects[tuple.Item3], tuple.Item2);
                      }
                  }

                  foreach (var pair in deserializationData)
                  {
                      pair.Key.GetType().GetConstructor(new Type[] { typeof(SerializationInfo), typeof(StreamingContext) }).Invoke(pair.Key, new object[] { pair.Value, Context });
                  }*/
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
                Data += assemblyName + ";" + typeName + ";" + this.IDGenerator.GetId(graph, out bool firstTime) + ";";
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
            Data += name + ";" + val.GetType() + ";" + val.ToString("d", DateTimeFormatInfo.InvariantInfo) + ";";
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
                Data += name + ";" + obj.GetType() + ";" + (string)obj + ";";
            }
            else
            {
                if (null != obj)
                {
                    Data += name + ";" + obj.GetType() + ";" + IDGenerator.GetId(obj, out bool firstTime).ToString();
                    if (firstTime)
                    {
                        this.m_objectQueue.Enqueue(obj);
                    }
                }
                else
                {
                    Data += name + ";null;0";
                }
            }
        }

        protected override void WriteSByte(sbyte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteSingle(float val, string name)
        {
            this.Data += name + ";" + val.GetType() + ";" + val.ToString("0.00", CultureInfo.InvariantCulture) + ";";
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
