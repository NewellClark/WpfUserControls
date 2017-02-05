using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace NewellClark.Wpf.UserControls.TypeConverters
{
	/// <summary>
	/// Converts a boolean to a pair of specified values (for example, red/green, or 
	/// Visible/Hidden). 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class BooleanToToggleConverter<T> : IValueConverter
	{
		public BooleanToToggleConverter() { }
		public BooleanToToggleConverter(T @true, T @false)
		{
			True = @true;
			False = @false;
		}
		public T True { get; set; }
		public T False { get; set; }

		public T Convert(bool value)
		{
			if (value)
				return True;
			return False;
		}

		public bool ConvertBack(T value)
		{
			bool? result = null;
			if (EqualityComparer<T>.Default.Equals(value, True))
				result = true;
			else if (EqualityComparer<T>.Default.Equals(value, False))
				result = false;

			if (result == null)
				throw new ArgumentException($"{value} must be either {True} or {False}");

			return result.Value;
		}

		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Convert((bool)value);
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var casted = (T)value;
			return ConvertBack(casted);
		}
	}
}
