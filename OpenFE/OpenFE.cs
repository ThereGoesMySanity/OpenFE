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
		public const int SCALE = 4;
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Font font;
		GUI gui;
		public static RNG rand;
		public int RNG {get{return rand.Get();}}

		public OpenFE()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 240*SCALE;
			graphics.PreferredBackBufferHeight = 160*SCALE;
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
			rand = new RNG(true);
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			font = new Font(Content.Load<Texture2D>("sheet_white"), 
			                Content.Load<Texture2D>("battle_numbers"));
			gui = new GUI(Content.Load<Texture2D>("BlankBattles"), 
			              Content.Load<Texture2D>("hp"), 
			              Content.Load<Texture2D>("icons"));
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

			spriteBatch.End();
			base.Draw(gameTime);
		}
		public static Point guiScale(Point p, float scale = 1, bool gui = true)
		{
			if (!gui) return (p.ToVector2()*scale).ToPoint();
			return new Point((int)(p.X * SCALE * scale), (int)(p.Y * SCALE * scale));
		}
		public static Point guiScale(int x, int y, float scale = 1, bool gui = true)
		{
			Contract.Ensures(Contract.Result<Point>() != null);
			if (!gui) return new Point((int)(x * scale), (int)(y * scale));
			return new Point((int)(x * SCALE * scale), (int)(y * SCALE * scale));
		}
	}
}
