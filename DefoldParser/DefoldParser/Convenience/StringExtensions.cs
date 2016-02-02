using System;

namespace Defold.Convenience
{
	public static class StringExtensions
	{
		public static string AddCitations(this string str) {
			if(!str.StartsWith("\"")) {
				str = "\"" + str;
			}
			if(!str.EndsWith("\"")) {
				str = str+"\"";
			}
			return str;
		}

		public static string ConvertAnimationName(this string str) {
			var parts = str.Split("-".ToCharArray());
			if (parts.Length == 1) {
				var dir = ConvertDirection (str);
				if (dir != str) {
					return "idle-" + ConvertDirection (str);
				} else {
					return str;
				}
			} else {
				if (ConvertDirection (parts [0]) != parts [0]) {
					return parts [1] + "-" + ConvertDirection (parts [0]);
				} else {
					return str;
				}
			}
		}

		private static string ConvertDirection(string dir) {
			switch(dir) {
			case "northwest":
				return "nw";
			case "southwest":
				return "sw";
			case "northeast":
				return "ne";
			case "southeast":
				return "se";
			case "north":
			case "south":
			case "west":
			case "east":
				return dir.Substring (0, 1);
			default:
				int result;
				if(int.TryParse(dir,out result)) {
					var directions = new [] { 
						"w",
						"nw",
						"n",
						"ne",
						"e",
						"se",
						"s",
						"sw"
					};
					return directions [result];
				}
				return dir;	
			}
		}
	}
}

