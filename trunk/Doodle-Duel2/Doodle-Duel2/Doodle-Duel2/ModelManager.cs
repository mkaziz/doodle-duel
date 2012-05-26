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
        List<PlayerModel> playerModels = new List<PlayerModel>();
        List<ShadowModel> shadowModels = new List<ShadowModel>();
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

            foreach (PlayerModel model in playerModels)
            {
                model.Update();
            }


            foreach (ShadowModel model in shadowModels)
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

                foreach (PlayerModel model in playerModels)
                {
                    model.Draw(((Game1)Game).camera);
                }
                foreach (ShadowModel model in shadowModels)
                {
                    model.Draw(((Game1)Game).camera);
                }
            }

            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            //Must add a player and their shadow with matching string tags. 
            PlayerModel player1 = new PlayerModel(Game.Content.Load<Model>(@"chicken"), 3.184f / 2, new Vector3(0, -15, 0),.5f, "playerone"); 
            playerModels.Add(player1);
            shadowModels.Add(new ShadowModel(Game.Content.Load<Model>(@"shadow"), player1, 3.184f / 2));
            base.LoadContent();
        }

    }
}