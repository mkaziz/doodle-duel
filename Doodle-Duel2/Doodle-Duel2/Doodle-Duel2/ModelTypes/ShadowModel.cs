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
    public class ShadowModel
    {

        public Model model { get; protected set; }
        protected Matrix world = Matrix.Identity;

        //Initial vars
        private Vector3 modelPosition;
        private float modelScale; 
        private PlayerModel linkedPlayer;

        //Vars for smooth jumping
        private float initialHeight;
        private float jumpTime = 0;
        private float velocity = 20f;//Change around to make jumping higher/lower
        private float gravity = 4.5f; //Can change around to make jumping faster/slower

        public ShadowModel(Model m, PlayerModel player)
        {
            model = m;
            modelPosition = player.Position; 
            linkedPlayer = player;
            modelScale = .1f;
        }

        public virtual void Update()
        {
            modelPosition = linkedPlayer.Position; 
            modelScale = 1/((linkedPlayer.CurrentHeight+linkedPlayer.Position.Y)*.3f); 

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
                    be.World = transforms[mesh.ParentBone.Index] * Matrix.CreateScale(modelScale) * Matrix.CreateTranslation(modelPosition);
                }

                mesh.Draw();
            }


        }


    }
}
