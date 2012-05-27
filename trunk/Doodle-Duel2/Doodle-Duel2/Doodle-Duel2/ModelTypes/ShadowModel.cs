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
    public class ShadowModel : BasicModel
    {

        private PlayerModel linkedPlayer;

        public ShadowModel(Model m, PlayerModel player, float rot) : base(m, rot, player.Position, 1)
        {
            model = m;
            modelPosition = player.Position; 
            linkedPlayer = player;
            modelScale = .1f;
        }

        public override void Update()
        {
            modelPosition = linkedPlayer.Position; 
            modelScale = 1/((linkedPlayer.CurrentHeight+linkedPlayer.Position.Y)*.3f);

            base.Update();

        }


        public override void Draw(Camera camera)
        {
            base.Draw(camera);


        }


    }
}
