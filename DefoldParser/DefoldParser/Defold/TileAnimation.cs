using System;
using System.Collections.Generic;

namespace Defold
{
	public class TileAnimation : DefoldEntity
	{
		public string Id { get; set;}
		public int StartTile {get;set;}
		public int EndTile {get;set;}
		public int Fps {get;set;}
		public string Playback {get;set;}
		public byte FlipHorizontal { get; set;}
		public byte FlipVertical { get; set;}

	}
}

