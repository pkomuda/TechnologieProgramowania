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
            throw new NotImplementedException();
        }

        private string Data = "";
        public override void Serialize(Stream serializationStream, object graph)
        {
            if (graph is ISerializable data)
            {
                SerializationInfo info = new SerializationInfo(graph.GetType(), new FormatterConverter());
                Binder.BindToName(graph.GetType(), out string assemblyName, out string typeName);
                Data += assemblyName + "|" + typeName + "|" + this.IDGenerator.GetId(graph, out bool firstTime);
                data.GetObjectData(info, Context);

                foreach (SerializationEntry item in info)
                {
                    WriteMember(item.Name, item.Value);
                }
                List<string> SaveIT = new List<string>();
                SaveIT.Add(Data + "\n");
                Data = "";
                while (this.m_objectQueue.Count != 0) //Contains a Queue of the objects left to serialize.
                {
                    this.Serialize(null, this.m_objectQueue.Dequeue());
                }

                if (serializationStream != null)
                {
                    using (StreamWriter streamWriter = new StreamWriter(serializationStream))
                    {
                        foreach (string s in SaveIT)
                        {
                            streamWriter.Write(s);
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
            Data += name + "|" + val.ToString("d", DateTimeFormatInfo.InvariantInfo) + "|"; 
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
                Data += name + "|" + obj.GetType() + "|" + (string)obj + "|";

            }
            else
            {
                if (null != obj)
                {
                    Data += "|" + name + "|" + obj.GetType() + "|" + IDGenerator.GetId(obj, out bool firstTime).ToString();
                    if (firstTime)
                    {
                        this.m_objectQueue.Enqueue(obj);
                    }
                }
                else
                {
                    Data += "|" + name + "|null|0";
                }
            }

        }

        protected override void WriteSByte(sbyte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteSingle(float val, string name)
        {
            this.Data += name + "|" + val.ToString("0.00", CultureInfo.InvariantCulture) + "|";
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
