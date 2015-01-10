//using Microsoft.Win32;
using MvvmDialogs.ViewModel;
using MvvmDialogs.Presenters;
using MvvmDialogs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ookii.Dialogs;
using Ookii.Dialogs.Wpf;
using Microsoft.Win32;

namespace MvvmDialogs.Presenters
{
	public class OpenFileDialogPresenter : IDialogBoxPresenter<OpenFileDialogViewModel>
	{
		public void Show(OpenFileDialogViewModel vm, Window parent)
		{
			//var dlg = new OpenFileDialog();
			var dlg = new VistaOpenFileDialog();

			dlg.Multiselect = vm.Multiselect;
			dlg.ReadOnlyChecked = vm.ReadOnlyChecked;
			dlg.ShowReadOnly = vm.ShowReadOnly;
			dlg.FileName = vm.FileName;
			dlg.Filter = vm.Filter;
			dlg.InitialDirectory = vm.InitialDirectory;
			dlg.RestoreDirectory = vm.RestoreDirectory;
			dlg.Title = vm.Title;
			dlg.ValidateNames = vm.ValidateNames;

			var result = dlg.ShowDialog();
			vm.Result = (result != null) && result.Value;

			vm.Multiselect = dlg.Multiselect;
			vm.ReadOnlyChecked = dlg.ReadOnlyChecked;
			vm.ShowReadOnly = dlg.ShowReadOnly;
			vm.FileName = dlg.FileName;
			vm.FileNames = dlg.FileNames;
			vm.Filter = dlg.Filter;
			vm.InitialDirectory = dlg.InitialDirectory;
			vm.RestoreDirectory = dlg.RestoreDirectory;
			vm.Title = dlg.Title;
			vm.ValidateNames = dlg.ValidateNames;			
		}
	}
}
