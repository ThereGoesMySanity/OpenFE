using System;
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

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime, GUI gui)
		{
			gui.DrawGUIBattle(spriteBatch, battle);
		}

		public void Update(GameTime gameTime)
		{
			throw new NotImplementedException();
		}
	}
}
