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
    public class ModelManager : Microsoft.Xna.Framework.DrawableGameComponent
    {

        List<BasicModel> models = new List<BasicModel>();
        private bool hidden; 

        public bool hideChar
        {
            get{ return hidden;}
            set { hidden = value; }

        }

        public ModelManager(Game game)
            : base(game)
        {
            hidden = false; 
            // TODO: Construct any child components here
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (BasicModel model in models)
            {
                model.Update();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (hidden == false)
            {
                foreach (BasicModel model in models)
                {
                    model.Draw(((Game1)Game).camera);
                }
            }

            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            models.Add(new BasicModel(Game.Content.Load<Model>(@"chicken"), 3.184f / 2, new Vector3(0, -15, 0),.5f));
            base.LoadContent();
        }

    }
}