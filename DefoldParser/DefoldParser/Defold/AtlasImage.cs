using System;
using System.Collections.Generic;
using Defold.Convenience;

namespace Defold
{
	public class AtlasImage
	{
		public string Image {get;set;}

		public List<KeyValuePair<string, object>> Serialize() {
			var list = new List<KeyValuePair<string, object>> ();

			list.Add (new KeyValuePair<string, object>("image", Image.AddCitations()));

			return list;
		}
	}
}

