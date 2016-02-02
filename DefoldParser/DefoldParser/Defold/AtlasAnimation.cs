using System;
using System.Collections.Generic;
using Defold.Convenience;

namespace Defold
{
	public class AtlasAnimation
	{
		public string Id {get;set;}
		public List<AtlasImage> Images {get;set;}
		public string Playback {get;set;}
		public byte FlipHorizontal { get; set;}
		public byte FlipVertical { get; set;}
		public int Fps {get;set;}

		public AtlasAnimation ()
		{
			Images = new List<AtlasImage> ();
			Playback = "PLAYBACK_ONCE_FORWARD";
		}

		public List<KeyValuePair<string, object>> Serialize() {
			var list = new List<KeyValuePair<string, object>> ();

			list.Add (new KeyValuePair<string, object>("id", Id.ToString().AddCitations()));
			foreach (var image in Images) {
				list.Add (new KeyValuePair<string, object> ("images", image.Serialize ()));
			}

			list.Add (new KeyValuePair<string, object>("fps", Fps.ToString()));
			list.Add (new KeyValuePair<string, object>("playback", Playback.ToString()));
			list.Add (new KeyValuePair<string, object>("flip_horizontal", FlipHorizontal.ToString()));
			list.Add (new KeyValuePair<string, object>("flip_vertical", FlipHorizontal.ToString()));

			return list;
		}
	}
}

