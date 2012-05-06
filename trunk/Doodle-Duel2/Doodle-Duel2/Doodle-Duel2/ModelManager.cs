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
    /// Class to manange all the models in the game
    /// </summary>
    public class ModelManager : Microsoft.Xna.Framework.DrawableGameComponent
    {

        List<BasicModel> models = new List<BasicModel>();

        public ModelManager(Game game)
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
            foreach (BasicModel model in models) 
            {
                model.Draw(((Game1)Game).camera);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (BasicModel model in models)
            {
                model.Update();
            }

            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            models.Add(new BasicModel(Game.Content.Load<Model>(@"chicken")));
            base.LoadContent();
        }

    }
}
