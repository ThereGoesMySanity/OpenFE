using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework.Graphics;

namespace OpenFE
{
	public class Map
	{
		public Terrain[][] Grid { get; private set; }
		public Map(int w, int h, string file, Texture2D sprite)
		{
			Grid = new Terrain[w][];
			for (int i = 0; i < w; i++)
			{
				Grid[i] = new Terrain[h];
			}
			using (StreamReader sr = new StreamReader(file))
			{
				int l = Int32.Parse(sr.ReadLine());
				Terrain[] t = new Terrain[l];
				Regex r = new Regex(@"(\d+): ([^:]+):?(.*)");
				for (int i = 0; i < l; i++)
				{
					Match m = r.Match(sr.ReadLine());
					t[i] = new Terrain(m.Captures[0].Value, m.Captures[1].Value);
				}
				for (int i = 0; i < w; i++)
				{
					string s = sr.ReadLine();
					for (int j = 0; j < h; j++)
					{
						Grid[i][j] = t[s[j] - '0'];
					}
				}
			}
		}
	}
}
