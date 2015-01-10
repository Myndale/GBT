using GalaSoft.MvvmLight.Command;
using MvvmDialogs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GBT.UI.About
{
	public class AboutDialogViewModel : CustomDialogViewModel
	{
		public string Title
		{
			get
			{
				return Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title;
			}
		}

		public string Version
		{
			get
			{
				return Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
			}
		}

		public string Copyright
		{
			get
			{
				return Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
			}
		}

		public ICommand OkCommand { get { return new RelayCommand(OnOk); } }
		private void OnOk()
		{
			RequestClose();
		}
	}
}
