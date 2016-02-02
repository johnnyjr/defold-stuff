using System;
using System.Collections.Generic;
using System.Text;

namespace Defold
{
	public class TileMapLayer
	{
		public string Id { get; set;}
		public float Z { get; set;}
		public int IsVisible {get;set;}
		public List<TileMapCell> Cells {get;set;}

		public TileMapLayer ()
		{
			Cells = new List<TileMapCell> ();
		}

		public static TileMapLayer Parse(List<string> rows) {
			var layer = new TileMapLayer ();
			while (rows.Count > 0 && rows [0].IndexOf ("}") == -1) {
				if (rows [0].IndexOf ("{") == -1) {
					var propName = rows [0].Substring (0, rows [0].IndexOf (":")).Trim ();
					var prop = "";
					foreach (var part in propName.Split("_".ToCharArray())) {
						prop += part.Substring (0, 1).ToUpper () + part.Substring (1);
					}
					var value = rows [0].Substring (rows [0].IndexOf (":") + 1).Replace("\"","");
					var property = layer.GetType ().GetProperty (prop);
					property.SetValue(layer, Convert.ChangeType(value, property.PropertyType));
				} else {
					var propName = rows [0].Substring (0, rows [0].IndexOf ("{")).Trim ();
					switch (propName) {
					case "cell":
						var cell = TileMapCell.Parse (rows);
						layer.Cells.Add (cell);
						break;
					}
				}
				rows.RemoveAt (0);
			}
			rows.RemoveAt (0);
			return layer;
		}

		public override string ToString ()
		{
			var sb = new StringBuilder ("layers {\n");
			sb.AppendLine (String.Format ("  id: \"{0}\"", Id.Trim()));
			sb.AppendLine (String.Format ("  z: {0}", Z));
			sb.AppendLine (String.Format ("  is_visible: {0}", IsVisible));
			foreach (var cell in Cells) {
				sb.AppendLine (cell.ToString ());
			}
			sb.AppendLine ("}");
			return sb.ToString();
		}

		public List<KeyValuePair<string, object>> Serialize() {
			var list = new List<KeyValuePair<string, object>> ();

			list.Add (new KeyValuePair<string, object>("id", String.Format ("\"{0}\"", Id.Trim())));
			foreach (var cell in Cells) {
				list.Add (new KeyValuePair<string, object> ("cell", cell.Serialize ()));
			}

			list.Add (new KeyValuePair<string, object>("z", Z.ToString()));
			list.Add (new KeyValuePair<string, object>("is_visible", IsVisible.ToString()));

			return list;
		}
	}
}

