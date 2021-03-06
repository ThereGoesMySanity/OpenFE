﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenFE
{
	public class BattleState : GameState
	{
		Battle battle;
		public BattleState(Battle b)
		{
			battle = b;
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Font f, GUI gui)
		{
			gui.DrawGUIBattle(spriteBatch, f, battle);
		}

		public void Update(GameTime gameTime)
		{
			throw new NotImplementedException();
		}
	}
}
