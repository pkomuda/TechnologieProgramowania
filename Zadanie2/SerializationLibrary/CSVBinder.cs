﻿using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace SerializationLibrary
{
    class CSVBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            return assembly.GetType(typeName);
        }
        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = serializedType.Assembly.FullName;
            typeName = serializedType.FullName;

        }
    }
}
