using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TengDa.Common;
using TengDa.Models.Common;

namespace TengDa.UI.ValueConvert
{
    public class VCBerthPositionConvertName : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                BerthPositionType berthPosition = (BerthPositionType)value;
                return berthPosition.FetchDescription();
            }
            return string.Empty;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
