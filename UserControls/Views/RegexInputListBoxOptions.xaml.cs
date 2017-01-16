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
	public partial class RegexInputListBoxOptions : UserControl
	{
		public RegexInputListBoxOptions()
		{
			_textBrushConverter = new BooleanToToggleConverter<Brush>(
				new SolidColorBrush(Colors.Black),
				new SolidColorBrush(Colors.Red));

			InitializeComponent();

			_viewModel = new RegexViewModel();
			DataContext = _viewModel;

			InitializeDesignerOnlyProperties();

			InitializeEventHandlers();

			InitializeIsValidBinding();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool IsValid
		{
			get { return (bool)GetValue(_isValidPropertyKey.DependencyProperty); }
			private set { SetValue(_isValidPropertyKey, value); }
		}
		private static DependencyPropertyKey _isValidPropertyKey = DependencyProperty.RegisterReadOnly(
			nameof(IsValid), typeof(bool), typeof(RegexInputListBoxOptions), new PropertyMetadata(false));

		[EditorBrowsable(EditorBrowsableState.Never)]
		public Regex Regex
		{
			get { return (Regex)GetValue(RegexProperty); }
			set { SetValue(RegexProperty, value); }
		}
		public static readonly DependencyProperty RegexProperty = DependencyProperty.Register(
			nameof(Regex), typeof(Regex), typeof(RegexInputListBoxOptions),
			new PropertyMetadata(OnRegexPropertyChanged));
		private static void OnRegexPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var target = (RegexInputListBoxOptions)sender;
			target._viewModel.Regex = (Regex)e.NewValue;
		}

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

		private void InitializeIsValidBinding()
		{
			var binding = new Binding(nameof(_viewModel.IsValid));
			binding.Mode = BindingMode.OneWay;
			binding.Converter = _textBrushConverter;
			binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
			pattern.SetBinding(TextBox.ForegroundProperty, binding);
		}

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
				if (e.PropertyName == nameof(_viewModel.Regex))
				{
					Regex = _viewModel.Regex;
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
