using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenFE
{
	public class Animation
	{
		Texture2D frames;
		bool guiAnim;
		int currentFrame;
		int rate;
		int rateTimer = 0;
		int x, y, width, height;
		public Animation(Texture2D frames, int width, int height = 0, int x = 0, int y = 0, int rate = 1, bool guiAnim = false)
		{
			this.frames = frames;
			this.rate = rate;
			this.guiAnim = guiAnim;
			this.width = width;
			this.height = height>0?height:frames.Height;     //defaults to height of image
			this.x = x;
			this.y = y;
		}
		public void Update()
		{
			rateTimer++;	
			if (rateTimer == rate)
			{
				currentFrame++;
				if (currentFrame == frames.Width/width)
				{
					currentFrame = 0;
				}
				rateTimer = 0;
			}
		}
		public void Draw(SpriteBatch spriteBatch, int destX, int destY)
		{
			spriteBatch.Draw(frames,
						new Rectangle(OpenFE.guiScale(destX, destY, 1, guiAnim), OpenFE.guiScale(width, height, 1, guiAnim)),
						new Rectangle(x + currentFrame * width, y, width, height), Color.White);
		}
	}
}
