using System;
using System.Collections.Generic;
using System.Text;

namespace Defold
{
	public class TileMapCell
	{
		public int X {get;set;}
		public int Y {get;set;}
		public int Tile { get; set;}
		public int HFlip {get;set;}
		public int VFlip {get;set;}

		public static TileMapCell Parse(List<string> rows) {
			var cell = new TileMapCell();
			while (rows.Count > 0 && rows [0].IndexOf ("}") == -1) {
				if (rows [0].IndexOf ("{") == -1) {
					var propName = rows [0].Substring (0, rows [0].IndexOf (":")).Trim ();
					var prop = "";
					foreach (var part in propName.Split("_".ToCharArray())) {
						prop += part.Substring (0, 1).ToUpper () + part.Substring (1);
					}
					var value = rows [0].Substring (rows [0].IndexOf (":") + 1).Trim ("\"".ToCharArray ());
					var property = cell.GetType ().GetProperty (prop);
					property.SetValue(cell, Convert.ChangeType(value, property.PropertyType));
				} 
				rows.RemoveAt (0);
			}
			return cell;
		}

		public override string ToString ()
		{
			var sb = new StringBuilder ("  cell {\n");
			sb.AppendLine (String.Format ("    x: {0}", X));
			sb.AppendLine (String.Format ("    y: {0}", Y));
			sb.AppendLine (String.Format ("    tile: {0}", Tile));
			sb.AppendLine (String.Format ("    h_flip: {0}", HFlip));
			sb.AppendLine (String.Format ("    v_flip: {0}", VFlip));
			sb.AppendLine ("  }");
			return sb.ToString ();
		}

		public List<KeyValuePair<string, object>> Serialize() {
			var list = new List<KeyValuePair<string, object>> ();

			list.Add (new KeyValuePair<string, object>("x", X.ToString()));
			list.Add (new KeyValuePair<string, object>("y", Y.ToString()));
			list.Add (new KeyValuePair<string, object>("tile", Tile.ToString()));
			list.Add (new KeyValuePair<string, object>("h_flip", HFlip.ToString()));
			list.Add (new KeyValuePair<string, object>("v_flip", VFlip.ToString()));

			return list;
		}
	}
}

