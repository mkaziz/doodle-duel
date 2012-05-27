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
        public Vector3 modelPosition;
        
        protected Matrix world = Matrix.Identity;
        protected float modelRotation;
        protected float initialHeight;
        protected float modelScale;
        protected BoundingBox boundingBox;

        public BasicModel(Model m, float rotation, Vector3 position, float scale)
        {
            model = m;
            modelRotation = rotation;
            modelPosition = position;
            initialHeight = position.Y;
            modelScale = scale; 
        }

        public virtual void Update()
        {

        }

        public virtual Matrix getWorld()
        {
            return world;
        }

        
        public virtual void Draw(Camera camera)
        {
            // copy-pasted code from some book I have - DO NOT MODIFY WITHOUT JUST CAUSE!
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
