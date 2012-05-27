﻿using System;
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
    public class PlatformModel
    {

        public Model model { get; protected set; }
        protected Matrix world = Matrix.Identity;
        private float modelRotation;
        private Vector3 modelPosition;
        private float initialHeight;
        private float modelScale;

        public Vector3 Position
        {
            get { return modelPosition; }
            set { modelPosition = value; }

        }

        public PlatformModel(Model m, float rotation, Vector3 position, float scale)
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
                    be.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(modelRotation) * Matrix.CreateTranslation(modelPosition) * Matrix.CreateScale(modelScale);
                }

                mesh.Draw();
            }


        }

    }
}
