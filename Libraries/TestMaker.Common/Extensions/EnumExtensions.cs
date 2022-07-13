using System.Reflection;
using TestMaker.Common.Attributes;

namespace TestMaker.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string? GetEnumName(this Enum value)
        {
            Type type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name != null)
            {
                var field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(field, typeof(EnumNameAttribute)) is EnumNameAttribute attr)
                    {
                        return attr.Name;
                    }
                }
            }
            return null;
        }
    }
}
