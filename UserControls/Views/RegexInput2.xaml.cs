using NewellClark.Wpf.UserControls.TypeConverters;
using NewellClark.Wpf.UserControls.ViewModels;
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

namespace NewellClark.Wpf.UserControls.Views
{
	/// <summary>
	/// Interaction logic for RegexInput2.xaml
	/// </summary>
	public partial class RegexInput2 : UserControl
	{
		public RegexInput2()
		{
			InitializeComponent();

			_viewModel = new RegexViewModel();
			DataContext = _viewModel;
			_textColorConverter = new BooleanToBrushConverter();
		}

		private RegexViewModel _viewModel;
		private BooleanToBrushConverter _textColorConverter;
	}
}
