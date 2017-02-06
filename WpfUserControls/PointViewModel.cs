using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using NewellClark.PropertyTracking;
using System.Windows;

namespace NewellClark.Wpf.UserControls
{
	public class PointViewModel : INotifyPropertyChanged
	{
		public PointViewModel()
		{
			_tracker = new PropertyTracker(e => PropertyChanged?.Invoke(this, e));

			_x = _tracker.CreateProperty<double>(nameof(X));
			_y = _tracker.CreateProperty<double>(nameof(Y));
			_value = _tracker.CreateProperty(
				nameof(Value), 
				() => new Point(X, Y), 
				v => { X = v.X; Y = v.Y; });
		}
		public event PropertyChangedEventHandler PropertyChanged;

		public Point Value
		{
			get { return _value.Value; }
			set { _value.Value = value; }
		}
		private TrackedProperty<Point> _value;

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X")]
		public double X
		{
			get { return _x.Value; }
			set { _x.Value = value; }
		}
		private TrackedProperty<double> _x;

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y")]
		public double Y
		{
			get { return _y.Value; }
			set { _y.Value = value; }
		}
		private TrackedProperty<double> _y;

		private PropertyTracker _tracker;
	}
}
