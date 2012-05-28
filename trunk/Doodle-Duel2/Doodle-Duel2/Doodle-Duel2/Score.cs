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
using System.Text;

namespace Doodle_Duel2
{
    class Score
    {
        private Vector2 scorePos;

        public SpriteFont Font { get; set; }

        public int scoreVal { get; set; }

        public Score(int x, int y)
        {
            scorePos = new Vector2(x, y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the Score in the top-left of screen
            spriteBatch.DrawString(
                Font,                          // SpriteFont
                "Score: " + scoreVal.ToString(),  // Text
                scorePos,                      // Position
                Color.Black);                  // Tint
        }
    }
}

//class implementation help from Code for Cake
//http://codeforcake.com/blog/?p=185