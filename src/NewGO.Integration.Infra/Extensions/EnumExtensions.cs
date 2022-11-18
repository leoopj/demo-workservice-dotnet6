using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace NewGO.Integration.Infra
{
    public static class EnumExtensions
    {
        public static string? GetDescription(this object value)
        {
            if (value.IsNull())
                return null;

            FieldInfo? fieldInfo = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.IsNotNull() && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string? GetDescription(this object value, string objJsonLog)
        {
            try
            {
                if (value.IsNull())
                    return null;

                FieldInfo? fieldInfo = value.GetType().GetField(value.ToString());

                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.IsNotNull() && attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return value.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                Debug.WriteLine(objJsonLog);
                throw;
            }
        }

        public static T ToEnumByValue<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static T ToEnumByDescription<T>(this string description)
        {
            var type = typeof(T);

            if (!type.IsEnum)
                throw new InvalidOperationException();

            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description.ToLower() == description.ToLower())
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name.ToLower() == description.ToLower())
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Enum not found.", $"[{description}]");
        }

        public static T ToEnumByDescription<T>(this string description, T defaultValue)
        {
            try
            {
                var type = typeof(T);
                if (!type.IsEnum) throw new InvalidOperationException();
                foreach (var field in type.GetFields())
                {
                    if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                    {
                        if (attribute.Description == description)
                            return (T)field.GetValue(null);
                    }
                    else
                    {
                        if (field.Name == description)
                            return (T)field.GetValue(null);
                    }
                }
            }
            catch
            {
            }

            return defaultValue;
        }
    }
}