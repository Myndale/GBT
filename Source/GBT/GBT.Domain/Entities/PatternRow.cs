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
	public class PatternRow : IInitializable
	{
		public const int NumChannels = 4;
		public virtual ObservableCollection<Note> Channels { get; set; }

		public void Initialize()
		{
			this.Channels = Enumerable.Range(0, NumChannels)
				.Select(i => Injector.Get<Note>())
				.ToObservableCollection();
		}
	}
}
