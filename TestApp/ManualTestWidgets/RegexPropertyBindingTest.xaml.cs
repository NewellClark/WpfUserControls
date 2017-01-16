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

namespace TestApp.ManualTestWidgets
{
	/// <summary>
	/// Checks to make sure the <c>Regex</c> property on <c>HorizontalRegexInputWidget</c> properly supports data-binding.
	/// The two regex input controls have a two-way binding between their Regex properties. 
	/// Note that if one control (say, the left one) is set so that it's in an invalid state, the right control will 
	/// not update. This is because the <c>RegexViewModel</c> remembers its primary input (<c>Pattern</c> and 
	/// <c>Options</c>) when its <c>Regex</c> property is assigned an invalid value (<c>null</c>). 
	/// 
	/// TLDR: when you update one control, the other will mirror it. If the control that you interracted with is put into
	/// an invalid state, the other one will NOT mirror it. If you put it back into a valid state from an invalid state,
	/// the other control WILL mirror it. 
	/// </summary>
	public partial class RegexPropertyBindingTest : UserControl
	{
		public RegexPropertyBindingTest()
		{
			InitializeComponent();
		}
	}
}
