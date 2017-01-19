using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace NewellClark.ViewModels
{
	public sealed class RegexViewModel : INotifyPropertyChanged
	{
		public RegexViewModel()
		{
			_flagBools = new List<FlagBool>();
			_dirtyProperties = new HashSet<string>();

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

		public Regex Regex
		{
			get { return _regex; }
			set { UpdateRegex(value); }
		}
		private Regex _regex;

		public string Pattern
		{
			get { return _pattern; }
			set { SetField(ref _pattern, value); }
		}
		private string _pattern;

		public bool IsValid
		{
			get { return _isValid; }
			private set { SetField(ref _isValid, value); }
		}
		private bool _isValid;

		public RegexError Error
		{
			get { return _error; }
			set { SetField(ref _error, value); }
		}
		private RegexError _error;

		public RegexOptions Options
		{
			get { return _options; }
			set
			{
				foreach (FlagBool flag in _flagBools)
				{
					RegexOptions next = flag.ValueWhenEnabled & value;
					RegexOptions current = flag.ValueWhenEnabled & _options;
					if (next != current)
						SetDirty(flag.Name);
				}

				SetField(ref _options, value);
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

		public bool IgnorePatternWhitespace
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

		private void UpdateProperties()
		{
			_isUpdating = true;

			SetRegexValueSwallowExceptions(_pattern, _options);
			IsValid = Regex != null;
			RaiseEventsOnDirtyProperties();

			_isUpdating = false;
		}

		/// <summary>
		/// Same as <c>UpdateProperties</c>, but called when the <c>Regex</c> property was set.
		/// </summary>
		/// <param name="regex"></param>
		private void UpdateRegex(Regex regex)
		{
			_isUpdating = true;

			if (!SetField(ref _regex, regex, nameof(Regex)))
			{
				_isUpdating = true;
				return;
			}

			if (_regex != null)
			{
				Options = _regex.Options;
				Pattern = _regex.ToString();
				IsValid = _regex != null;
			}

			RaiseEventsOnDirtyProperties();

			_isUpdating = false;
		}

		private bool SetField<T>(ref T field, T value, [CallerMemberName]string name = "")
		{
			//return Common.SetField(PropertyChanged, ref field, value, name);
			if (EqualityComparer<T>.Default.Equals(field, value))
				return false;
			field = value;
			SetDirty(name);
			if (!_isUpdating)
				UpdateProperties();
			return true;
		}

		private void SetDirty(string propertyName)
		{
			_dirtyProperties.Add(propertyName);
		}

		private void RaiseEventsOnDirtyProperties()
		{
			foreach (string dirtyName in _dirtyProperties)
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(dirtyName));

			_dirtyProperties.Clear();
		}

		private void SetRegexValueSwallowExceptions(string pattern, RegexOptions options)
		{
			Regex result;
			RegexError error = RegexError.None;
			try
			{
				result = new Regex(pattern, options);
			}
			catch (ArgumentNullException)
			{
				result = null;
				error = RegexError.NullPattern;
			}
			catch (ArgumentOutOfRangeException)
			{
				result = null;
				error = RegexError.InvalidOptions;
			}
			catch (ArgumentException)
			{
				result = null;
				error = RegexError.InvalidPattern;
			}
			Error = error;
			Regex = result;
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
			public FlagBool(RegexViewModel outer, RegexOptions valueWhenEnabled)
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

			private RegexViewModel _outer;
		}

		private List<FlagBool> _flagBools;
		private HashSet<string> _dirtyProperties;
		private bool _isUpdating = false;
	}

	internal enum RegexError
	{
		None = 0,
		NullPattern = 1,
		InvalidPattern = 2,
		InvalidOptions = 3
	}
}
