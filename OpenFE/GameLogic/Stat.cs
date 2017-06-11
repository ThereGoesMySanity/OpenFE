using System;
namespace OpenFE
{
	public class Stat
	{
		string type;
		public int value, growth;
		public Stat(string type, int val)
		{
			this.type = type;
			value = val;
		}


	}
}
