using NewellClark.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RegexDemoApp
{
	class DemoViewModel : INotifyPropertyChanged
	{
		public string InputText
		{
			get { return _inputText; }
			set
			{
				SetField(ref _inputText, value);
				UpdateReplacedText();
			}
		}
		private string _inputText;

		public Regex Regex
		{
			get { return _regex; }
			set
			{
				SetField(ref _regex, value);
				UpdateReplacedText();
			}
		}
		private Regex _regex;

		public string ReplacementPattern
		{
			get { return _replacementPattern; }
			set
			{
				SetField(ref _replacementPattern, value);
				UpdateReplacedText();
			}
		}
		private string _replacementPattern;

		public string ReplacedText
		{
			get { return _replacedText; }
			private set { SetField(ref _replacedText, value); }
		}
		private string _replacedText;


		public event PropertyChangedEventHandler PropertyChanged;

		protected bool SetField<T>(ref T field, T value, [CallerMemberName]string propertyName = "")
		{
			if (EqualityComparer<T>.Default.Equals(field, value))
				return false;
			field = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			return true;
		}

		private async Task<string> PerformTextReplacement(string input, Regex regex, string replacementPattern)
		{
			string result = await Task.Run(() => regex?.Replace(input, replacementPattern)).ConfigureAwait(false);
			return result;
		}
		private Task UpdateReplacedText()
		{
			if (ReplacementPattern == null)
				ReplacementPattern = string.Empty;
			if (InputText == null)
				InputText = string.Empty;

			//	To avoid the warning about async methods returning immediately, we wrap our "await" code 
			//		in here and return the task produced by it.
			Func<Task> wrapperToAvoidTheWarning = async () =>
			{
				//	Cancel previous replacement operation to prevent race.
				_cancelSource?.Cancel();
				_cancelSource = new CancellationTokenSource();
				string temp = await PerformTextReplacement(InputText, Regex, ReplacementPattern).ConfigureAwait(true);

				//	If a replacement operation has started more recently, cancel this one to prevent race.
				if (!_cancelSource.Token.IsCancellationRequested)
					ReplacedText = temp;
			};

			return wrapperToAvoidTheWarning();
		}

		private CancellationTokenSource _cancelSource;
	}
}
