﻿using NewellClark.Wpf.UserControls.TypeConverters;
using NewellClark.ViewModels;
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
	/// A UI component that allows the user to enter a regular expression and configure all possible regex options.
	/// It fits on a single line.
	/// Provides visual feedback when the user enters an invalid regular expression.
	/// </summary>
	public partial class SingleLineRegexInput : UserControl
	{
		public SingleLineRegexInput()
		{
			_textBrushConverter = new BooleanToToggleConverter<Brush>(
				new SolidColorBrush(Colors.Black),
				new SolidColorBrush(Colors.Red));

			_viewModel = new RegexViewModel();
			DataContext = _viewModel;

			InitializeComponent();

			InitializeDesignerOnlyProperties();
			InitializeEventHandlers();
			InitializeTextBoxForegroundBrushBinding();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public Regex Regex
		{
			get { return (Regex)GetValue(RegexProperty); }
			set { SetValue(RegexProperty, value); }
		}
		public static readonly DependencyProperty RegexProperty = DependencyProperty.Register(
			nameof(Regex), typeof(Regex), typeof(SingleLineRegexInput),
			new PropertyMetadata(OnRegexPropertyChanged));
		private static void OnRegexPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			((SingleLineRegexInput)sender)._viewModel.Regex = (Regex)e.NewValue;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool IsValid
		{
			get { return (bool)GetValue(_isValidPropertyKey.DependencyProperty); }
		}
		private static readonly DependencyPropertyKey _isValidPropertyKey = DependencyProperty.RegisterReadOnly(
			nameof(IsValid), typeof(bool), typeof(SingleLineRegexInput), new PropertyMetadata(false));

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

		private void InitializeTextBoxForegroundBrushBinding()
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

		private RegexViewModel _viewModel;
		private BooleanToToggleConverter<Brush> _textBrushConverter;
	}
}
