using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBT.Domain.Entities
{
	public class Command : IInitializable
	{
		public virtual CommandType Type { get; set; }
		public virtual int X { get; set; }
		public virtual int Y { get; set; }

		public void Initialize()
		{
		}
	}
}
