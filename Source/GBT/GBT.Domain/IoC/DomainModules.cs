using GBT.Domain.Entities;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBT.Domain.IoC
{
	public class DomainModules : NinjectModule
	{
		public override void Load()
		{
			Bind<Song>().ToMethod(ctx => new Song());
			Bind<PatternRow>().ToMethod(ctx => new PatternRow());
			Bind<Note>().ToMethod(ctx => new Note());
		}
	}
}
