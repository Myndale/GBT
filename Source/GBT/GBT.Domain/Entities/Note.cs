using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBT.Domain.Entities
{
	public class Note : IInitializable
	{
		public virtual Tone Key { get; set; }
		public virtual int Octave { get; set; }

		public void Initialize()
		{
			this.Key = Tone.None;
			this.Octave = 4;
		}
		
	}
}
