using GBT.Domain.IoC;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GBT.Domain.Helpers;

namespace GBT.Domain.Entities
{
	public class Song : IInitializable
	{
		public virtual ObservableCollection<Pattern> Patterns { get; set; }

		public void Initialize()
		{
			this.Patterns = Enumerable.Range(0, 1)
				.Select(i => Injector.Get<Pattern>())
				.ToObservableCollection();
		}
	}
}
