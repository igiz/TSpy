using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DesktopView.WPF.Converters
{
	/// <summary>Converts a <see cref="bool"/> to a given <see cref="Enum"/> value.</summary>
	internal class BoolToEnumConverter : IValueConverter
	{
		#region Public Methods

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object result = value.Equals(parameter);
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object result = value.Equals(true) ? result = parameter : DependencyProperty.UnsetValue;
			return result;
		}

		#endregion
	}
}