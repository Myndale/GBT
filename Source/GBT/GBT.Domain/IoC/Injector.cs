using Ninject;
using Ninject.Modules;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBT.Domain.IoC
{
	public class Injector
	{
		private static StandardKernel Kernel { get; set; }

		public static void Init(params INinjectModule[] modules)
		{
			Kernel = new StandardKernel(modules);
		}

		public static T Get<T>()
		{
			return Kernel.Get<T>();
		}

		public static T Get<T>(params IParameter[] parameters)
		{
			return Kernel.Get<T>(parameters);
		}

		public static object Get(Type type)
		{
			try
			{
				return Kernel.Get(type);
			}
			catch (Exception e)
			{
				Debug.Assert(false, e.Message);
				throw e;
			}
		}

		public static void Inject(params IParameter[] parameters)
		{
			Kernel.Inject(parameters);
		}

		public static void Inject(object instance, params IParameter[] parameters)
		{
			Kernel.Inject(instance, parameters);
		}

		public static void Load(params INinjectModule[] modules)
		{
			Kernel.Load(modules);
		}

	}
}
