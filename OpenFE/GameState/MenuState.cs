using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OpenFE
{
	public class MenuState : GameState
	{
		public string[] options = { "start", "options" };
		int currentOption;
		KeyboardState last;
		public MenuState()
		{
			currentOption = 0;
			last = Keyboard.GetState();
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Font f, GUI gui)
		{
			for (int i = 0; i < options.Length; i++)
			{
				f.DrawText(options[i], 120 - f.TextSize(options[i]) / 2,
						   80 + (i - options.Length / 2) * 10, spriteBatch, 
				           currentOption == i ? Color.Green : Color.White);
			}
		}

		public void Update(GameTime gameTime)
		{
			KeyboardState k = Keyboard.GetState();
			if (k.IsKeyDown(Keys.Up) && !last.IsKeyDown(Keys.Up))
			{
				currentOption = (--currentOption+options.Length) % options.Length;
			}
			if (k.IsKeyDown(Keys.Down) && !last.IsKeyDown(Keys.Down))
			{
				currentOption = ++currentOption % options.Length;
			}
			if (k.IsKeyDown(Keys.Enter))
			{
				switch (currentOption)
				{
					case 0:
						OpenFE.Instance.State = new MapState(new Map());
						break;
				}
			}
			last = k;
		}
	}
}
