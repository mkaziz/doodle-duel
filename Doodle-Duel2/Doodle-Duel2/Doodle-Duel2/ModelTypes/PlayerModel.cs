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
        public JumpState currJumpState;
        public enum JumpState { UP, DOWN };

        public Vector3 Position 
        {

            get { return new Vector3(modelPosition.X,initialHeight-2, modelPosition.Z); }
            set { modelPosition = value;}
        }

        public float CurrentHeight 
        {
            get { return modelPosition.Y; }
        }

        // override from BasicModel, make public
        public float initialHeight;

        //Vars for smooth jumping
        private float jumpTime = 0;
        private float initialVelocity = 20f;
        private float currVelocity = 20f;
        private float gravity = 10f; //Can change around to make jumping faster/slower
        public float maxHeightThusFar = float.MinValue; 

        public void setNewPlatform() {
            initialHeight = modelPosition.Y;
            jumpTime = 0f;
            currVelocity = initialVelocity;
        }

        public PlayerModel(Model m, float rotation, Vector3 position, float scale, string t) : base(m, rotation, position, scale)
        {
            tag = t;
            currJumpState = JumpState.UP;
        }

        public override void Update()
        {
            // calculate velocity via v = u + at;
            currVelocity = initialVelocity - gravity * jumpTime;

            // if velocity is negative (ie character is going downwards) set jumpstate to down
            if (currVelocity <= 0)
                currJumpState = JumpState.DOWN;
            else
                currJumpState = JumpState.UP;

            //that is acceleration downward due to gravity, initial velocity, and the initial position
            float timeSquared = (float)Math.Pow(jumpTime, 2);
            float distFromPlatform =  initialVelocity * jumpTime - gravity * timeSquared/2;

            modelPosition = new Vector3(modelPosition.X, distFromPlatform + initialHeight, modelPosition.Z);

            if (distFromPlatform + initialHeight > maxHeightThusFar)
                maxHeightThusFar = distFromPlatform + initialHeight;

            if (modelPosition.Y <= initialHeight)
            {
                jumpTime = 0f;
                currVelocity = initialVelocity;
            }

            jumpTime += .05f;


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