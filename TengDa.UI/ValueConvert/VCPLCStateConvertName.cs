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
    public class VCPLCStateConvertName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                PLCStateType plcState=(PLCStateType)value;
                return plcState.FetchDescription();                
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
