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

        private String character;
        private bool hidden;
<<<<<<< .mine
=======        private Camera camera;
        private Random random;
>>>>>>> .theirs        // This is the maximum height achieved by any player. When the maxHeightThusFar is exceeded,
        // the background should scroll
        private float maxHeightThusFar = float.MinValue;

        // Set to true when the background is currently scrolling
        public bool moveBackground;

        public bool hideChar
        {
            get { return hidden; }
            set { hidden = value; }

        }

        public ModelManager(Game game, String c)
            : base(game)
        {
            hidden = false;
            character = c;
            camera = ((Game1)game).camera;
            random = new Random();
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
                /*foreach (PlatformModel platform in platformModels)
                {
                    // If there is a collision between player and platform, set the new platform
                    // as the base platform for that player
                    if (isOnPlatform(model, platform))
                    {
                        model.setNewPlatform();
                    }

                }
                */
                isOnPlatform();
                if (model.initialHeight > 0f)
                {
                    scrollObjects();
                    moveBackground = true;
                }
                else
                    moveBackground = false;

                model.Update();
            }

            foreach (ShadowModel model in shadowModels)
            {
                model.Update();
            }

            foreach (PlatformModel model in platformModels)
            {
                
                if (model.modelPosition.Y < -35)
                {
                    float xRange = 50f;
                    model.modelPosition.Y = 20;
                    model.modelPosition.X = (float)random.NextDouble()*xRange-xRange/2;
                }
                model.Update();
            }
            /*
            /*if (moveBackground)
                scrollObjects();
            */

            // if maxHeightThusFar has changed, the background should be scrolling
            /*if (heightChanged())
                moveBackground = true;
            else
                moveBackground = false;
            */
            base.Update(gameTime);
        }

        /*private void scrollObjects()
        {
            float scrollAmount = 0.6f;
            foreach (PlayerModel model in playerModels)
            {
                // if background is currently scrolling, move player down the screen
                model.Position -= new Vector3(0,scrollAmount,0);
                //model.maxHeightThusFar -= scrollAmount;
                //model.initialHeight -= scrollAmount;
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
             
        }*/

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
            platformModels.Add(new PlatformModel(Game.Content.Load<Model>(@"platform"), 3.184f / 2, new Vector3(0, -5, 0), .5f));
            platformModels.Add(new PlatformModel(Game.Content.Load<Model>(@"platform"), 3.184f / 2, new Vector3(0, 10, 0), .5f));
            platformModels.Add(new PlatformModel(Game.Content.Load<Model>(@"platform"), 3.184f / 2, new Vector3(0, 32, 0), .1f));
            //Must add a player and their shadow with matching string tags. 
<<<<<<< .mine            PlayerModel player1 = new PlayerModel(Game.Content.Load<Model>(@character), 3.184f / 2, new Vector3(0, 0, 0), .5f, "playerone");

=======            PlayerModel player1 = new PlayerModel(Game.Content.Load<Model>(@character), 3.184f / 2, new Vector3(0, -5, 0),.5f, "playerone"); 
            
>>>>>>> .theirs            playerModels.Add(player1);
<<<<<<< .mine            //shadowModels.Add(new ShadowModel(Game.Content.Load<Model>(@"shadow"), player1, 3.184f / 2));

=======            //shadowModels.Add(new ShadowModel(Game.Content.Load<Model>(@"shadow"), player1, 3.184f / 2));
            //platformModels.Add(new PlatformModel(Game.Content.Load<Model>(@"platform"), 3.184f / 2, new Vector3(0, -15, 0), .5f));
            //platformModels.Add(new PlatformModel(Game.Content.Load<Model>(@"platform"), 3.184f / 2, new Vector3(0, 15, 0), .5f));
            //platformModels.Add(new PlatformModel(Game.Content.Load<Model>(@"platform"), 3.184f / 2, new Vector3(0, 32, 0), .1f));
            platformModels.Add(new PlatformModel(Game.Content.Load<Model>(@"platform"), 3.184f / 2, new Vector3(0, -20, 0), .3f));
            platformModels.Add(new PlatformModel(Game.Content.Load<Model>(@"platform"), 3.184f / 2, new Vector3(0, 0, 0), .5f));
            platformModels.Add(new PlatformModel(Game.Content.Load<Model>(@"platform"), 3.184f / 2, new Vector3(0, 20, 0), .1f));
            
>>>>>>> .theirs            base.LoadContent();
        }

        /*private bool heightChanged()
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
        } */

<<<<<<< .mine        private void isOnPlatform()
=======        private float getMaxPlatformHeight()
        {
            float height = float.MinValue;
            foreach (PlatformModel platform in platformModels)
            {
                if (platform.modelPosition.Y > height)
                    height = platform.modelPosition.Y;
            }
            return height;
        }

        private bool isOnPlatform(PlayerModel player, PlatformModel platform)
>>>>>>> .theirs        {
            foreach (var component in platformModels)
            {
                PlatformModel pm = component as PlatformModel;
                //Check x
                if (pm != null)
                {
                    if ((pm.modelPosition.X - pm.Scale * 20) < playerModels[0].Position.X && playerModels[0].Position.X < (pm.modelPosition.X + pm.Scale * 20))
                    {
                       if (Math.Abs(Math.Abs(pm.modelPosition.Y) - Math.Abs(playerModels[0].cPosition.Y)) < 1)
                          {
                            if ((pm.modelPosition.Z - pm.Scale * 20) < playerModels[0].Position.Z && playerModels[0].Position.Z < (pm.modelPosition.Z + pm.Scale * 20))
                            {
                                if (playerModels[0].cVelocity < 0)
                                {
                                    playerModels[0].iHeight = pm.modelPosition.Y;
                                    playerModels[0].jTime = 0;
                                }
                            }
                         }
                    }
                }

            }
        }
    }
}