﻿using NewellClark.Wpf.UserControls.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace NewellClark.Wpf.UserControls.Views.Components
{
	/// <summary>
	/// Interaction logic for RegexOptionsComboBox.xaml
	/// </summary>
	internal partial class RegexOptionsComboBox : UserControl
	{
		public RegexOptionsComboBox()
		{
			InitializeComponent();
		}

		private void optionsSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (optionsSelector.Items.Count != 0)
			{
				optionsSelector.SelectedIndex = 0;
				e.Handled = true;
			}
		}

		private void optionsSelector_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			//if (optionsSelector.DataContext == null)
			//	return;
			if (optionsSelector.DataContext is RegexViewModel == false)
				throw new InvalidOperationException(
					$@"{nameof(RegexOptionsComboBox)} elements require a data-context of type {nameof(RegexViewModel)}");
		}
	}
}
