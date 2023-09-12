using System.ComponentModel;
using System.Reflection;

namespace TaxService.Common
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fieldInfo.GetCustomAttributes
                (typeof(DescriptionAttribute), false);

            return attributes.Length > 0
                ? attributes[0].Description
                : enumValue.ToString();
        }
        public static List<K> GetEnumItems<K>()
        {
            if (typeof(K).BaseType != typeof(Enum))
            {
                throw new InvalidCastException();
            }

            return Enum.GetValues(typeof(K)).Cast<K>().ToList();
        }
        public static TEnum GetEnum<TEnum>(this string value)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value, true);
        }
    }
}
