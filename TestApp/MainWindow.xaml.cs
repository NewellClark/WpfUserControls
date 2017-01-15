using NewellClark.Wpf.UserControls.Views;
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

namespace TestApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			
		}

		private void expander_Expanded(object sender, RoutedEventArgs e)
		{
			
		}

		private void SingleLineRegexInputControl_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private void invalidColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
		{
		}

		private void validGreen_Click(object sender, RoutedEventArgs e)
		{
			colorBindingRealTimeTest.ValidTextColor = Colors.Green;
		}
	}
}
