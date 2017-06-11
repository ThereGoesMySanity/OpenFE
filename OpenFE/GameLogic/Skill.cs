using System;
using DynamicLua;
namespace OpenFE
{
	public class Skill
	{
		string script;
		public dynamic lua;
		public Skill(string scriptFile)
		{
			lua = new DynamicLua.DynamicLua();
			lua.NewTable("user");
			script = scriptFile;
		}
		public void onTurnStart()
		{
			lua("if onTurnStart~=nil then onTurnStart() end");
		}
		public void onTurnEnd()
		{
            lua("if onTurnEnd~=nil then onTurnEnd() end");
		}
		public void onAttack(Unit enemy)
		{
			dynamic foe;
			if (lua("return enemy == nil"))
			{
				foe = lua.NewTable("enemy");
			}
			else
			{
				foe = lua.enemy;
			}
			enemy.loadIntoLua(foe);
			lua("if onAttack~=nil then onAttack() end");
		}
		public int calculateDamage(Unit enemy, bool attacker)
		{
			dynamic foe;
			if (lua("return enemy == nil"))
			{
				foe = lua.NewTable("enemy");
			}
			else
			{
				foe = lua.enemy;
			}
			enemy.loadIntoLua(foe);
			lua.attacker = attacker;
			return lua("if calculateDamage~=nil then return calculateDamage(attacker) end");
		}
		public void onDefend(Unit enemy)
		{
			dynamic foe;
			if (lua("return enemy == nil"))
			{
				foe = lua.NewTable("enemy");
			}
			else
			{
				foe = lua.enemy;
			}
			enemy.loadIntoLua(foe);
			lua("if onDefend~=nil then onDefend() end");
		}
	}
}
