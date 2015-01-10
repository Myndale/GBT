using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBT.UI.Localization
{
	public class Language
	{
		public virtual string Description { get; set; }
		public virtual string Culture { get; set; }
		public virtual Dictionary<string, string> Strings { get; set; }

		public virtual string this[string index]
		{
			get
			{
				if (this.Strings.ContainsKey(index))
					return this.Strings[index];
				else
				{
					Debug.WriteLine("Error: Couldn't find localization string '{0}' in '{1}' language file.", index, this.Description);
					return index;
				}
			}
		}
	}
}
