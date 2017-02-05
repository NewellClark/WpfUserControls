using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewellClark.PropertyTracking;
using System.Windows;
using System.ComponentModel;

namespace NewellClark.Wpf.UserControls
{
	class RectViewModel : INotifyPropertyChanged
	{
		public RectViewModel()
		{
			_tracker = new PropertyTracker(e => PropertyChanged?.Invoke(this, e));

			_left = _tracker.CreateProperty<double>(nameof(Left));
			_top = _tracker.CreateProperty<double>(nameof(Top));
			_width = _tracker.CreateProperty<double>(nameof(Width));
			_height = _tracker.CreateProperty<double>(nameof(Height));
			_right = _tracker.CreateProperty(
				nameof(Right), () => Left + Width, v => Left = v - Width);
			_bottom = _tracker.CreateProperty(
				nameof(Bottom), () => Top + Height, v => Top = v - Height);
			_value = _tracker.CreateProperty(
				nameof(Value),
				() => new Rect(Left, Top, Width, Height),
				v => { Left = v.Left; Top = v.Top; Width = v.Width; Height = v.Height; });
		}
		public event PropertyChangedEventHandler PropertyChanged;

		public Rect Value
		{
			get { return _value.Value; }
			set { _value.Value = value; }
		}
		private TrackedProperty<Rect> _value;

		public double Left
		{
			get { return _left.Value; }
			set { _left.Value = value; }
		}
		private TrackedProperty<double> _left;

		public double Top
		{
			get { return _top.Value; }
			set { _top.Value = value; }
		}
		private TrackedProperty<double> _top;

		public double Right
		{
			get { return _right.Value; }
			set { _right.Value = value; }
		}
		private TrackedProperty<double> _right;

		public double Bottom
		{
			get { return _bottom.Value; }
			set { _bottom.Value = value; }
		}
		private TrackedProperty<double> _bottom;

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
