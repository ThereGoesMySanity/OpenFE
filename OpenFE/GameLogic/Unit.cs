using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace OpenFE
{
	enum UnitType { ALLY, ENEMY, NEUTRAL }
	enum DamageType { RED, BLUE, GREEN }

	public class Unit
	{
		public readonly string[] WeaponType = { "Sword", "Lance", "Axe", "Bow", "Staff", "Tome" };
		public readonly string[] StatType = { "HP", "Str", "Skl", "Spd", "Lck", "Def", "Res", "Mov", "Con" };
		UnitType unitType;
		DamageType damageType;
		List<Skill> skills;
		string name, className;
		Dictionary<string, Stat> stats;
		Dictionary<string, int> proficiency;
		public Weapon weapon;
		public Unit(string file)
		{
			stats = new Dictionary<string, Stat>();
			proficiency = new Dictionary<string, int>();
			foreach (string w in WeaponType)
			{
				proficiency.Add(w, 0);
			}
			using (StreamReader sr = new StreamReader(file))
			{
				name = sr.ReadLine();
				className = sr.ReadLine();

				Regex r = new Regex("([A-Za-z]+) (\\d+)");
				for (int i = 0; i < 2; i++)
				{
					Match m = r.Match(sr.ReadLine());
					while (m.Success)
					{
						if (StatType.Contains(m.Captures[0].Value))
						{
							switch (i)
							{
								case 0:
									stats.Add(m.Captures[0].Value, 
									          new Stat(m.Captures[0].Value, Int32.Parse(m.Captures[1].Value)));
									break;
								case 1:
									stats[m.Captures[0].Value].growth = Int32.Parse(m.Captures[1].Value);
									break;
							}
						}
						m = r.Match(sr.ReadLine());
					}
				}

			}
		}
		public void addSkill(Skill s)
		{
			loadIntoLua(s.lua.user);
			skills.Add(s);
		}
		public void loadIntoLua(dynamic table)
		{
			//TODO: write this
		}
	}
}
