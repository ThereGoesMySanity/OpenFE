using System;
namespace OpenFE
{
	public class RNG
	{
		private Random r;
		private bool average;
		public RNG(bool avg)
		{
			average = avg;
			r = new Random();
		}
		public bool Hit(int i)
		{
			int rand = (int)(r.NextDouble() * 200);
			if (average)
			{
				rand = (rand/2+(int)(r.NextDouble() * 100));
			}
			return rand < i*2;
		}
		public bool Get(int i)
		{
			int rand = (int)(r.NextDouble() * 100);
			return rand < i;
		}
		public int Get()
		{
			return (int)(r.NextDouble() * 100);
		}
	}
}
