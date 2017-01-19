using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewellClark.Wpf.UserControls;
using System.Windows.Media;
using NewellClark.Wpf.UserControls.TypeConverters;

namespace UserControlsTests
{
	[TestFixture]
	public class BooleanToToggleConverterTests
	{
		[Test]
		public void Convert_ConvertsTrueToTrueProperty()
		{
			Color @true = Colors.Green;
			Color @false = Colors.Red;
			var converter = new BooleanToToggleConverter<Color>(@true, @false);

			Color result = converter.Convert(true, null, null);

			Assert.That(@true == result);
		}

		[Test]
		public void Convert_ConvertsFalseToFalseProperty()
		{
			Color @true = Colors.Green;
			Color @false = Colors.Red;
			var converter = new BooleanToToggleConverter<Color>(@true, @false);

			Color result = converter.Convert(false, null, null);

			Assert.That(@false == result);
		}

		[Test]
		public void ConvertBack_ConvertsTrueColorToTrue()
		{
			Color @true = Colors.Green;
			Color @false = Colors.Red;
			var converter = new BooleanToToggleConverter<Color>(@true, @false);

			bool result = converter.ConvertBack(@true, null, null);

			Assert.That(result == true);
		}

		[Test]
		public void ConvertBack_ConvertsFalseColorToFalse()
		{
			Color @true = Colors.Green;
			Color @false = Colors.Red;
			var converter = new BooleanToToggleConverter<Color>(@true, @false);

			bool result = converter.ConvertBack(@false, null, null);

			Assert.That(result == false);
		}

		[Test]
		public void ConvertBack_ThrowsOnInvalidColor()
		{
			Color @true = Colors.Green;
			Color @false = Colors.Red;
			var converter = new BooleanToToggleConverter<Color>(@true, @false);

			TestDelegate illegal = () => converter.ConvertBack(Colors.Yellow, null, null);

			Assert.Throws<ArgumentException>(illegal);
		}
	}
}
