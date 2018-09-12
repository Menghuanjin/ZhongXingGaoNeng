using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace TengDa.UI.ValueConvert
{
    public class VCItemIsCheckedColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null&& (bool)value)
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF49A9FF"));
            }
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF207BBC"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
