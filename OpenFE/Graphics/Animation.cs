using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenFE
{
	public class Animation
	{
		Texture2D image;
		bool guiAnim;
		int currentFrame;
		int rate;
		int rateTimer = 0;
		int x, y, width, height, frameCount;
		public Animation(Texture2D img, int width, int height = 0, int frameCount = 0, 
		                 int x = 0, int y = 0, int rate = 1, bool guiAnim = false)
		{
			/*just img and width: assumes each frame is the height of the image and only one row
			 * add height: changes height of each frame, only one row
			 * add frameCount: allows for multiple rows
			 * add x/y: changes starting location in source image
			 * add rate: changes number of frames per frame of animation
			 * add guiAnim: scales animation with guiScale
			 */
			image = img;
			this.rate = rate;
			this.guiAnim = guiAnim;
			this.width = width;
			this.height = height > 0 ? height : img.Height;     //defaults to height of image
			this.x = x;
			this.y = y;
			this.frameCount = frameCount == 0 ? img.Width / width : frameCount;
		}
		public void Update()
		{
			rateTimer++;
			if (rateTimer == rate)
			{
				currentFrame++;
				if (currentFrame == frameCount)
				{
					currentFrame = 0;
				}
				rateTimer = 0;
			}
		}
		public void Draw(SpriteBatch spriteBatch, int destX, int destY)
		{
			int srcX = x + (currentFrame * width % image.Width);
			int srcY = y + (currentFrame * width / image.Width);
			spriteBatch.Draw(image,
						new Rectangle(OpenFE.guiScale(destX, destY, 1, guiAnim), 
			                          OpenFE.guiScale(width, height, 1, guiAnim)),
			                 new Rectangle(srcX, srcY, width, height), Color.White);
		}
	}
}
