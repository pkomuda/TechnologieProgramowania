using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace SerializationLibrary
{
    class CSVFormatter : Formatter
    {
        public override SerializationBinder Binder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override StreamingContext Context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override ISurrogateSelector SurrogateSelector { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObjectIDGenerator IDGenerator { get; set; }

        private string Content = "";
        private bool FirtsTime;

        public CSVFormatter()
        {
            IDGenerator = new ObjectIDGenerator();
        }

        public override object Deserialize(Stream serializationStream)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(Stream serializationStream, object graph)
        {
            if (graph is ISerializable data)
            {
                SerializationInfo info = new SerializationInfo(graph.GetType(), new FormatterConverter());
                info.AddValue("ID", IDGenerator.GetId(graph, out FirtsTime));
                info.AddValue("Type", graph.GetType().FullName);
                StreamingContext streamingContext = new StreamingContext(StreamingContextStates.File);
                data.GetObjectData(info, streamingContext);
                foreach (SerializationEntry entry in info)
                {
                    if (entry.Value != null && entry.Value is ISerializable && entry.Value.GetType() != typeof(DateTime))
                    {
                        WriteMember(entry.Name, entry.Value);
                        if (FirtsTime == true)
                        {
                            Serialize(serializationStream, entry.Value);
                        }
                    }
                    else
                    {
                        WriteMember(entry.Name, entry.Value);
                    }
                }
                byte[] content = Encoding.UTF8.GetBytes(this.Content);
                serializationStream.Write(content, 0, content.Length);
                this.Content = "";
            }
            else
            {
                throw new ArgumentException("Implementation of is mandatory");
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        protected override void WriteSByte(sbyte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteSingle(float val, string name)
        {
            this.Content += name + "|" + val.ToString("0.00", CultureInfo.InvariantCulture) + "|";
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
