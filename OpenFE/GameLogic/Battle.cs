﻿using System;
namespace OpenFE
{
	public class Battle
	{
		public Tuple<int, int> Hit { get; set; }
		public Tuple<int, int> Dmg { get; set; }
		public Tuple<int, int> Crit { get; set; }
		public Unit One { get; set; }
		public Unit Two { get; set; }
		public Terrain TOne { get; set; }
		public Terrain TTwo { get; set; }
		public bool DblAtkOne { get; set; }
		public bool DblAtkTwo { get; set; }
		public Battle(Unit one, Unit two, Terrain tone, Terrain ttwo)
		{
			One = one;
			Two = two;
			TOne = tone;
			TTwo = ttwo;
			int avoOne = one.AttackSpeed * 2 + one.Stats["Lck"].getValue() + tone.Bonus["Avo"];
			int avoTwo = two.AttackSpeed * 2 + two.Stats["Lck"].getValue() + ttwo.Bonus["Avo"];
			Hit = new Tuple<int, int>(one.GetAcc(two) - avoTwo, two.GetAcc(one) - avoOne);
			DblAtkOne = one.AttackSpeed - two.AttackSpeed >= 4;
			DblAtkOne = two.AttackSpeed - one.AttackSpeed >= 4;
			int defOne = one.Stats[two.Weapon.DamageType == "Str" ? "Def" : "Res"].getValue() + tone.Bonus["Def"];
			int defTwo = two.Stats[one.Weapon.DamageType == "Str" ? "Def" : "Res"].getValue() + ttwo.Bonus["Def"];
			Dmg = new Tuple<int, int>(one.GetAttack(two) - defTwo, two.GetAttack(one) - defOne);
			Crit = new Tuple<int, int>(one.GetCrit() - two.Stats["Lck"].getValue(),
									   two.GetCrit() - one.Stats["Lck"].getValue());
		}
		public void Execute(BattleState b)
		{
			if (OpenFE.rand.Hit(Hit.Item1))
			{
				if (OpenFE.rand.Get(Crit.Item1))
				{
					Two.HP -= 3 * Dmg.Item1;
				}
				else
				{
					Two.HP -= Dmg.Item1;
				}
			}
			if (OpenFE.rand.Hit(Hit.Item2))
			{
				if (OpenFE.rand.Get(Crit.Item2))
				{
					One.HP -= 3 * Dmg.Item2;
				}
				else
				{
					One.HP -= Dmg.Item2;
				}
			}
			if (DblAtkOne)
			{
				if (OpenFE.rand.Hit(Hit.Item1))
				{
					if (OpenFE.rand.Get(Crit.Item1))
					{
						Two.HP -= 3 * Dmg.Item1;
					}
					else
					{
						Two.HP -= Dmg.Item1;
					}
				}
			}
			if (DblAtkTwo)
			{
				if (OpenFE.rand.Hit(Hit.Item2))
				{
					if (OpenFE.rand.Get(Crit.Item2))
					{
						One.HP -= 3 * Dmg.Item2;
					}
					else
					{
						One.HP -= Dmg.Item2;
					}
				}
			}
		}
	}
}
