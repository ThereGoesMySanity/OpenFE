﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenFE
{
	public class MapState : GameState
	{
		Map map;
		public MapState(Map m)
		{
			map = m;
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Font f, GUI gui)
		{
			throw new NotImplementedException();
		}

		public void Update(GameTime gameTime)
		{
			throw new NotImplementedException();
		}
	}
}
