using NewellClark.Wpf.UserControls.TypeConverters;
using NewellClark.Wpf.UserControls.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewellClark.Wpf.UserControls.Views
{
	/// <summary>
	/// A UI widget that allows users to enter a .NET-flavored Regular Expression.
	/// Text changes color to indicate when the pattern entered by the user is invalid. 
	/// </summary>
	public partial class RegexInputWidgetHorizontal : UserControl
	{
		//public RegexInputWidgetHorizontal()
		//{
		//	_textColorConverter = new BooleanToColorBrushConverter(
		//		Colors.Black, Colors.Red);

		//	InitializeComponent();

		//	_viewModel = new RegexViewModel();
		//	DataContext = _viewModel;

		//	InitializeDesignerOnlyProperties();
		//	InitializeEventHandlers();
		//	InitializeIsValidToTextBoxTextColorBinding();
		//	InitializeRegexPropertyDataBinding();
		//}
		#region
		//private void InitializeDesignerOnlyProperties()
		//{
		//	_validTextColor = new DesignerOnlyProperty<Color>(
		//		this,
		//		() => _textColorConverter.TrueColor,
		//		x => _textColorConverter.TrueColor = x);
		//	_invalidTextColor = new DesignerOnlyProperty<Color>(
		//		this,
		//		() => _textColorConverter.FalseColor,
		//		x => _textColorConverter.FalseColor = x);
		//}

		//private void InitializeEventHandlers()
		//{
		//	_viewModel.PropertyChanged += (o, e) =>
		//	{
		//		if (e.PropertyName == nameof(_viewModel.IsValid))
		//		{
		//			SetValue(IsValidPropertyKey, _viewModel.IsValid);
		//		}
		//		else if (e.PropertyName == nameof(_viewModel.Value))
		//		{
		//			Regex = _viewModel.Value;
		//		}
		//	};
		//}

		//private void InitializeRegexPropertyDataBinding()
		//{
		//	var binding = new Binding(nameof(_viewModel.Value));
		//	binding.Source = _viewModel;
		//	binding.Mode = BindingMode.TwoWay;
		//	binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
		//	SetBinding(RegexProperty, binding);
		//}

		//private void InitializeIsValidToTextBoxTextColorBinding()
		//{
		//	var textBinding = new Binding(nameof(_viewModel.IsValid));
		//	textBinding.Mode = BindingMode.OneWay;
		//	textBinding.Converter = _textColorConverter;
		//	textBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
		//	pattern.SetBinding(TextBox.ForegroundProperty, textBinding);
		//}

		///// <summary>
		///// Gets or sets the regular expression that is currently entered in the control. 
		///// Returns null if the currently-entered regex is invalid.
		///// </summary>
		////[EditorBrowsable(EditorBrowsableState.Never)]
		//public Regex Regex
		//{
		//	//get { return _viewModel.Value; }
		//	//set { _viewModel.Value = value; }
		//	get { return (Regex)GetValue(RegexProperty); }
		//	set
		//	{
		//		if (Regex != value)
		//		{
		//			SetValue(RegexProperty, value);
		//			RegexChanged?.Invoke(this, new EventArgs());
		//		}
		//	}
		//}

		//public event EventHandler RegexChanged;

		///// <summary>
		///// Indicates whether the currently-entered pattern and options form a valid regular expression.
		///// </summary>
		//[EditorBrowsable(EditorBrowsableState.Never)]
		//public bool IsValid
		//{
		//	get { return (bool)GetValue(IsValidPropertyKey.DependencyProperty); }
		//}

		///// <summary>
		///// Gets or sets the color that the text will be when the regular expression is valid.
		///// </summary>
		///// <exception cref="InvalidOperationException">The property can only be set in the designer, and 
		///// once at run-time.</exception>
		//public Color ValidTextColor
		//{
		//	get { return _validTextColor.Get(); }
		//	set { _validTextColor.Set(value); }
		//}
		//private DesignerOnlyProperty<Color> _validTextColor;

		///// <summary>
		///// Gets or sets the color that the text will be when the regular expression is invalid.
		///// </summary>
		//public Color InvalidTextColor
		//{
		//	get { return _invalidTextColor.Get(); }
		//	set { _invalidTextColor.Set(value); }
		//}
		//private DesignerOnlyProperty<Color> _invalidTextColor;

		//public static readonly DependencyProperty RegexProperty = DependencyProperty.Register(
		//	nameof(Regex), typeof(Regex), typeof(RegexInputWidgetHorizontal));
		////private static readonly DependencyPropertyKey RegexPropertyKey = DependencyProperty.RegisterReadOnly(
		////	nameof(Regex), typeof(Regex), typeof(RegexInputWidgetHorizontal), new PropertyMetadata(null));

		//private static readonly DependencyPropertyKey IsValidPropertyKey = DependencyProperty.RegisterReadOnly(
		//	nameof(IsValid), typeof(bool), typeof(RegexInputWidgetHorizontal), new PropertyMetadata(false));

		//private RegexViewModel _viewModel;
		//private BooleanToColorBrushConverter _textColorConverter;

		//private class BooleanToColorBrushConverter : BooleanToToggleConverter<SolidColorBrush>
		//{
		//	public BooleanToColorBrushConverter(Color @true, Color @false)
		//		: base(new SolidColorBrush(@true), new SolidColorBrush(@false))
		//	{
		//		TrueColor = @true;
		//		FalseColor = @false;
		//	}

		//	public Color TrueColor
		//	{
		//		get { return True.Color; }
		//		set { True = new SolidColorBrush(value); }
		//	}

		//	public Color FalseColor
		//	{
		//		get { return False.Color; }
		//		set { False.Color = value; }
		//	}
		//}
		#endregion

		public RegexInputWidgetHorizontal()
		{
			_textBrushConverter = new BooleanToToggleConverter<Brush>(
				new SolidColorBrush(Colors.Black),
				new SolidColorBrush(Colors.Red));

			InitializeComponent();

			_viewModel = new RegexViewModel();
			DataContext = _viewModel;

			InitializeDesignerOnlyProperties();

			InitializeEventHandlers();

			var binding = new Binding(nameof(_viewModel.IsValid));
			binding.Mode = BindingMode.OneWay;
			binding.Converter = _textBrushConverter;
			binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
			pattern.SetBinding(TextBox.ForegroundProperty, binding);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool IsValid
		{
			get { return (bool)GetValue(_isValidPropertyKey.DependencyProperty); }
		}
		private static DependencyPropertyKey _isValidPropertyKey = DependencyProperty.RegisterReadOnly(
			nameof(IsValid), typeof(bool), typeof(RegexInputWidgetHorizontal), new PropertyMetadata(false));

		[EditorBrowsable(EditorBrowsableState.Never)]
		public Regex Regex
		{
			get { return (Regex)GetValue(_regexPropertyKey.DependencyProperty); }
		}
		private static DependencyPropertyKey _regexPropertyKey = DependencyProperty.RegisterReadOnly(
			nameof(Regex), typeof(Regex), typeof(RegexInputWidgetHorizontal), new PropertyMetadata(null));

		public Brush ValidTextBrush
		{
			get { return _validTextBrush.Get(); }
			set { _validTextBrush.Set(value); }
		}
		private DesignerOnlyProperty<Brush> _validTextBrush;

		public Brush InvalidTextBrush
		{
			get { return _invalidTextBrush.Get(); }
			set { _invalidTextBrush.Set(value); }
		}
		private DesignerOnlyProperty<Brush> _invalidTextBrush;

		private BooleanToToggleConverter<Brush> _textBrushConverter;
		private RegexViewModel _viewModel;

		private void InitializeEventHandlers()
		{
			_viewModel.PropertyChanged += (o, e) =>
			{
				if (e.PropertyName == nameof(_viewModel.IsValid))
				{
					SetValue(_isValidPropertyKey, _viewModel.IsValid);
				}
			};
			_viewModel.PropertyChanged += (o, e) =>
			{
				if (e.PropertyName == nameof(_viewModel.Value))
				{
					SetValue(_regexPropertyKey, _viewModel.Value);
				}
			};
		}

		private void InitializeDesignerOnlyProperties()
		{
			_validTextBrush = new DesignerOnlyProperty<Brush>(
				this,
				() => _textBrushConverter.True,
				x => _textBrushConverter.True = x);
			_invalidTextBrush = new DesignerOnlyProperty<Brush>(
				this,
				() => _textBrushConverter.False,
				x => _textBrushConverter.False = x);
		}
	}
}
