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
		public RegexInputWidgetHorizontal()
		{
			_textColorConverter = new BooleanToColorBrushConverter(
				Colors.Black, Colors.Red);

			InitializeComponent();

			_viewModel = new RegexViewModel();
			DataContext = _viewModel;

			InitializeDesignerOnlyProperties();
			InitializeIsValidBinding();
			InitializeIsValidToTextBoxTextColorBinding();
		}

		private void InitializeDesignerOnlyProperties()
		{
			_validTextColor = new DesignerOnlyProperty<Color>(
				this,
				() => _textColorConverter.TrueColor,
				x => _textColorConverter.TrueColor = x);
			_invalidTextColor = new DesignerOnlyProperty<Color>(
				this,
				() => _textColorConverter.FalseColor,
				x => _textColorConverter.FalseColor = x);
		}

		private void InitializeIsValidBinding()
		{
			_viewModel.PropertyChanged += (o, e) =>
			{
				if (e.PropertyName == nameof(_viewModel.IsValid))
				{
					SetValue(IsValidPropertyKey, _viewModel.IsValid);
				}
			};
		}

		private void InitializeIsValidToTextBoxTextColorBinding()
		{
			var textBinding = new Binding(nameof(_viewModel.IsValid));
			textBinding.Mode = BindingMode.OneWay;
			textBinding.Converter = _textColorConverter;
			textBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
			pattern.SetBinding(TextBox.ForegroundProperty, textBinding);
		}

		/// <summary>
		/// Gets or sets the regular expression that is currently entered in the control. 
		/// Returns null if the currently-entered regex is invalid.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Regex Regex
		{
			get { return (Regex)GetValue(RegexProperty); }
			set { SetValue(RegexProperty, value); }
		}
		
		/// <summary>
		/// Indicates whether the currently-entered pattern and options form a valid regular expression.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool IsValid
		{
			get { return (bool)GetValue(IsValidPropertyKey.DependencyProperty); }
		}

		/// <summary>
		/// Gets or sets the color that the text will be when the regular expression is valid.
		/// </summary>
		/// <exception cref="InvalidOperationException">The property can only be set in the designer, and 
		/// once at run-time.</exception>
		public Color ValidTextColor
		{
			get { return _validTextColor.Get(); }
			set { _validTextColor.Set(value); }
		}
		private DesignerOnlyProperty<Color> _validTextColor;

		/// <summary>
		/// Gets or sets the color that the text will be when the regular expression is invalid.
		/// </summary>
		public Color InvalidTextColor
		{
			get { return _invalidTextColor.Get(); }
			set { _invalidTextColor.Set(value); }
		}
		private DesignerOnlyProperty<Color> _invalidTextColor;

		public static readonly DependencyProperty RegexProperty = DependencyProperty.Register(
			nameof(Regex), typeof(Regex), typeof(RegexInputWidgetHorizontal));

		private static readonly DependencyPropertyKey IsValidPropertyKey = DependencyProperty.RegisterReadOnly(
			nameof(IsValid), typeof(bool), typeof(RegexInputWidgetHorizontal), new PropertyMetadata(false));

		private RegexViewModel _viewModel;
		private BooleanToColorBrushConverter _textColorConverter;

		private class BooleanToColorBrushConverter : BooleanToToggleConverter<SolidColorBrush>
		{
			public BooleanToColorBrushConverter(Color @true, Color @false)
				: base(new SolidColorBrush(@true), new SolidColorBrush(@false))
			{
				TrueColor = @true;
				FalseColor = @false;
			}

			public Color TrueColor
			{
				get { return True.Color; }
				set { True = new SolidColorBrush(value); }
			}

			public Color FalseColor
			{
				get { return False.Color; }
				set { False.Color = value; }
			}
		}
	}
}
