using System;
using System.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace piggy_bank_uwp.Convertes
{
    public class ValueToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is bool)
            {
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;
            }
            else if(value is IEnumerable)
            {
                return (value as IEnumerable).GetEnumerator().MoveNext() ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
