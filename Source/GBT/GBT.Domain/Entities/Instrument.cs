using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBT.Domain.Entities
{
	public class Instrument : IInitializable
	{
		public virtual int Duration { get; set; }
		public virtual int Waveform { get; set; }
		public virtual int Volume { get; set; }
		public virtual int PitchOffset { get; set; }

		public void Initialize()
		{
		}
	}
}
