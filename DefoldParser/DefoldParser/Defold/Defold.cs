using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Reflection;

namespace Defold
{
	public class DefoldHelper
	{
		public static IEnumerable<KeyValuePair<string, object>> ReadFile(string file) {
			var contents = System.IO.File.ReadAllText (file);
			var rows = contents.Split("\n".ToCharArray()).ToList();

			var o = new List<KeyValuePair<string, object>>();

			while (rows.Count > 0) {
				if (!String.IsNullOrWhiteSpace (rows [0])) {
					o.Add (ReadValue (rows));
				} else {
					rows.RemoveAt (0);
				}
			}
			return o;
		}

		public static void Save(IEnumerable<KeyValuePair<string, object>> contents, string file) {
			var str = "";
			foreach (var v in contents) {
				str += GetString (v);
			}
			System.IO.File.WriteAllText (file, str);
		}

		private static string GetString(KeyValuePair<string, object> row) {
			if (row.Value.GetType () == typeof(string)) {
				return row.Key + ": " + row.Value + "\n";	
			} else {
				var str = row.Key + " {\n";
				foreach (var v in row.Value as IEnumerable<KeyValuePair<string, object>>) {
					str = str + GetString (v);
				}
				str=str+"}\n";
				return str;
			}
		}

		public static T ReadFile<T>(string file) {
			var contents = ReadFile (file);
			T instance = (T)Activator.CreateInstance (typeof(T));
			SetValues (contents, instance);
			return instance;
		}

		public static void SetValues(IEnumerable<KeyValuePair<string, object>> contents, object instance) {
			foreach (var kv in contents) {
				if (kv.Value.GetType () == typeof(string)) {
					var prop = instance.GetType ().GetProperty (GetPropertyName (kv.Key));
					prop.SetValue (instance, Convert.ChangeType(kv.Value, prop.PropertyType));
				} else {
					var prop = instance.GetType ().GetProperty (GetPropertyName (kv.Key));
					var type = prop.PropertyType.GetGenericArguments () [0];
					var read = type.GetMethod ("Read");
					var list = Activator.CreateInstance (type);
					SetValues ((IEnumerable<KeyValuePair<string, object>>)kv.Value, list);
					prop.PropertyType.GetMethod ("Add").Invoke(prop.GetValue(instance), new [] { list });
				}
			}
		}

		public static DefoldEntity ReadEntity<T>(KeyValuePair<string, object> kv) where T:DefoldEntity {
			DefoldEntity instance = (T)Activator.CreateInstance (typeof(T));
			return instance;
		}

		public static string GetPropertyName(string propName, Type t = null) {
			var prop = "";

			foreach (var part in propName.Split("_".ToCharArray())) {
				prop += part.Substring (0, 1).ToUpper () + part.Substring (1);
			}
			return prop;
		}

		private static KeyValuePair<string, object> ReadValue(List<string> rows) {
			if (rows [0].IndexOf ("{") == -1) {
				var kv = rows [0].Split (":".ToCharArray ());
				rows.RemoveAt (0);
				return new KeyValuePair<string, object> (kv [0].Trim(), kv [1].Trim());
				//Simple value
			} else {
				//Complex value
				return ReadComplexValue (rows);
			}
		}

		private static KeyValuePair<string, object> ReadComplexValue(List<string> rows) {
			var key = rows[0].Trim("{".ToCharArray()).Trim();
			var val = new List<KeyValuePair<string,object>>();
			rows.RemoveAt (0);
			while (rows.Count > 0 && rows [0].IndexOf ("}") == -1) {
				val.Add (ReadValue (rows));
			}
			rows.RemoveAt (0);
			return new KeyValuePair<string, object> (key, val);
		}
	}
}

