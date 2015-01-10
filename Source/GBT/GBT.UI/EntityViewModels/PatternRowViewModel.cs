using GalaSoft.MvvmLight;
using GBT.Domain.Entities;
using GBT.Domain.IoC;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBT.UI.EntityViewModels
{
	public class PatternRowViewModel : ViewModelBase
	{
		private PatternRow Row;

		public virtual ObservableCollection<NoteViewModel> Channels { get; set; }
		public int RowNum { get; private set; }

		/*
		private bool _Selected;
		public bool Selected
		{
			get { return this._Selected; }
			set
			{
				this._Selected = value;
				foreach (var note in this.Channels)
					note.RowSelected = value;
				RaisePropertyChanged(() => this.Selected);
			}
		}
		 * */
		
		public PatternRowViewModel(PatternRow row, int rowNum)
		{
			this.Row = row;
			this.RowNum = rowNum;
			this.Channels = new ObservableCollection<NoteViewModel>(row.Channels.Select((note, index) => 
				Injector.Get<NoteViewModel>(
					new ConstructorArgument("note", note),
					new ConstructorArgument("row", this),
					new ConstructorArgument("channelNum", index)
				)
			));
		}
	}
}
