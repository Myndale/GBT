using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs.ViewModel;
using MvvmDialogs.ViewModels;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GBT.Domain.IoC;
using GBT.UI.About;
using System.Windows;
using GBT.UI.Localization;
using GBT.Domain.Entities;
using GBT.UI.EntityViewModels;
using Ninject.Parameters;

namespace GBT.UI.Main
{
	public class MainViewModel : ViewModelBase, IInitializable, IDisposable
	{
		[Inject]
		public IDialogViewModelCollection Dialogs { get; set; }

		private string _Status = "Ready";
		public string Status
		{
			get { return this._Status = "Ready"; }
			set { this._Status = value; RaisePropertyChanged(() => this.Status); }
		}
		
		private SongViewModel _CurrentSong;
		public SongViewModel CurrentSong
		{
			get { return _CurrentSong; }
			set { _CurrentSong = value; RaisePropertyChanged(() => this.CurrentSong); }
		}

		private PatternViewModel _CurrentPattern;
		public PatternViewModel CurrentPattern
		{
			get { return _CurrentPattern; }
			set
			{
				if (this._CurrentPattern != value)
				{
					this._CurrentPattern = value;
					RaisePropertyChanged(() => this.CurrentPattern);
				}
			}
		}

		private NoteViewModel _CurrentNote;
		public NoteViewModel CurrentNote
		{
			get { return _CurrentNote; }
			set
			{
				if (_CurrentNote != value)
				{
					this._CurrentNote = value;
					RaisePropertyChanged(() => this.CurrentNote);
					this.CurrentPattern.SelectedItem = (value==null) ? null : value.Row;
				}
			}
		}

		private int _CurrentOctave = 4;
		public int CurrentOctave
		{
			get { return _CurrentOctave; }
			set { _CurrentOctave = value; RaisePropertyChanged(() => this.CurrentOctave); }
		}

		public void Initialize()
		{
			LoadUserSettings();
			OnNew();
		}

		public void Dispose()
		{
			SaveUserSettings();
		}

		public ICommand LoadedCommand { get { return new RelayCommand(OnLoaded); } }
		private void OnLoaded()
		{
		}

		public ICommand ClosingCommand { get { return new RelayCommand<CancelEventArgs>(OnClosing); } }
		private void OnClosing(CancelEventArgs args)
		{
#if !DEBUG
			var dlg = new MessageBoxViewModel
			{
				Caption = "Exit",
				Message = "Save changes and quit?",
				Buttons = System.Windows.MessageBoxButton.YesNo,
				Image = System.Windows.MessageBoxImage.Question
			};
			args.Cancel = (dlg.Show(this.Dialogs) == System.Windows.MessageBoxResult.No);
#endif
		}

		public ICommand ClosedCommand { get { return new RelayCommand(OnClosed); } }
		private void OnClosed()
		{
			this.Dispose();
		}

		public ICommand SelectLanguageCommand { get { return new RelayCommand<Language>(OnSelectLanguage); } }
		private void OnSelectLanguage(Language language)
		{
			var sp = Application.Current.Resources["Localizations"] as LocalizationManager;
			sp.CurrentLanguage = language;
		}

		public ICommand NewCommand { get { return new RelayCommand(OnNew); } }
		private void OnNew()
		{
			this.CurrentSong = Injector.Get<SongViewModel>(
				new ConstructorArgument("song", Injector.Get<Song>())
			);
			this.CurrentPattern = this.CurrentSong.Patterns.FirstOrDefault();
		}

		public ICommand OpenCommand { get { return new RelayCommand(OnOpen); } }
		private void OnOpen()
		{
		}

		public ICommand CloseCommand { get { return new RelayCommand(OnClose); } }
		private void OnClose()
		{
		}

		public ICommand SaveCommand { get { return new RelayCommand(OnSave); } }
		private void OnSave()
		{
		}

		public ICommand SaveAsCommand { get { return new RelayCommand(OnSaveAs); } }
		private void OnSaveAs()
		{
		}

		public ICommand CutCommand { get { return new RelayCommand(OnCut); } }
		private void OnCut()
		{
		}

		public ICommand CopyCommand { get { return new RelayCommand(OnCopy); } }
		private void OnCopy()
		{
		}

		public ICommand PasteCommand { get { return new RelayCommand(OnPaste); } }
		private void OnPaste()
		{
		}

		public ICommand AboutCommand { get { return new RelayCommand(OnAbout); } }
		private void OnAbout()
		{
			new AboutDialogViewModel().Show(this.Dialogs);
		}

		private void LoadUserSettings()
		{
		}

		private void SaveUserSettings()
		{
		}

		public void Select(NoteViewModel note)
		{
			if (this.CurrentNote != null)
			{
				//this.CurrentNote.Row.Selected = false;
				this.CurrentNote.Editing = false;
			}
			this.CurrentNote = note;
			if (this.CurrentNote != null)
			{
				//this.CurrentNote.Row.Selected = true;
				this.CurrentNote.Editing = true;
			}
		}

		public void Down()
		{
			var row = this.CurrentPattern.SelectedItem;
			var rowNum = row.RowNum;
			var newRowNum = (rowNum + 1) % this.CurrentPattern.Rows.Count();
			this.CurrentPattern.SelectedItem = this.CurrentPattern.Rows[newRowNum];
		}

		public void Up()
		{
			var row = (this.CurrentNote.Row.RowNum + this.CurrentPattern.Rows.Count() - 1) % this.CurrentPattern.Rows.Count();
			var channel = this.CurrentNote.ChannelNum;
			Select(this.CurrentPattern.Rows[row].Channels[channel]);
		}

		public void Left()
		{
			var row = this.CurrentNote.Row.RowNum;
			var channel = (this.CurrentNote.ChannelNum + this.CurrentPattern.Rows[row].Channels.Count() - 1) % this.CurrentPattern.Rows[row].Channels.Count();
			Select(this.CurrentPattern.Rows[row].Channels[channel]);
		}

		public void Right()
		{
			var row = this.CurrentNote.Row.RowNum;
			var channel = (this.CurrentNote.ChannelNum + 1) % this.CurrentPattern.Rows[row].Channels.Count();
			Select(this.CurrentPattern.Rows[row].Channels[channel]);
		}
		

	}
}
