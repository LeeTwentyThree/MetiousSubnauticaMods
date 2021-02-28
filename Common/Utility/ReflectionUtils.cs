using System;
using System.Reflection;

namespace Common.Utility
{
    public static class ReflectionUtils
    {
        public static Type GetTypeFromAssembly(string typeName, string assemblyName)
        {
            Type type = Type.GetType($"{typeName}, {assemblyName}", false, false);
            return type;
        }
        public static MethodInfo GetMethod(Type declaringType, string methodName, bool isStatic = false)
        {
            MethodInfo method = declaringType.GetMethod(methodName, BindingFlags.NonPublic | (isStatic ? BindingFlags.Static : BindingFlags.Instance));
            return method;
        }
        public static FieldInfo GetField(Type declaringType, string fieldName, bool isStatic = false)
        {
            FieldInfo field = declaringType.GetField(fieldName, BindingFlags.NonPublic | (isStatic ? BindingFlags.Static : BindingFlags.Instance));
            return field;
        }
    }
}