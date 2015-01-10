using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GBT.UI.Converters
{
	public class EqualityConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if ((values == null) || (values.Count() != 2))
				return false;
			if ((values[0]==null) && (values[1]==null))
				return true;
			return values[0].Equals(values[1]);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
