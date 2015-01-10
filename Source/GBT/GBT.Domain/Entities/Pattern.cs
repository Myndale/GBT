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
	public class Pattern : IInitializable
	{
		public virtual ObservableCollection<PatternRow> Rows { get; set; }

		public void Initialize()
		{
			this.Rows = Enumerable.Range(0, 128)
				.Select(i => Injector.Get<PatternRow>())
				.ToObservableCollection();
		}

	}
}
