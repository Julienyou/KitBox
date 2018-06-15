using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace KitBox.WPFcore.Converter
{
    public class OppositeBoolToVisibility : IValueConverter
    {
        BooleanToVisibilityConverter boolToVis = new BooleanToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {

            return boolToVis.Convert(!(bool)value, targetType, parameter, language);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            return boolToVis.ConvertBack(!(bool)value, targetType, parameter, language);
        }
    }
}
