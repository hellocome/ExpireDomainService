using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;

namespace ExpireDomainService.Common.Reflection
{
    public static class ObjectHelper
    {
        public static T Create<T>(string implementationClass) where T : class
        {
            Type tType = Type.GetType(implementationClass);
            Object implObj = Activator.CreateInstance(tType);
            return implObj as T;
        }

        public static T CreateFrom<T>(string assemblyPath, string implementationClass) where T : class
        {
            return Activator.CreateInstanceFrom(assemblyPath, implementationClass).Unwrap() as T;
        }

        public static T Create<T>(string assemblyName, string implementationClass) where T : class
        {
            string fullPath = AppDomain.CurrentDomain.BaseDirectory + System.IO.Path.DirectorySeparatorChar + assemblyName;
            return Activator.CreateInstanceFrom(fullPath, implementationClass).Unwrap() as T;
        }

        public static T CreateFrom<T>(string assemblyPath, string implementationClass, params object[] args) where T : class
        {
            Assembly asm = Assembly.LoadFrom(assemblyPath);
            Type type = asm.GetType(implementationClass);

            return Activator.CreateInstance(type, args) as T;
        }

        public static T Create<T>(string assemblyName, string implementationClass, params object[] args) where T : class
        {
            string fullPath = AppDomain.CurrentDomain.BaseDirectory + System.IO.Path.DirectorySeparatorChar + assemblyName;
            return CreateFrom<T>(assemblyName, implementationClass, args);
        }

    }
}
