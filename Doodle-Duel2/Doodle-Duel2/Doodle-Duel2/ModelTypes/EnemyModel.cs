using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Doodle_Duel2
{
    public class EnemyModel : BasicModel
    {
        enum Facing {LEFT, RIGHT};
        Facing currentlyFacing;

        float xDisplacement = 0.2f;

        public EnemyModel(Model m, float rotation, Vector3 position, float scale) : base(m, rotation, position, scale)  
        {
            currentlyFacing = Facing.LEFT;
        }

        public override void Update()
        {
            if (currentlyFacing == Facing.RIGHT && modelRotation < Math.PI)
                modelRotation = modelRotation + (float)Math.PI / 10;
            else if (currentlyFacing == Facing.LEFT && modelRotation >= 0)
                modelRotation = modelRotation - (float)Math.PI / 10;


            if (modelPosition.X > 20f)
            {
                xDisplacement = -0.2f;
                currentlyFacing = Facing.RIGHT;
            }
            else if (modelPosition.X < -20f)
            {
                xDisplacement = 0.2f;
                currentlyFacing = Facing.LEFT;
            }
            modelPosition.X = modelPosition.X + xDisplacement;

            //modelRotation = modelRotation + .05f;

            base.Update();
        }

        public override void Draw(Camera camera)
        {
            base.Draw(camera);
        }

    }
}
