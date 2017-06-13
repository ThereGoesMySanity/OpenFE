using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoonSharp.Interpreter;

namespace OpenFE
{
	public class Weapon
	{
		public static Dictionary<string, string> WeaponType = new Dictionary<string, string>
		{
			["Sword"] = "Red",
			["Lance"] = "Blue",
			["Axe"] = "Green",
			["Bow"] = "Green",
			["Staff"] = ""
		};
		public string Name { get; set; }
		public string Type { get; set; }
		public string DamageType { get; set; }
		public string Color { get; set; }
		public int Rank { get; set; }
		public bool[] Range { get; set; }
		public bool Equipped { get; set; }
		public Dictionary<string, int> Stats { get; set; }
		private Script lua;
		public Weapon(string file, string script = null)
		{
			Range = new bool[] { false, false, false, false };
			using (StreamReader sr = new StreamReader("Scripts/Weapons/"+file+".txt"))
			{
				Name = sr.ReadLine();
				Type = sr.ReadLine();
				DamageType = sr.ReadLine();
				if (WeaponType.Keys.Contains(Type)) Color = WeaponType[Type];
				else Color = sr.ReadLine();
				switch (sr.ReadLine()[0])
				{
					case 'S':
						Rank = 251;
						break;
					case 'A':
						Rank = 181;
						break;
					case 'B':
						Rank = 121;
						break;
					case 'C':
						Rank = 71;
						break;
					case 'D':
						Rank = 31;
						break;
					case 'E':
						Rank = 1;
						break;
				}
				string s = sr.ReadLine();
				for (int i = 1; i < 4; i++)
				{
					Range[i] = s[i - 1] == '1';
				}
				Stats = new Dictionary<string, int>();
				sr.ReadLine();
				while (!sr.EndOfStream)
				{
					string[] ss = sr.ReadLine().Split(' ');
					Stats.Add(ss[0], Int32.Parse(ss[1]));
				}

			}
			if (script != null)
			{
				lua = new Script();
				lua.DoFile(script);
			}
		}
		public int advantage(Weapon b)
		{
			if (Color.Length + b.Color.Length < 6 || Color == b.Color) return 0;        
								//if they're the same or one or both of them is blank
			if (Color == "Red")
			{
				if (b.Color == "Blue") return -1;
				return 1;
			}
			else if (Color == "Blue")
			{
				if (b.Color == "Green") return -1;
				return 1;
			}
			else
			{
				if (b.Color == "Red") return -1;
				return 1;
			}
		}
		public int getEffectiveMod(Unit enemy)
		{
			if (lua == null) return 1;
			enemy.loadIntoLua(lua.Globals.Get("enemy"));
			return (int)lua.DoString(
				"if getEffectiveMod ~= nil then return getEffectiveMod() else return 1 end"
			).Number;
		}
	}
}
