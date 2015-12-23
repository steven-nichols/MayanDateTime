using System;
using System.Reflection;

namespace MayanDate
{
    public static class EnumHelper
    {
        public static T GetAttribute<T>(this Enum enumValue)
            where T : Attribute
        {
            return enumValue
                .GetType()
                .GetMember(enumValue.ToString())[0]
                .GetCustomAttribute<T>();
        }
    }
}
