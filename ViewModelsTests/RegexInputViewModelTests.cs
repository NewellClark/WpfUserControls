using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Text.RegularExpressions;
using static System.Text.RegularExpressions.RegexOptions;
using NewellClark.ViewModels;
using System.ComponentModel;
using static UserControlsTests.Common;

namespace UserControlsTests
{
	[TestFixture]
	public class RegexViewModelTests
	{
		[Test]
		public void Options_SetsBooleans()
		{
			RegexOptions options = Multiline | IgnoreCase | Compiled;
			var vm = new RegexViewModel();
			vm.Options = options;

			Assert.That(vm.Multiline);
			Assert.That(vm.IgnoreCase);
			Assert.That(vm.Compiled);
		}

		[Test]
		public void Booleans_SetOptions()
		{
			var vm = new RegexViewModel();
			vm.RightToLeft = vm.SingleLine = vm.EcmaScript = true;
			RegexOptions expected = RightToLeft | Singleline | ECMAScript;

			Assert.That(expected == vm.Options);
		}

		[Test]
		public void Booleans_RaisePropertyChangedEvent()
		{
			RegexViewModel vm = new RegexViewModel();
			vm.ExplicitCapture = false;

			AssertPropertyChangedEventWorks(
				vm, x => x.ExplicitCapture = true, nameof(vm.ExplicitCapture));
		}

		[Test]
		public void IsValid_FalseWhenPatternIsNotValidRegex()
		{
			string _badPattern = @"hell[0o";
			var vm = new RegexViewModel();

			vm.Pattern = _badPattern;

			Assert.False(vm.IsValid);
		}

		[Test]
		public void IsValid_TrueWhenPatternIsValidRegex()
		{
			string _goodPattern = "hell[0o]";
			var vm = new RegexViewModel();

			vm.Pattern = _goodPattern;

			Assert.True(vm.IsValid);
		}

		[Test]
		public void Value_UpdatesWithPatternChange()
		{
			string pattern = @"<[a-z]>";
			var vm = new RegexViewModel();

			vm.Pattern = pattern;

			Assert.That(vm.Regex.ToString() == pattern);
		}

		[Test]
		public void Pattern_UpdatesWithValueChange()
		{
			string pattern = "<[a-z0-9]>";
			var vm = new RegexViewModel();

			vm.Regex = new Regex(pattern);

			Assert.AreEqual(pattern, vm.Pattern);
		}

		[Test]
		public void Options_UpdatesWithValueChange()
		{
			RegexOptions options = IgnoreCase | CultureInvariant;
			Regex regex = new Regex("gr[ae]y", options);
			var vm = new RegexViewModel();

			vm.Regex = regex;

			Assert.AreEqual(options, vm.Options);
		}

		[Test]
		public void Value_NullWhenPatternInvalid()
		{
			string pattern = "gr(a|e)yback[";
			var vm = new RegexViewModel();

			vm.Pattern = pattern;

			Assert.AreEqual(vm.Regex, null);
		}


		[Test]
		public void Pattern_FiresAllEvents()
		{
			string pattern = @"gr(a|e)y";
			var vm = new RegexViewModel();

			AssertPropertyChangedEventFires(
				vm,
				x => x.Pattern = pattern,
				nameof(vm.Pattern), nameof(vm.Regex), nameof(vm.IsValid));
		}

		[Test]
		public void Options_FiresAllEvents()
		{
			var options =
				RegexOptions.Multiline |
				RegexOptions.IgnoreCase |
				RegexOptions.RightToLeft |
				RegexOptions.Singleline |
				RegexOptions.ExplicitCapture;
			var vm = new RegexViewModel();
			vm.Pattern = "hello world [0-9]+";
			var changingProperties = new string[]
			{
				nameof(vm.Regex),
				nameof(vm.Options),
				nameof(vm.Multiline),
				nameof(vm.IgnoreCase),
				nameof(vm.RightToLeft),
				nameof(vm.SingleLine),
				nameof(vm.ExplicitCapture)
			};

			AssertPropertyChangedEventFires(vm, x => x.Options = options,
				changingProperties);
		}

		[Test]
		public void Options_DoesNotFireValueChangedWhenValueWasNull()
		{
			RegexOptions options =
				RegexOptions.Compiled |
				RegexOptions.Multiline |
				RegexOptions.IgnorePatternWhitespace;
			var vm = new RegexViewModel();
			string[] dependantProperties = new string[]
			{
				nameof (vm.Compiled),
				nameof(vm.Multiline),
				nameof(vm.IgnorePatternWhiteSpace),
				nameof(vm.Options),
				nameof(vm.Error)
			};

			AssertPropertyChangedEventFires(
				vm, x => x.Options = options, true, dependantProperties);
		}

