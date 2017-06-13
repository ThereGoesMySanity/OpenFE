using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenFE
{
	public class GUI
	{
		Texture2D battleGUI;
		Texture2D hpBar;
		Animation advantage;
		Animation disadvantage;
		private Rectangle screen1 = new Rectangle(0, 0, 120*OpenFE.SCALE, 160*OpenFE.SCALE);
		private Rectangle screen2 = new Rectangle(120*OpenFE.SCALE, 0, 120*OpenFE.SCALE, 160*OpenFE.SCALE);
		private Dictionary<string, int> sides = new Dictionary<string, int>
		{
			["player"] = 0,
			["ally"] = 160,
			["enemy"] = 320,
			["neutral"] = 480
		};
		public GUI(Texture2D battle, Texture2D hp, Texture2D arrows)
		{
			battleGUI = battle;
			hpBar = hp;
			advantage = new Animation(arrows, 7, 10, true, 2, 28);
			disadvantage = new Animation(arrows, 7, 10, true, 4);
		}
		public void UpdateMap(GameTime t)
		{

		}
		public void UpdateBattle(GameTime t)
		{
			disadvantage.Update();
			advantage.Update();
		}
		public void DrawGUIMap(SpriteBatch spriteBatch, Font f, Map map)
		{

		}
		public void DrawGUIBattle(SpriteBatch spriteBatch, Font f, Battle b)
		{
			spriteBatch.Draw(battleGUI, screen1,
			                 new Rectangle(0, sides[b.One.Type], 120, 160), Color.White);
			spriteBatch.Draw(battleGUI, screen2,
							 new Rectangle(120, sides[b.Two.Type], 120, 160), Color.White);
			f.DrawNumbersLeft(b.Hit.Item1, 40, 112, spriteBatch);
			f.DrawNumbersLeft(b.Crit.Item1, 40, 120, spriteBatch);
			f.DrawNumbersLeft(b.Dmg.Item1, 40, 128, spriteBatch);
			f.DrawNumbers(b.Hit.Item2, 238, 112, spriteBatch);
			f.DrawNumbers(b.Crit.Item2, 238, 120, spriteBatch);
			f.DrawNumbers(b.Dmg.Item2, 238, 128, spriteBatch);
			f.DrawNumbersLeft(b.One.HP, 25, 145, spriteBatch);
			f.DrawNumbersLeft(b.Two.HP, 145, 145, spriteBatch);
			f.DrawText(b.One.Weapon.Name, 88 - f.TextSize(b.One.Weapon.Name)/2, 120, spriteBatch);
			f.DrawText(b.Two.Weapon.Name, 167 - f.TextSize(b.Two.Weapon.Name)/2, 120, spriteBatch);
			DrawHP(b.One.HP, b.One.Stats["HP"].Value, 1, spriteBatch);
			DrawHP(b.Two.HP, b.Two.Stats["HP"].Value, 2, spriteBatch);
			f.DrawText(b.One.Name, 28 - f.TextSize(b.One.Name)/2, 8, spriteBatch);
			f.DrawText(b.Two.Name, 212 - f.TextSize(b.Two.Name)/2, 8, spriteBatch);
			int adv = b.One.Weapon.advantage(b.Two.Weapon);
			switch (adv)
			{
				case -1:
					disadvantage.Draw(spriteBatch, 54, 126);
					advantage.Draw(spriteBatch, 133, 126);
					break;
				case 1:
					advantage.Draw(spriteBatch, 133, 126);
					disadvantage.Draw(spriteBatch, 54, 126);
					break;
			}
		}
		void DrawHP(int hp, int max, int num, SpriteBatch spriteBatch)
		{
			if (max > 40)
			{
				if (hp > 40)
				{
					for (int i = 0; i < 40; i++)
					{
						spriteBatch.Draw(hpBar, new Rectangle(OpenFE.guiScale(29 + 120 * (num - 1) + 2 * i, 150),
															  OpenFE.guiScale(2, 7)),
										 new Rectangle(0, 7, 2, 7), Color.White);
					}

					for (int i = 0; i < hp - 40; i++)
					{
						spriteBatch.Draw(hpBar, new Rectangle(OpenFE.guiScale(29 + 120 * (num - 1) + 2 * i, 143),
															  OpenFE.guiScale(2, 7)),
										 new Rectangle(0, 7, 2, 7), Color.White);
					}
					for (int i = hp - 40; i < max - 40; i++)
					{
						spriteBatch.Draw(hpBar, new Rectangle(OpenFE.guiScale(29 + 120 * (num - 1) + 2 * i, 143),
															  OpenFE.guiScale(2, 7)),
										 new Rectangle(0, 0, 2, 7), Color.White);
					}
				}
				else
				{
					for (int i = 0; i < hp; i++)
					{
						spriteBatch.Draw(hpBar, new Rectangle(OpenFE.guiScale(29 + 120 * (num - 1) + 2 * i, 150),
															  OpenFE.guiScale(2, 7)),
										 new Rectangle(0, 7, 2, 7), Color.White);
					}
					for (int i = hp; i < 40; i++)
					{
						spriteBatch.Draw(hpBar, new Rectangle(OpenFE.guiScale(29 + 120 * (num - 1) + 2 * i, 150),
															  OpenFE.guiScale(2, 7)),
										 new Rectangle(0, 0, 2, 7), Color.White);
					}
					for (int i = 0; i < max - 40; i++)
					{
						spriteBatch.Draw(hpBar, new Rectangle(OpenFE.guiScale(29 + 120 * (num - 1) + 2 * i, 143),
															  OpenFE.guiScale(2, 7)),
										 new Rectangle(0, 0, 2, 7), Color.White);
					}
				}
				spriteBatch.Draw(hpBar, new Rectangle(OpenFE.guiScale(109 + 120 * (num - 1), 150),
														  OpenFE.guiScale(1, 7)),
								 new Rectangle(2, 0, 1, 7), Color.White);
				spriteBatch.Draw(hpBar, new Rectangle(OpenFE.guiScale(29 + 120 * (num - 1) + 2 * (max - 40), 143),
														  OpenFE.guiScale(1, 7)),
									 new Rectangle(2, 0, 1, 7), Color.White);
			}
			else
			{
				for (int i = 0; i < hp; i++)
				{
					spriteBatch.Draw(hpBar, new Rectangle(OpenFE.guiScale(29 + 120 * (num - 1) + 2 * i, 146),
															  OpenFE.guiScale(2, 7)),
										 new Rectangle(0, 7, 2, 7), Color.White);
				}
				for (int i = hp; i < max; i++)
				{
					spriteBatch.Draw(hpBar, new Rectangle(OpenFE.guiScale(29 + 120 * (num - 1) + 2 * i, 146),
															  OpenFE.guiScale(2, 7)),
										 new Rectangle(0, 0, 2, 7), Color.White);
				}
				spriteBatch.Draw(hpBar, new Rectangle(OpenFE.guiScale(29 + 120 * (num - 1) + 2 * max, 146),
														  OpenFE.guiScale(1, 7)),
									 new Rectangle(2, 0, 1, 7), Color.White);
			}
		}
	}
}
