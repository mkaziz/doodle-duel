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
    public class PlayerModel : BasicModel
    {

        private string tag;

        public Vector3 Position
        {
            get { return new Vector3(modelPosition.X, initialHeight - 2, modelPosition.Z); }
            set { modelPosition = value; }
        }

        public Vector3 cPosition
        {
            get { return modelPosition; }
            set { modelPosition = value; }
        } 

        public float CurrentHeight
        {
            get { return modelPosition.Y; }
        }

        public float iHeight
        {
            get { return initialHeight; }
            set { initialHeight = value; }
        }

        public float jTime
        {
            get { return jumpTime; }
            set { jumpTime = value; }
        }
        
        public float cVelocity
        {
            get { return currentVelocity; }
            set { currentVelocity = value; }
        }
        //Vars for smooth jumping
        private float initialHeight;
        private float jumpTime = 0;
        private float velocity = 20f; //Change around to make jumping higher/lower
        private float gravity = 4.5f; //Can change around to make jumping faster/slower
        private float currentVelocity;

        //Vars for projectiles
        private ModelManager manager;
        //To prevent multiple projectiles in one key press
        private KeyboardState oldkeyboard; 

        public float initialVelocity 
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public PlayerModel(Model m, float rotation, Vector3 position, float scale) : base(m,rotation,position,scale)
        {
            model = m;
            modelRotation = rotation;
            modelPosition = position;
            initialHeight = position.Y;
            modelScale = scale;
            oldkeyboard = Keyboard.GetState(); 
        }

        public PlayerModel(Model m, float rotation, Vector3 position, float scale, string t, ModelManager man)   
            : base(m, rotation, position, scale)
        {
            model = m;
            tag = t;
            modelRotation = rotation;
            modelPosition = position;
            initialHeight = position.Y;
            modelScale = scale;
            manager = man;
        }

        public virtual void Update()
        {
            //find velocity via x = gt + Vo
            currentVelocity = -2*gravity * jumpTime + velocity;
                //utilizes y = .5at^2 + Vot + yo
                //that is acceleration downward due to gravity, initial velocity, and the initial position
                float timeSquared = (float)Math.Pow(jumpTime, 2);
                float initialVelocity = velocity * jumpTime;
                float gravityLoss = gravity * timeSquared;
                modelPosition = new Vector3(modelPosition.X, initialVelocity - gravityLoss + initialHeight, modelPosition.Z);
                jumpTime += .05f;

                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    modelPosition += new Vector3(-.5f, 0, 0);
                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    modelPosition += new Vector3(.5f, 0, 0);
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    modelPosition += new Vector3(0, 0, .5f);
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    modelPosition += new Vector3(0, 0, -.5f);
                else if (Keyboard.GetState().IsKeyDown(Keys.Space) && !oldkeyboard.IsKeyDown(Keys.Space))
                    Fire();

                oldkeyboard = Keyboard.GetState();
                
            List<BasicModel> needtoremove = new List<BasicModel>();
               
            if (manager.models.Count > 0)
                {
                    foreach (BasicModel model in manager.models)
                    {
                        //First check if it is off screen
                        if (model.modelPosition.X > manager.Game.GraphicsDevice.Viewport.Width || model.modelPosition.Y > manager.Game.GraphicsDevice.Viewport.Height)
                        {
                            needtoremove.Add(model);
                        }
                        else if (manager.enemyModels.Count > 0)
                        {
                            if ((manager.enemyModels[0].modelPosition - model.modelPosition).X < 2 && (manager.enemyModels[0].modelPosition - model.modelPosition).Y < 2)
                            {
                                needtoremove.Add(model);
                                //manager.models.Remove(model);
                                manager.enemyModels.Remove(manager.enemyModels[0]);
                            }
                            else
                                model.modelPosition += (manager.enemyModels[0].modelPosition - model.modelPosition) * .1f;
                        }
                        else
                            model.modelPosition += new Vector3(0, .7f, 0);
                    }
                }
            if(needtoremove.Count > 0)
                for (int i = 0; i < needtoremove.Count; i++)
                {
                    manager.models.Remove(needtoremove[i]);
                    needtoremove.Remove(needtoremove[i]);
                }

        }

        public virtual Matrix getWorld()
        {
            return world;
        }

        public void Draw(Camera camera)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect be in mesh.Effects)
                {
                    be.EnableDefaultLighting();
                    be.Projection = camera.projection;
                    be.View = camera.view;
                    be.World = getWorld() * mesh.ParentBone.Transform;
                    be.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(modelRotation) * Matrix.CreateScale(modelScale) * Matrix.CreateTranslation(modelPosition);
                }

                mesh.Draw();
            }


        }

        public void Fire()
        {
            manager.models.Add(new BasicModel(manager.Game.Content.Load<Model>(@"projectile"),0, modelPosition, .1f));
        }

    }
}

