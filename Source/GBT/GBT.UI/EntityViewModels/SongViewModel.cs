using GalaSoft.MvvmLight;
using GBT.Domain.Entities;
using GBT.Domain.IoC;
using GBT.UI.Main;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBT.UI.EntityViewModels
{
	public class SongViewModel : ViewModelBase, IInitializable
	{
		private Song Song;

		private ObservableCollection<PatternViewModel> _Patterns;
		public ObservableCollection<PatternViewModel> Patterns
		{
			get { return _Patterns; }
			set { _Patterns = value; RaisePropertyChanged(() => this.Patterns); }
		}

		public SongViewModel(Song song)
		{
			this.Song = song;
		}

		public void Initialize()
		{
			this.Patterns = new ObservableCollection<PatternViewModel>(
				this.Song.Patterns.Select(
					pattern => Injector.Get<PatternViewModel>(
							new ConstructorArgument("pattern", pattern)
							)
					)
			);
		}
	}
}
