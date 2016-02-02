using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace Defold
{
	public class TileMap
	{

		public TileMap ()
		{
			Layers = new List<TileMapLayer> ();
		}

		public int Width {get;set;}
		public int Height {get;set;}

		public string TileSet {get;set;}
		public string Material {get;set;}
		public string BlendMode {get;set;}
		public string Filename { get; set;}

		public List<TileMapLayer> Layers {get;set;}

		public static TileMap Parse(string text) {
			var tileMap = new TileMap();
			var rows = text.Split("\n".ToCharArray()).ToList();
			while (rows.Count > 0 && !String.IsNullOrWhiteSpace(rows[0])) {
				if (rows [0].IndexOf ("{") == -1) {
					var propName = rows [0].Substring (0, rows [0].IndexOf (":")).Trim ();
					var prop = "";
					foreach (var part in propName.Split("_".ToCharArray())) {
						prop += part.Substring (0, 1).ToUpper () + part.Substring (1);
					}
					var value = rows [0].Substring (rows [0].IndexOf (":") + 1).Replace("\"","");
					tileMap.GetType ().GetProperty (prop).SetValue (tileMap, value);
					rows.RemoveAt (0);
				} else {
					var propName = rows[0].Substring(0, rows[0].IndexOf("{")).Trim ();
					switch (propName) {
					case "layers":
						var layer = TileMapLayer.Parse (rows);
						tileMap.Layers.Add (layer);
						break;
					}
				}
			}
			return tileMap;
		}


		public override string ToString ()
		{
			var sb = new StringBuilder();
			sb.AppendLine (String.Format ("tile_set: \"{0}\"", TileSet.Trim()));
			foreach (var layer in Layers) {
				sb.AppendLine(layer.ToString ());
			}
			sb.AppendLine (String.Format ("material: \"{0}\"", Material.Trim()));
			sb.AppendLine (String.Format ("blend_mode: {0}", BlendMode.Trim()));
			return sb.ToString ();
		}
		public void Save() {

		}

		public List<KeyValuePair<string, object>> Serialize() {
			var list = new List<KeyValuePair<string, object>> ();

			foreach (var layer in Layers) {
				list.Add (new KeyValuePair<string, object>("layers", layer.Serialize()));
			}

			list.Add (new KeyValuePair<string, object>("tile_set", String.Format ("\"{0}\"", TileSet.Trim())));
			list.Add (new KeyValuePair<string, object> ("material", String.Format ("\"{0}\"", Material.Trim ())));
			list.Add (new KeyValuePair<string, object> ("blend_mode", BlendMode.Trim ().ToString ()));

			return list;
		}
	}
}

