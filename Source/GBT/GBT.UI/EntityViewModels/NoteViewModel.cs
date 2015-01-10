using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GBT.Domain.Entities;
using GBT.UI.Main;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GBT.UI.Helpers;

namespace GBT.UI.EntityViewModels
{
	public class NoteViewModel : ViewModelBase
	{
		[Inject]
		public MainViewModel MainViewModel { get; set; }

		public Note Note { get; private set; }
		public PatternRowViewModel Row { get; private set; }
		public int ChannelNum { get; private set; }

		private bool _Editing;
		public bool Editing
		{
			get { return this._Editing; }
			set
			{
				if (this._Editing != value)
				{
					this._Editing = value;
					RaisePropertyChanged(() => this.Editing);
				}
			}
		}

		private bool _RowSelected;
		public bool RowSelected
		{
			get { return this._RowSelected; }
			set
			{
				if (this._RowSelected != value)
				{
					this._RowSelected = value;
					RaisePropertyChanged(() => this.RowSelected);
				}
			}
		}

		public ICommand ClickCommand { get { return new RelayCommand<MouseButtonEventArgs>(OnClick); } }
		private void OnClick(MouseButtonEventArgs args)
		{
			this.MainViewModel.Select(this);
			//args.Handled = true;
		}

		private string _DisplayText;
		public string DisplayText
		{
			get { return this._DisplayText; }
			set { this._DisplayText = value; RaisePropertyChanged(() => this.DisplayText); }
		}

		/*
		public ICommand KeyDownCommand { get { return new RelayCommand<KeyEventArgs>(OnKeyDown); } }
		private void OnKeyDown(KeyEventArgs args)
		{
			args.Handled = true;
			char ch = KeyHelper.GetCharFromKey(args.Key);

			// cursor control
			if (args.Key == Key.Up)
				this.MainViewModel.Up();
			else if (args.Key == Key.Down)
				this.MainViewModel.Down();
			else if (args.Key == Key.Left)
				this.MainViewModel.Left();
			else if (args.Key == Key.Right)
				this.MainViewModel.Right();

			// clear
			else if (args.Key == Key.Delete)
			{
				this.Note.Key = Tone.None;
				UpdateDisplayText();
			}

			// clear and advance
			else if (args.Key == Key.Space)
			{
				this.Note.Key = Tone.None;
				UpdateDisplayText();
				this.MainViewModel.Down();
			}

			// keys and octaves
			else if ((ch >= '1') && (ch <= '9'))
			{
				if (this.Note.Key == Tone.None)
					this.Note.Key = Tone.C;
				this.Note.Octave = Int32.Parse(ch.ToString());
				UpdateDisplayText();
				this.MainViewModel.CurrentOctave = this.Note.Octave;
				this.MainViewModel.Down();
			}

			// change tone
			else if (((ch >= 'a') && (ch <= 'g')) || ((ch >= 'A') && (ch <= 'G')))
			{
				if (this.Note.Key == Tone.None)
					this.Note.Octave = this.MainViewModel.CurrentOctave;
				this.Note.Key = (Tone)Enum.Parse(typeof(Tone), args.Key.ToString().ToUpper());
				UpdateDisplayText();
				this.MainViewModel.Down();
			}

			else
				args.Handled = false;
		}
		*/

		public NoteViewModel(Note note, PatternRowViewModel row, int channelNum)
		{
			this.Note = note;
			this.Row = row;
			this.ChannelNum = channelNum;
			UpdateDisplayText();
		}

		public void Edit()
		{
			this.MainViewModel.Select(this);
		}

		public void OnKeyDown(KeyEventArgs args)
		{
			char ch = KeyHelper.GetCharFromKey(args.Key);
			if (args.Key == Key.Left)
			{
				args.Handled = true;
				this.MainViewModel.Left();
				args.Handled = true;
			}
			else if (args.Key == Key.Right)
			{
				args.Handled = true;
				this.MainViewModel.Right();
			}

			// clear
			else if (args.Key == Key.Delete)
			{
				args.Handled = true;
				this.Note.Key = Tone.None;
				UpdateDisplayText();
			}

			// clear and advance
			else if (args.Key == Key.Space)
			{
				args.Handled = true;
				this.Note.Key = Tone.None;
				UpdateDisplayText();
				this.MainViewModel.Down();
			}

			// change octave
			else if ((ch >= '1') && (ch <= '9'))
			{
				args.Handled = true;
				if (this.Note.Key == Tone.None)
					this.Note.Key = Tone.C;
				this.Note.Octave = Int32.Parse(ch.ToString());
				UpdateDisplayText();
				this.MainViewModel.CurrentOctave = this.Note.Octave;
				this.MainViewModel.Down();
			}

			// change tone
			else if (((ch >= 'a') && (ch <= 'g')) || ((ch >= 'A') && (ch <= 'G')))
			{
				args.Handled = true;
				if (this.Note.Key == Tone.None)
					this.Note.Octave = this.MainViewModel.CurrentOctave;
				this.Note.Key = (Tone)Enum.Parse(typeof(Tone), args.Key.ToString().ToUpper());
				UpdateDisplayText();
				this.MainViewModel.Down();
			}

		}

		private void UpdateDisplayText()
		{
			if (this.Note.Key == Tone.None)
				this.DisplayText = " -  .. ...";
			else
				this.DisplayText = this.Note.Key.ToString() + "-" + this.Note.Octave.ToString() + " .. ...";
		}

	}
}
