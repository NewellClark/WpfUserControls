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
	/// Interaction logic for SizeInputSpinner.xaml
	/// </summary>
	public partial class SizeInputSpinner : UserControl
	{
		public SizeInputSpinner() : this(new SizeViewModel()) { }
		public SizeInputSpinner(SizeViewModel viewModel)
		{
			InitializeComponent();
			ViewModel = viewModel;
		}

		public SizeViewModel ViewModel
		{
			get { return (SizeViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
			nameof(ViewModel), typeof(SizeViewModel), typeof(SizeInputSpinner));

		public GridLength SpinnerSeparation
		{
			get { return (GridLength)GetValue(SpinnerSeparationProperty); }
			set { SetValue(SpinnerSeparationProperty, value); }
		}
		public static readonly DependencyProperty SpinnerSeparationProperty = DependencyProperty.Register(
			nameof(SpinnerSeparation), typeof(GridLength), typeof(SizeInputSpinner),
			new PropertyMetadata(new GridLength(12)));
	}
}
