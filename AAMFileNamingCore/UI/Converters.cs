using AAMFileNamingCore.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AAMFileNamingCore.UI
{
    internal class TypeCodeEnabledConverter : IValueConverter
    {
        // WPF converter
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return false;

            List<string> AcceptableTypeCodes = new List<string> { "DR", "SP", "SH", };

            if (value is DocumentTypeIso dt)
            {
                var abbrev = NamingLists.GetAbbreviation(dt);

                if (abbrev != null && AcceptableTypeCodes.Contains(abbrev))
                    return true;
            }

            if (value is string valueString)
            {
                if (AcceptableTypeCodes.Contains(valueString))
                    return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
