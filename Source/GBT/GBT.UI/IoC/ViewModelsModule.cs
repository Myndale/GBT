using MvvmDialogs.ViewModels;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GBT.UI.Main;
using GBT.UI.EntityViewModels;

namespace GBT.UI.IoC
{
	public class ViewModelsModule : NinjectModule
	{
		public override void Load()
		{
			Bind<MainViewModel>().ToSelf().InSingletonScope();
			Bind<IDialogViewModelCollection>().To<DialogViewModelCollection>().InSingletonScope();

			Bind<NoteViewModel>().ToSelf();
		}
	}
}
 