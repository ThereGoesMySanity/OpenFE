using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Squared.Tiled;

namespace OpenFE
{
	public class FEMap
	{
		Vector2 camera;
		public int Scale { get; set; }
		public Map map { get; private set; }
		public FEMap(Map m)
		{
			map = m;
		}
		public void Update()
		{
			
		}
		public void Draw(SpriteBatch s)
		{
			map.Draw(s, new Rectangle(0, 0, 240 * OpenFE.SCALE, 160 * OpenFE.SCALE), camera);
		}
	}
}
