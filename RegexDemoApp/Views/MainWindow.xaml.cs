using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using static RegexDemoApp.Properties.Settings;

namespace RegexDemoApp.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			_viewModel = new DemoViewModel();
			DataContext = _viewModel;

			_viewModel.InputText = Default.InputText;
			if (!string.IsNullOrEmpty(Default.RegexPattern))
				_viewModel.Regex = new Regex(Default.RegexPattern);
			_viewModel.ReplacementPattern = Default.ReplacementPattern;
		}

		private DemoViewModel _viewModel;
	}
}
