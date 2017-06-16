using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenFE
{
	public class Font
	{
		public Texture2D font;
		public Texture2D numbers;
		private Dictionary<char, Tuple<Point, int>> textLoc;    //character: (location, kerning)
		private Point size;
		public Font(Texture2D font, Texture2D num)
		{
			this.font = font;
			numbers = num;
			size = new Point(10, 16);
			textLoc = new Dictionary<char, Tuple<Point, int>>();
			using (StreamReader sr = new StreamReader("Content/chars.dat"))
			{
				string s = sr.ReadLine();
				int[] kerning = s.ToCharArray().Select(a => a - '0').ToArray();    //char array to int array
				for (int i = 0; (s = sr.ReadLine()) != null; i++)
				{
					for (int j = 0; j < s.Length; j++)
					{
						if (!textLoc.ContainsKey(s[j]))
						{
							textLoc.Add(s[j], new Tuple<Point, int>(new Point(10 * j, 16 * i), kerning[i * 17 + j]));
						}
					}
				}
			}
		}
		public void DrawText(string text, int x, int y, SpriteBatch spriteBatch, Color? color = null)
		{
			if (color == null) color = Color.White;
			int offset = 0;
			foreach (char c in text)
			{
				if (!textLoc.ContainsKey(c)) break;
				if (c == 'j') offset -= 1;  //j has an underhang(? don't know what to call it)
				spriteBatch.Draw(font,
							   new Rectangle(OpenFE.guiScale(x + offset, y), OpenFE.guiScale(size)),
				                 new Rectangle(textLoc[c].Item1, size), color.Value);
				offset += textLoc[c].Item2;
			}
		}
		public int TextSize(string text)
		{
			int i = 0;
			foreach (char c in text)
			{
				i += textLoc[c].Item2;
			}
			return i;
		}
		public void DrawNumbers(string text, int x, int y, SpriteBatch spriteBatch)
		{
			if (text.ToCharArray().Min() < '0' || text.ToCharArray().Max() > '9')
			{
				throw new ArgumentException("Non-number character in text");
			}
			for (int i = 0; i < text.Length; i++)
			{
				spriteBatch.Draw(numbers,
								 new Rectangle(OpenFE.guiScale(x + 7 * i, y), OpenFE.guiScale(8, 8)),
								 new Rectangle(8 * (text[i] - '0'), 0, 8, 8), Color.White);
			}
		}
		public void DrawNumbersLeft(string text, int x, int y, SpriteBatch spriteBatch)
		{
			if (text.ToCharArray().Min() < '0' || text.ToCharArray().Max() > '9')
			{
				throw new ArgumentException("Non-number character in text");
			}
			for (int i = 0; i < text.Length; i++)
			{
				spriteBatch.Draw(numbers,
								 new Rectangle(OpenFE.guiScale(x - 7 * (text.Length - i), y), 
				                               OpenFE.guiScale(8, 8)),
								 new Rectangle(8 * (text[i] - '0'), 0, 8, 8), Color.White);

			}
		}
		public void DrawNumbers(int num, int x, int y, SpriteBatch spriteBatch)
		{
			DrawNumbers(num.ToString(), x, y, spriteBatch);
		}
		public void DrawNumbersLeft(int num, int x, int y, SpriteBatch spriteBatch)
		{
			DrawNumbersLeft(num.ToString(), x, y, spriteBatch);
		}

	}
}