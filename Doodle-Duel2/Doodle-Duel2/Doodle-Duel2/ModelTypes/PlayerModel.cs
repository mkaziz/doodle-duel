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
        private float velocity = 22.5f; //Change around to make jumping higher/lower
        private float gravity = 4.5f; //Can change around to make jumping faster/slower
        private float currentVelocity;

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
        }

        public PlayerModel(Model m, float rotation, Vector3 position, float scale, string t)   : base(m, rotation, position, scale)
        {
            model = m;
            tag = t;
            modelRotation = rotation;
            modelPosition = position;
            initialHeight = position.Y;
            modelScale = scale;
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

    }
}

