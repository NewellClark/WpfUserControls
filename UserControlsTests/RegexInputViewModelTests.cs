using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Text.RegularExpressions;
using static System.Text.RegularExpressions.RegexOptions;
using NewellClark.Wpf.UserControls.ViewModels;
using System.ComponentModel;

namespace UserControlsTests
{
	[TestFixture]
	public class RegexInputViewModelTests
	{
		[Test]
		public void Options_SetsBooleans()
		{
			RegexOptions options = Multiline | IgnoreCase | Compiled;
			var vm = new RegexInputViewModel();
			vm.Options = options;

			Assert.That(vm.Multiline);
			Assert.That(vm.IgnoreCase);
			Assert.That(vm.Compiled);
		}

		[Test]
		public void Booleans_SetOptions()
		{
			var vm = new RegexInputViewModel();
			vm.RightToLeft = vm.Singleline = vm.ECMAScript = true;
			RegexOptions expected = RightToLeft | Singleline | ECMAScript;

			Assert.That(expected == vm.Options);
		}

		[Test]
		public void Booleans_RaisePropertyChangedEvent()
		{
			RegexInputViewModel vm = new RegexInputViewModel();
			vm.ExplicitCapture = false;

			AssertPropertyChangedEventWorks(
				vm, x => x.ExplicitCapture = true, nameof(vm.ExplicitCapture));
		}

		[Test]
		public void Options_RaisesAllRelaventPropertyChangedEvents()
		{
			var vm = new RegexInputViewModel();
			RegexOptions options = Multiline | Singleline | IgnoreCase;
			var list = new List<object>();
			vm.PropertyChanged += (o, e) =>
			{
				if (e.PropertyName == nameof(Multiline))
					list.Add(new object());
			};
			vm.PropertyChanged += (o, e) =>
			{
				if (e.PropertyName == nameof(Singleline))
					list.Add(new object());
			};
			vm.PropertyChanged += (o, e) =>
			{
				if (e.PropertyName == nameof(IgnoreCase))
					list.Add(new object());
			};

			vm.Options = options;
			Assert.That(list.Count == 3);
		}

		
		private void AssertPropertyChangedEventWorks<T>(
			T target,
			Action<T> mutateProperty,
			string property) where T : INotifyPropertyChanged
		{
			bool success = false;
			target.PropertyChanged += (o, e) =>
			{
				success = true;
				Assert.AreEqual(e.PropertyName, property);
			};
			mutateProperty(target);
			Assert.True(success);
		}

		private class EventRaisedException : Exception
		{

		}
	}
}
