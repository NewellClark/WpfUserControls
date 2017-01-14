using System;
using NUnit.Framework;
using System.ComponentModel;
using NewellClark.Wpf.UserControls;

namespace UserControlsTests
{
	[TestFixture]
	public class CommonTests
	{
		[Test]
		public void SetField_RaisesEventWhenChanged()
		{
			string propertyName = "TestName";
			Assume.That(!_raised);
			_propertyChanged += (o, e) =>
			{
				_raised = true;
				Assert.That(e.PropertyName == propertyName, "Incorrect PropertyName on event args");
			};

			string field = "field";
			_propertyChanged.SetField(ref field, "new value", propertyName);

			Assert.That(_raised);
		}
		private event PropertyChangedEventHandler _propertyChanged;
		private bool _raised = false;
	}
}