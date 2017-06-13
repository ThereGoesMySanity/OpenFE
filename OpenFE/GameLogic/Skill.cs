using System;
using MoonSharp.Interpreter;

namespace OpenFE
{
	public class Skill
	{
		public Script lua;
		public Skill(string scriptFile)
		{
			lua = new Script();
			lua.DoFile(scriptFile);
		}
		public void onTurnStart()
		{
			lua.DoString("if onTurnStart~=nil then onTurnStart() end");
		}
		public void onTurnEnd()
		{
            lua.DoString("if onTurnEnd~=nil then onTurnEnd() end");
		}
		public void onAttack(Unit enemy)
		{
			DynValue foe = lua.Globals.Get("enemy");
			enemy.loadIntoLua(foe);
			lua.DoString("if onAttack~=nil then onAttack() end");
		}
		public int calculateDamage(Unit enemy, bool attacker)
		{
			DynValue foe = lua.Globals.Get("enemy");
			enemy.loadIntoLua(foe);
			lua.Globals.Set("attacker", DynValue.NewBoolean(attacker));
			return (int)lua.DoString("if calculateDamage~=nil then return calculateDamage(attacker) end").Number;
		}
		public void onDefend(Unit enemy)
		{
			DynValue foe = lua.Globals.Get("enemy");
			enemy.loadIntoLua(foe);
			lua.DoString("if onDefend~=nil then onDefend() end");
		}
	}
}
