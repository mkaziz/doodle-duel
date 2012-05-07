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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Background : Microsoft.Xna.Framework.DrawableGameComponent
    {

        Texture2D background;
        Vector2 position;

        public Background(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            position.Y += 5;

            if (position.Y >= this.GraphicsDevice.Viewport.Height)
                position.Y = this.GraphicsDevice.Viewport.Height - background.Height;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sd = new SpriteBatch(this.GraphicsDevice);
            sd.Begin();
            
            sd.Draw(background, position, Color.White);

            if (position.Y >= 0 & position.Y <= this.GraphicsDevice.Viewport.Height)
                sd.Draw(
                    background,
                    new Vector2(position.X, position.Y - background.Height),
                    Color.White);

            sd.End();

            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("background");
            position = Vector2.Zero;
            position.Y = this.GraphicsDevice.Viewport.Height - background.Height;
            base.LoadContent();
        }
    }
}
