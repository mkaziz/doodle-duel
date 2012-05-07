using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Doodle_Duel2
{
    public class Background
    {

        Texture2D texture;
        Vector2 position, screenSize;

        public Background(Game g)
        {
            screenSize = new Vector2(g.GraphicsDevice.Viewport.Width, g.GraphicsDevice.Viewport.Height);
            texture = g.Content.Load<Texture2D>("background");
            position = Vector2.Zero;
            position.Y = screenSize.Y - texture.Height;
            
        }

<<<<<<< .mine
=======
        public override void Initialize()
        {
            // TODO: Add your initialization code here
>>>>>>> .r16

        public void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            position.Y += 3;

            if (position.Y >= screenSize.Y)
                position.Y = position.Y - texture.Height;

        }

        public void Draw(GameTime gameTime, SpriteBatch sd)
        {
            //this.Update(gameTime);
            
            sd.Draw(texture, position, Color.White);

<<<<<<< .mine
            if (position.Y >= 0 & position.Y <= screenSize.Y)
=======
            if (position.Y >= 0 && position.Y <= this.GraphicsDevice.Viewport.Height)
>>>>>>> .r16
                sd.Draw(
                    texture,
                    new Vector2(position.X, position.Y - texture.Height),
                    Color.White);


        }

<<<<<<< .mine
            

=======
        protected override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("background");
            position = Vector2.Zero;
            position.X = (this.GraphicsDevice.Viewport.Width - background.Width) / 2;
            position.Y = this.GraphicsDevice.Viewport.Height - background.Height;
            base.LoadContent();
        }
>>>>>>> .r16
    }
}
