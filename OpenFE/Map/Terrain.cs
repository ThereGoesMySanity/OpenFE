using System;
using System.Collections.Generic;
using System.IO;

namespace OpenFE
{
	
	public class Terrain
	{
		public Dictionary<string, int> Bonus { get; set; }
		public Dictionary<string, int> MovCost { get; set; }
		public string Name { get; set; }
		public Terrain(string file, string script = null)
		{
			using (StreamReader sr = new StreamReader("Scripts/Terrain/" + file + ".txt"))
			{
				Name = sr.ReadLine();
				string s = sr.ReadLine();
				if (s == "[Bonus]")s = sr.ReadLine();
				Bonus = new Dictionary<string, int>();
				while (s != "[MovementCost]")
				{
					string[] ss = s.Split(' ');
					Bonus.Add(ss[0], Int32.Parse(ss[1]));
					s = sr.ReadLine();
				}
				MovCost = new Dictionary<string, int>();
				while (!sr.EndOfStream)
				{
					string[] ss = sr.ReadLine().Split(' ');
					MovCost.Add(ss[0], Int32.Parse(ss[1]));
				}
			}
			if (!(script == null || script == ""))
			{

			}
		}
	}
}
