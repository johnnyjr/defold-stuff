using System;
using System.Collections.Generic;
using System.Drawing;

namespace Defold
{
	public class TileSource : DefoldEntity
	{

		private string BasePath { get;set; }="";

		public string ImagePath {
			get {
				return System.IO.Path.Combine (BasePath, Image.Replace ("\"", "").TrimStart ("/".ToCharArray ()));
			}
		}

		private Bitmap _img;
		public Bitmap Img { get 
			{ 
				if (_img == null) {
					_img = new Bitmap(ImagePath);
				}
				return _img;

			}
		}

		public string Image {get;set;}
		public int TileWidth { get; set;}
		public int TileHeight {get;set;}
		public int TileMargin {get;set;}
		public int TileSpacing {get;set;}
		public string Collision {get;set;}
		public string CollisionGroups {get;set;}
		public string MaterialTag {get;set;}
		public byte ExtrudeBorders { get; set;}
		public byte InnerPadding { get; set;}
		public List<TileAnimation> Animations {get;set;}

		public TileSource ()
		{
			Animations = new List<TileAnimation> ();
		}

		public TileSource(string basePath) : this() {
			BasePath = basePath;
		}

		public System.Drawing.Rectangle GetTile(int tileNo) {
			var row = (tileNo-1) / Cols;
			var col = tileNo - (row * Cols);

			return new System.Drawing.Rectangle ((col-1) * TileWidth, (row) * TileHeight, TileWidth, TileHeight);
		}

		public int Rows {
			get { 
				return (int) Math.Floor((decimal) Img.Height / TileHeight);
			}
		}

		public int Cols {
			get { 
				return (int) Math.Floor((decimal) Img.Width / TileWidth);
			}
		}


	}
}

