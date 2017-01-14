﻿using NewellClark.Wpf.UserControls.TypeConverters;
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
	/// A UI widget that allows users to enter a .NET-flavor Regular Expression.
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

			var textBinding = new Binding(nameof(_viewModel.IsValid));
			textBinding.Mode = BindingMode.OneWay;
			textBinding.Converter = _textColorConverter;
			textBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
			pattern.SetBinding(TextBox.ForegroundProperty, textBinding);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public Regex Value
		{
			get { return _viewModel?.Value; }
			set {
				_viewModel.Value = value;
			}
		}
		
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool IsValid
		{
			get { return _viewModel?.IsValid == true; }
		}

		public Color TextWhenValid
		{
			get { return _textColorConverter.TrueColor; }
			set { _textColorConverter.TrueColor = value; }
		}

		public Color TextWhenInvalid
		{
			get { return _textColorConverter.FalseColor; }
			set { _textColorConverter.FalseColor = value; }
		}

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