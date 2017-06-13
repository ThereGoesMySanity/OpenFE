using System;
using System.Collections.Generic;

namespace OpenFE
{
	public class Modifier
	{
		public int Value { get; set; }
		public string ID { get; set; }
		public Modifier(string id, int i)
		{
			Value = i;
			ID = id;
		}
		public static int Total(List<Modifier> l)
		{
			int i = 0;
			foreach (Modifier m in l)
			{
				i += m.Value;
			}
			return i;
		}
		public override bool Equals(object obj)
		{
			if (typeof(object) != typeof(Modifier)) return false;
			Modifier m = (Modifier)obj;
			return ID == m.ID;
		}
	}
}
