using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GBT.Domain.Entities;
using GBT.UI.Helpers;
using GBT.UI.Main;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GBT.UI.EntityViewModels
{
	public class PatternViewModel : ViewModelBase
	{
		[Inject]
		public MainViewModel MainViewModel { get; set; }

		private Pattern Pattern;

		private ObservableCollection<PatternRowViewModel> _Rows;
		public ObservableCollection<PatternRowViewModel> Rows
		{
			get { return _Rows; }
			set
			{
				if (this._Rows != value)
				{
					_Rows = value;
					RaisePropertyChanged(() => this.Rows);
				}
			}
		}

		private PatternRowViewModel _SelectedItem = null;
		public PatternRowViewModel SelectedItem
		{
			get { return this._SelectedItem; }
			set
			{
				if (this._SelectedItem != value)
				{
					var editChannel = (SelectedItem == null) ? null : this.SelectedItem.Channels.Where(c => c.Editing).FirstOrDefault();
					this._SelectedItem = value;
					RaisePropertyChanged(() => this.SelectedItem);
					if ((editChannel != null) && (value != null))
						value.Channels[editChannel.ChannelNum].Edit();
				}
			}
		}

		public PatternViewModel(MainViewModel mainViewModel, Pattern pattern)
		{
			this.MainViewModel = mainViewModel;
			this.Pattern = pattern;
			this.Rows = new ObservableCollection<PatternRowViewModel>(pattern.Rows.Select((row, index) => new PatternRowViewModel(row, index)));
		}

		public ICommand KeyDownCommand { get { return new RelayCommand<KeyEventArgs>(OnKeyDown); } }
		private void OnKeyDown(KeyEventArgs args)
		{
			if (this.MainViewModel.CurrentNote != null)
					this.MainViewModel.CurrentNote.OnKeyDown(args);
		}

	}
}
