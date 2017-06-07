using System;
using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OpenFE
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class OpenFE : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Font font;
		public const int GUI_SCALE = 4;

		public OpenFE()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 960;
			graphics.PreferredBackBufferHeight = 640;
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();

		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			font = new Font(Content.Load<Texture2D>("sheet_white"));
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
#endif

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin(samplerState: SamplerState.PointClamp);
			font.drawText("I wonder if this senjenjgjce looks any good...", 0, 0, spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}
		public static Point guiScale(Point p, float scale = 1, bool gui = true)
		{
			if (!gui) return p;
			return new Point((int)(p.X * GUI_SCALE * scale), (int)(p.Y * GUI_SCALE * scale));
		}
		public static Point guiScale(int x, int y, float scale = 1, bool gui = true)
		{
			Contract.Ensures(Contract.Result<Point>() != null);
			if (!gui) return new Point(x,y);
			return new Point((int)(x * GUI_SCALE * scale), (int)(y * GUI_SCALE * scale));
		}
	}
}
