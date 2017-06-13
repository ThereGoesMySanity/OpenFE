using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenFE
{
	public interface GameState
	{
		void Update(GameTime gameTime);
		void Draw(SpriteBatch spriteBatch, GameTime gameTime, GUI gui);
	}
}
