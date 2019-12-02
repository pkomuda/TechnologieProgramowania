using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace SerializationLibrary
{
    public class B : ISerializable
    {
        public string Name { get; set; }
        public float Number { get; set; }
        public DateTime Date { get; set; }
        public C ObjectC { get; set; }

        public B(string name, float number, DateTime date, C objectC)
        {
            Name = name;
            Number = number;
            Date = date;
            ObjectC = objectC;
        }
        public B(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Number = (float)info.GetValue("Number", typeof(float));
            Date = (DateTime)info.GetValue("Date", typeof(DateTime));
            ObjectC = (C)info.GetValue("ObjectC", typeof(C));
        }
        public override string ToString()
        {
            return "B: " + this.Name + " Date: " + this.Date.ToString(CultureInfo.InvariantCulture) + " Number: " + this.Number;
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Number", Number);
            info.AddValue("Date", Date);
            info.AddValue("ObjectC", ObjectC, typeof(C));
        }
    }
}
