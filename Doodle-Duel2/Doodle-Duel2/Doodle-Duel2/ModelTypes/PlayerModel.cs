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
            get { return new Vector3(modelPosition.X,initialHeight-2, modelPosition.Z); }
        }

        public float CurrentHeight 
        {
            get { return modelPosition.Y; }
        }
        //Vars for smooth jumping
        private float jumpTime = 0;
        private float velocity = 20f;//Change around to make jumping higher/lower
        private float gravity = 4.5f; //Can change around to make jumping faster/slower

        public PlayerModel(Model m, float rotation, Vector3 position, float scale, string t) : base(m, rotation, position, scale)
        {
            tag = t;
        }

        public override void Update()
        {
            if (jumpTime > velocity / gravity)
            {
                jumpTime = 0; //restart jump cycle. Right now it prevents user from falling out of frame
            }
            else
            {
                //utilizes y = .5at^s + Vot + yo
                //that is acceleration downward due to gravity, initial velocity, and the initial position
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
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                modelPosition += new Vector3(0, 0, .5f);
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                modelPosition += new Vector3(0, 0, -.5f);

            base.Update();

        }

        public override void Draw(Camera camera)
        {
            base.Draw(camera);
        }


    }
}
