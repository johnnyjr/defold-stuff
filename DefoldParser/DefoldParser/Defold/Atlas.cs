using System;
using System.Collections.Generic;

namespace Defold
{
	public class Atlas
	{
		public List<AtlasImage> Images { get; set;}
		public List<AtlasAnimation> Animations { get; set; }
		public int Margin {get;set;}
		public byte ExtrudeBorders { get; set;}
		public byte InnerPadding { get; set;}

		public Atlas ()
		{
			Images = new List<AtlasImage> ();
			Animations = new List<AtlasAnimation> ();
		}


		public List<KeyValuePair<string, object>> Serialize() {
			var list = new List<KeyValuePair<string, object>> ();

			foreach (var animation in Animations) {
				list.Add (new KeyValuePair<string, object>("animations", animation.Serialize()));
			}

			list.Add (new KeyValuePair<string, object>("margin", Margin.ToString()));
			list.Add (new KeyValuePair<string, object>("extrude_borders", ExtrudeBorders.ToString()));
			list.Add (new KeyValuePair<string, object>("inner_padding", InnerPadding.ToString()));

			return list;
		}
	}
}

