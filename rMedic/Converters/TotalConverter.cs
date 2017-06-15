using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace rMedic.Converters
{
    public class TotalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            double sum = 0.0;

            Type valueType = value.GetType();

            if (valueType.Name == typeof(ObservableCollection<>).Name)
            {
                foreach (var item in (ICollection)value)
                {
                    Type itemType = item.GetType();
                    PropertyInfo itemPropertyInfo = itemType.GetProperty((string)parameter);
                    double itemValue = ((double)itemPropertyInfo.GetValue(item, null));
                    MessageBox.Show(itemValue.ToString());
                    sum += itemValue;
                }
                return sum;
            }
            return 0.0;
        }
        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        { throw new NotImplementedException(); }
    }
}
