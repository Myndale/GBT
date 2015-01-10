using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GBT.UI.Converters
{
	public class StripConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return "";
			var result = value.ToString();
			if (parameter != null)
				foreach (var ch in parameter.ToString().ToCharArray())
					result = result.Replace(ch.ToString(), "");
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
