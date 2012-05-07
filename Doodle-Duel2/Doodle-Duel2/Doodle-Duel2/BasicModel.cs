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
    public class BasicModel
    {

        public Model model { get; protected set; }
        protected Matrix world = Matrix.Identity;
        private float modelRotation;
        private Vector3 modelPosition;
        private float initialHeight;
        private float jumpTime = 0; 
        private float velocity = 15f;
        private float gravity = 4.5f; 

        public BasicModel(Model m, float rotation, Vector3 position)
        {
            model = m;
            modelRotation = rotation;
            modelPosition = position;
            initialHeight = position.Y;
        }

        public virtual void Update()
        {
            if (jumpTime > velocity/gravity)
            {
                jumpTime = 0;
            }
            else
            {
                float timeSquared = (float)Math.Pow(jumpTime, 2);
                float initialVelocity = velocity * jumpTime;
                float gravityLoss = gravity * timeSquared;
                modelPosition = new Vector3(modelPosition.X, initialVelocity - gravityLoss + initialHeight, modelPosition.Z);
                jumpTime += .05f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                modelPosition += new Vector3(-.5f, 0, 0);
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                modelPosition += new Vector3(.5f, 0, 0);

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
                    be.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(modelRotation) * Matrix.CreateTranslation(modelPosition); ;
                }

                mesh.Draw();
            }


        }

    }
}
