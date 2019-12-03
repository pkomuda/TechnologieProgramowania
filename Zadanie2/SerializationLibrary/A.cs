using System;
using System.Globalization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SerializationLibrary
{
    public class A : ISerializable
    {
        public string Name { get; set; }
        public float Number { get; set; }
        public DateTime Date { get; set; }
        public B ObjectB { get; set; }

        [JsonConstructor]
        public A(string name, float number, DateTime date, B objectB)
        {
            Name = name;
            Number = number;
            Date = date;
            ObjectB = objectB;
        }
        public A(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Number = (float)info.GetValue("Number", typeof(float));
            Date = (DateTime)info.GetValue("Date", typeof(DateTime));
            ObjectB = (B)info.GetValue("ObjectB", typeof(B));
        }
        public override string ToString()
        {
            return "A: " + this.Name + " Date: " + this.Date.ToString(CultureInfo.InvariantCulture) + " Number: " + this.Number;
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Number", Number);
            info.AddValue("Date", Date);
            info.AddValue("ObjectB", ObjectB, typeof(B));
        }
    }
}
