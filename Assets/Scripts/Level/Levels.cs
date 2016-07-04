using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public static class Levels
	{
		public static Dictionary<string, string> Get {
			get {
				return new Dictionary<string, string> () {
					{ "green1", "green2" },
					{ "green2", "green3" },
					{ "green3", "green4" },
					{ "green4", "green5" },
					{ "green5", "green6" },
					{ "green6", "green7" },
					{ "green7", "green8" },
					{ "green8", "green9" },
					{ "green9", null },
					{ "red1", "red2" },
					{ "red2", "red3" },
					{ "red3", "red4"  },
					{ "red4", null  },
				};
			}
		}

	}
}