		[Test]
		public void Value_FiresAllEvents_WhenSetToNonNullFromNonNull()
		{
			var vm = new RegexViewModel();
			vm.Regex = new Regex($"[a4A][lIi]", RegexOptions.Multiline);
			string pattern = "(h.+)*[aeiou]";
			RegexOptions options =
				RegexOptions.RightToLeft |
				RegexOptions.IgnoreCase |
				RegexOptions.ExplicitCapture;

			string[] changingProperties = new string[]
			{
				nameof(vm.Regex),
				nameof(vm.Pattern),
				nameof(vm.Options),

				nameof(vm.Multiline),	//	Flag gets unset.

				nameof(vm.RightToLeft),
				nameof(vm.IgnoreCase),
				nameof(vm.ExplicitCapture)
			};

			AssertPropertyChangedEventFires(
				vm, x => x.Regex = new Regex(pattern, options), 
				changingProperties);
		}

		[Test]
		public void Value_FiresAllEvents_WhenSetToNonNullFromNull()
		{
			var vm = new RegexViewModel();
			Regex regex = new Regex(
				@"<[a-z0-9]+>",
				IgnoreCase | IgnorePatternWhitespace | Multiline);
			var changingProperties = new string[]
			{
				nameof(vm.Regex),
				nameof(vm.Pattern),
				nameof(vm.Options),
				nameof(vm.IgnoreCase),
				nameof(vm.IgnorePatternWhiteSpace),
				nameof(vm.Multiline),
				nameof(vm.IsValid)
			};

			AssertPropertyChangedEventFires(
				vm, x => x.Regex = regex, true, changingProperties);
		}

		[Test]
		public void Options_RemembersOptionsWhenValueSetToNullExplicitly()
		{
			string pattern = @"hell[0o]+\s+world!";
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.RightToLeft;

			var vm = new RegexViewModel();
			vm.Regex = new Regex(pattern, options);

			AssertPropertyChangedEventFires(
				vm, x => x.Regex = null, true, nameof(vm.Regex));
			Assert.That(vm.Options == options, $"Must remember {nameof(vm.Options)} when " +
				$"{nameof(vm.Regex)} set to null");
		}

		[Test]
		public void Pattern_RemembersPatternWhenValueSetToNullExplicitly()
		{
			string pattern = @"[0-9]{5}";
			var vm = new RegexViewModel();
			vm.Regex = new Regex(pattern);

			AssertPropertyChangedEventFires(
				vm, x => x.Regex = null, true, nameof(vm.Regex));
			Assert.That(vm.Pattern == pattern,
				$"Did not remember {nameof(vm.Pattern)} when {nameof(vm.Regex)} was set to null.");
		}

		[Test]
		public void Error_NullPattern()
		{
			var vm = new RegexViewModel();
			vm.Pattern = @"gr(a|e)y";
			vm.Pattern = null;

			Assume.That(vm.Regex == null);
			
			Assert.That(vm.Error == RegexError.NullPattern);
		}

		[Test]
		public void Error_InvalidPattern()
		{
			var vm = new RegexViewModel();
			vm.Pattern = @"valid";
			vm.Pattern = @"gr[aey";

			Assume.That(vm.Regex == null);

			Assert.That(vm.Error == RegexError.InvalidPattern);
		}

		[Test]
		public void Error_InvalidOptions()
		{
			var vm = new RegexViewModel();
			vm.Pattern = @"ch(aeiou)";
			vm.Options = ECMAScript | RightToLeft;

			Assume.That(vm.Regex == null);

			Assert.That(vm.Error == RegexError.InvalidOptions);
		}

		[Test]
		public void Error_None()
		{
			var vm = new RegexViewModel();
			vm.Pattern = @"col(o|ou}r";

			Assume.That(vm.Regex == null);

			vm.Pattern = @"col(o|ou)r";

			Assert.That(vm.Error == RegexError.None);
		}

		private void AssertPropertyChangedEventWorks<T>(
			T target,
			Action<T> mutateProperty,
			string property) where T : INotifyPropertyChanged
		{
			bool success = false;
			target.PropertyChanged += (o, e) =>
			{
				if (e.PropertyName == property)
					success = true;
			};
			mutateProperty(target);
			Assert.True(success);
		}
	}
}
