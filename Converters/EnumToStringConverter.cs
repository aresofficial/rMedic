using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace rMedic.Converters
{
    [ValueConversion(typeof(Enum), typeof(string))]
    public class EnumToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            { return null; }

            Type valueType = value.GetType();
            FieldInfo fieldInfo = valueType.GetField(value.ToString(), BindingFlags.Static | BindingFlags.Public);

            if (fieldInfo == null)
            { throw new ArgumentException("String not support!", "value"); }

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            { return attributes[0].Description; }
            else
            { return fieldInfo.Name; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            { return null; }

            string str = (string)value;

            foreach (FieldInfo fieldInfo in targetType.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                if (fieldInfo.Name == str)
                { return fieldInfo.GetValue(null); }

                DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                foreach (DescriptionAttribute attribute in attributes)
                {
                    if (attribute.Description == str)
                    { return fieldInfo.GetValue(null); }
                }
            }

            throw new ArgumentException($"Localization of {str} not found!", "value");
        }

        #endregion
    }
}
