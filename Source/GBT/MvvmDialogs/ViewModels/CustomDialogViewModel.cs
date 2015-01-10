using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmDialogs.ViewModels
{
	public class CustomDialogViewModel : ViewModelBase, IUserDialogViewModel
	{
		public virtual bool IsModal
		{
			get { return true; }
		}

		public virtual void RequestClose()
		{
			if (this.DialogClosing != null)
				this.DialogClosing(this, new EventArgs());
		}

		public virtual event EventHandler DialogClosing;

		public virtual void Show(IDialogViewModelCollection dialogs)
		{
			dialogs.Add(this);
		}
	}
}
