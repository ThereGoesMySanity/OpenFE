using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using MoonSharp.Interpreter;

namespace OpenFE
{
	public class Unit
	{
		public readonly string[] Types =  { "player", "ally", "enemy", "neutral" };
		public readonly string[] StatType = { "HP", "Str", "Skl", "Spd", "Lck", "Def", "Res", "Mov", "Con" };
		public string Type { get; private set; }
		public List<Skill> Skills { get; private set; }
		public string Name { get; private set; }
		public string ClassName { get; private set; }
		public Dictionary<string, Stat> Stats { get; set; }
		public Dictionary<string, int> Proficiency { get; set; }
		public int HP { get; set; }
		public Weapon Weapon { get; set; }
		public int GetAttack(Unit b)
		{
			return Stats["Str"].getValue() + 
				               (Weapon.Stats["Mt"] + Weapon.advantage(b.Weapon)) * Weapon.getEffectiveMod(b);
		}
		public int GetAcc(Unit b)
		{
			return Weapon.Stats["Hit"] +
						 Stats["Skl"].getValue() * 2 +
						 Stats["Lck"].getValue() / 2 +
						 Weapon.advantage(b.Weapon) * 15 +
						 Proficiency[Weapon.Type] >= 251 ? 5 : 0;
		}
		public int GetCrit()
		{
			//TODO: critical bonus
			return Weapon.Stats["Crt"] + 
				         Stats["Skl"].getValue() / 2 + 
				         Proficiency[Weapon.Type] >= 251 ? 5 : 0;
		}
		public int AttackSpeed
		{
			get
			{
				return Math.Max(0, Stats["Spd"].getValue() - Math.Max(0, Weapon.Stats["Wt"] - Stats["Con"].getValue()));
			}
		}
		public Unit(string file, string weapon, string type)
		{
			Stats = new Dictionary<string, Stat>();
			Proficiency = new Dictionary<string, int>();
			Type = type;
			foreach (string w in Weapon.WeaponType.Keys)
			{
				Proficiency.Add(w, 0);
			}
			using (StreamReader sr = new StreamReader("Scripts/Units/" + file + ".txt"))
			{
				Name = sr.ReadLine();
				ClassName = sr.ReadLine();
				sr.ReadLine();
				for (int i = 0; i < 2; i++)
				{
					string[] ss = sr.ReadLine().Split(' ');
					while (ss[0] != "[Growth")
					{
						if (StatType.Contains(ss[0]))
						{
							switch (i)
							{
								case 0:
									Stats.Add(ss[0], 
									          new Stat(ss[0], Int32.Parse(ss[1])));
									break;
								case 1:
									Stats[ss[0]].Growth = Int32.Parse(ss[1]);
									break;
							}
						}
						if(i == 1) break;
						ss = sr.ReadLine().Split(' ');
					}
				}
			}
			HP = Stats["HP"].Value;
			Weapon = new Weapon(weapon);
		}
		public void addSkill(Skill s)
		{
			loadIntoLua(s.lua.Globals.Get("user"));
			Skills.Add(s);
		}
		public void addModifier(string stat, Modifier m)
		{
			Stats[stat].Mods.Add(m);
		}
		public void loadIntoLua(DynValue value)
		{
			Table table = value.Table;
			table.Set("type", DynValue.NewString(Type));
			table.Set("name", DynValue.NewString(Name));
			table.Set("class", DynValue.NewString(ClassName));
			table.Set("stats", DynValue.NewTable(new Script()));
			foreach (string s in Stats.Keys)
			{
				table.Get("stats").Table.Set(s, DynValue.FromObject(new Script(), Stats[s]));
			}
			foreach (string s in Proficiency.Keys)
			{
				table.Set(s, DynValue.NewNumber(Proficiency[s]));
			}

		}
	}
}
