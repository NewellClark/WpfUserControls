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
using System.Windows.Shapes;

namespace RegexDemoApp
{
	/// <summary>
	/// Interaction logic for QuickTest.xaml
	/// </summary>
	public partial class QuickTest : Window
	{
		public QuickTest()
		{
			InitializeComponent();
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			var vm = new NewellClark.Wpf.UserControls.PointViewModel();
			vm.X = DateTime.Now.Millisecond;
			vm.Y = DateTime.Now.Millisecond * 55.6;
			pointSpinner.ViewModel = vm;
		}
	}
}
