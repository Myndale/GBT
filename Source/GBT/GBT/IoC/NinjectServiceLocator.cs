using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GBT.Domain.IoC;
using GBT.UI.IoC;
using GBT.UI.Main;

namespace GBT.IoC
{
	public class NinjectServiceLocator
	{
		public NinjectServiceLocator()
		{
			Injector.Init(new ViewModelsModule(), new DomainModules());
		}

		// can this be obtained from the injector directly?
		public MainViewModel Main { get { return Injector.Get<MainViewModel>(); } }
	}
}
