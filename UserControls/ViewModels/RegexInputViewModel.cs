using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NewellClark.Wpf.UserControls.ViewModels
{
	internal class RegexInputViewModel : INotifyPropertyChanged
	{
		public RegexInputViewModel()
		{
			_flagBools = new List<FlagBool>();

			_ignoreCase = CreateFlag(RegexOptions.IgnoreCase);
			_multiline = CreateFlag(RegexOptions.Multiline);
			_explicitCapture = CreateFlag(RegexOptions.ExplicitCapture);
			_compiled = CreateFlag(RegexOptions.Compiled);
			_singleline = CreateFlag(RegexOptions.Singleline);
			_ignorePatternWhiteSpace = CreateFlag(RegexOptions.IgnorePatternWhitespace);
			_rightToLeft = CreateFlag(RegexOptions.RightToLeft);
			_ecmaScript = CreateFlag(RegexOptions.ECMAScript);
			_cultureInvariant = CreateFlag(RegexOptions.CultureInvariant);
		}

		public RegexOptions Options
		{
			get { return _options; }
			set
			{
				UpdateOptions(value);
			}
		}
		private RegexOptions _options;
		
		public bool IgnoreCase
		{
			get { return _ignoreCase.Enabled; }
			set { _ignoreCase.Enabled = value; }
		}
		private FlagBool _ignoreCase;

		public bool Multiline
		{
			get { return _multiline.Enabled; }
			set { _multiline.Enabled = value; }
		}
		private FlagBool _multiline;

		public bool ExplicitCapture
		{
			get { return _explicitCapture.Enabled; }
			set { _explicitCapture.Enabled = value; }
		}
		private FlagBool _explicitCapture;

		public bool Compiled
		{
			get { return _compiled.Enabled; }
			set { _compiled.Enabled = value; }
		}
		private FlagBool _compiled;

		public bool Singleline
		{
			get { return _singleline.Enabled; }
			set { _singleline.Enabled = value; }
		}
		private FlagBool _singleline;

		public bool IgnorePatternWhiteSpace
		{
			get { return _ignorePatternWhiteSpace.Enabled; }
			set { _ignorePatternWhiteSpace.Enabled = value; }
		}
		private FlagBool _ignorePatternWhiteSpace;

		public bool RightToLeft
		{
			get { return _rightToLeft.Enabled; }
			set { _rightToLeft.Enabled = value; }
		}
		private FlagBool _rightToLeft;

		public bool ECMAScript
		{
			get { return _ecmaScript.Enabled; }
			set { _ecmaScript.Enabled = value; }
		}
		private FlagBool _ecmaScript;

		public bool CultureInvariant
		{
			get { return _cultureInvariant.Enabled; }
			set { _cultureInvariant.Enabled = value; }
		}
		private FlagBool _cultureInvariant;

		public event PropertyChangedEventHandler PropertyChanged;


		private void UpdateOptions(RegexOptions options)
		{
			if (_options == options)
				return;

			var dirtyFlags = new List<FlagBool>();
			foreach (var flag in _flagBools)
			{
				var current = flag.ValueWhenEnabled & options;
				var previous = flag.ValueWhenEnabled & _options;
				if (current != previous)
					dirtyFlags.Add(flag);
			}

			_options = options;

			foreach (var flag in dirtyFlags)
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(flag.Name));
		}

		private bool SetField<T>(ref T field, T value, [CallerMemberName]string name = "")
		{
			return Common.SetField(PropertyChanged, ref field, value, name);
		}

		private FlagBool CreateFlag(RegexOptions valueWhenEnabled)
		{
			FlagBool result = new FlagBool(this, valueWhenEnabled);
			_flagBools.Add(result);

			return result;
		}

		/// <summary>
		/// Sets its <c>CurrentValue</c> property to <c>ValueWhenEnabled</c> when <c>Enabled</c> is set to true, and vice-versa.
		/// </summary>
		private class FlagBool
		{
			public FlagBool(RegexInputViewModel outer, RegexOptions valueWhenEnabled)
			{
				_outer = outer;
				ValueWhenEnabled = valueWhenEnabled;
				Name = Enum.GetName(typeof(RegexOptions), valueWhenEnabled);
			}

			public RegexOptions ValueWhenEnabled { get; }

			public RegexOptions CurrentValue
			{
				get { return _outer.Options & ValueWhenEnabled; }
			}

			public bool Enabled
			{
				get { return (_outer.Options & CurrentValue) == ValueWhenEnabled; }
				set
				{
					if (value)
					{
						_outer.Options |= ValueWhenEnabled;
						return;
					}
					_outer.Options &= ~ValueWhenEnabled;
				}
			}

			public string Name { get; }

			private RegexInputViewModel _outer;
		}

		private List<FlagBool> _flagBools;
	}
}
