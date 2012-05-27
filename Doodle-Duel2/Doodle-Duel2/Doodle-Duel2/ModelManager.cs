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

        // There shouldn't be any off these in the game
        List<BasicModel> models = new List<BasicModel>();
        
        List<PlayerModel> playerModels = new List<PlayerModel>();
        List<ShadowModel> shadowModels = new List<ShadowModel>();
        List<PlatformModel> platformModels = new List<PlatformModel>();
        
        private bool hidden;
 
        // This is the maximum height achieved by any player. When the maxHeightThusFar is exceeded,
        // the background should scroll
        private float maxHeightThusFar = float.MinValue;
        
        // Set to true when the background is currently scrolling
        public bool moveBackground;

        public bool hideChar
        {
            get { return hidden; }
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
                foreach (PlatformModel platform in platformModels)
                {
                    // If there is a collision between player and platform, set the new platform
                    // as the base platform for that player
                    if (isOnPlatform(model, platform))
                    {
                        model.setNewPlatform();
                    }
                }

                model.Update();
            }

            foreach (ShadowModel model in shadowModels)
            {
                model.Update();
            }
            
            foreach (PlatformModel model in platformModels)
            {
                // if background is currently scrolling, move platform down the screen
                model.Update();
            }

            if (moveBackground)
                scrollObjects();
            
            // if maxHeightThusFar has changed, the background should be scrolling
            if (heightChanged())
                moveBackground = true;
            else
                moveBackground = false;

            base.Update(gameTime);
        }

        private void scrollObjects()
        {
            float scrollAmount = 0.6f;
            foreach (PlayerModel model in playerModels)
            {
                // if background is currently scrolling, move player down the screen
                model.modelPosition.Y -= scrollAmount;
                model.maxHeightThusFar -= scrollAmount;
                model.initialHeight -= scrollAmount;
            }

            foreach (ShadowModel model in shadowModels)
            {
                model.modelPosition.Y -= scrollAmount;
            }

            foreach (PlatformModel model in platformModels)
            {
                // if background is currently scrolling, move platform down the screen
                model.modelPosition.Y -= scrollAmount;    
            }
            
        }

        public override void Draw(GameTime gameTime)
        {
            if (hidden == false)
            {
                foreach (PlatformModel model in platformModels)
                {
                    model.Draw(((Game1)Game).camera);
                } 
                
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
            platformModels.Add(new PlatformModel(Game.Content.Load<Model>(@"platform"), 3.184f / 2, new Vector3(0, -5, 0), .5f));
            platformModels.Add(new PlatformModel(Game.Content.Load<Model>(@"platform"), 3.184f / 2, new Vector3(0, 15, 0), .5f));
            platformModels.Add(new PlatformModel(Game.Content.Load<Model>(@"platform"), 3.184f / 2, new Vector3(0, 32, 0), .1f));
            
            base.LoadContent();
        }

        private bool heightChanged()
        {
            float newMaxHeightThusFar = float.MinValue;

            // find the maximum Y-point of all players in the game
            foreach (PlayerModel m in playerModels)
            {
                if (m.maxHeightThusFar > newMaxHeightThusFar)
                {
                    newMaxHeightThusFar = m.maxHeightThusFar;
                }
            }

            // if this is greater than the maximum y point achieved thus far, 
            // scroll background and set maxHeightThusFar appropriately
            if (newMaxHeightThusFar > maxHeightThusFar)
            {
                maxHeightThusFar = newMaxHeightThusFar;
                return true;
            }

            return false;
        }

        private bool isOnPlatform(PlayerModel player, PlatformModel platform)
        {
            if (player.currJumpState == PlayerModel.JumpState.DOWN  &&
                (Math.Abs(player.modelPosition.Y - platform.modelPosition.Y) < 1))
                return true;

            return false;
        }

    }
}