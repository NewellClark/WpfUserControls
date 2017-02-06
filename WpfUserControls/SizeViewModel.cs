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
	public class SizeViewModel : INotifyPropertyChanged
	{
		public SizeViewModel()
		{
			_tracker = new PropertyTracker(e => PropertyChanged?.Invoke(this, e));

			_width = _tracker.CreateProperty<double>(nameof(Width));
			_height = _tracker.CreateProperty<double>(nameof(Height));
			_value = _tracker.CreateProperty(
				nameof(Value),
				() => new Size(Width, Height),
				v => { Width = v.Width; Height = v.Height; });
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public Size Value
		{
			get { return _value.Value; }
			set { _value.Value = value; }
		}
		private TrackedProperty<Size> _value;

		public double Width
		{
			get { return _width.Value; }
			set { _width.Value = value; }
		}
		private TrackedProperty<double> _width;

		public double Height
		{
			get { return _height.Value; }
			set { _height.Value = value; }
		}
		private TrackedProperty<double> _height;

		private PropertyTracker _tracker;
	}
}
