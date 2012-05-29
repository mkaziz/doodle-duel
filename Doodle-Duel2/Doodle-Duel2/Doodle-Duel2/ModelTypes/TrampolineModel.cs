﻿using System;
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
    class TrampolineModel : BasicModel
    {
        public TrampolineModel(Model m, float rotation, Vector3 position, float scale)
            : base(m, rotation, position, scale)
        {
            modelPosition = position + new Vector3(0, 3, -8);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(Camera camera)
        {
            base.Draw(camera);
        }

        public void boostUp(PlayerModel player)
        {
            player.iHeight = modelPosition.Y;
            player.jTime = 0;
            player.initialVelocity = 30f;
        }


    }
}