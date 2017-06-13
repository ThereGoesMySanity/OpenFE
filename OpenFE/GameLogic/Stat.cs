using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;

namespace OpenFE
{
	public class Stat
	{
		public string Type { get; set; }
		public int Value { get; set; }
		public int Growth { get; set; }
		public List<Modifier> Mods { get; set; }
		public Stat(string type, int val)
		{
			Type = type;
			Value = val;
			Mods = new List<Modifier>();
		}
		/// <summary>
		/// Returns value including mods.
		/// </summary>
		public int getValue()
		{
			return Value + Modifier.Total(Mods);
		}
	}
}
