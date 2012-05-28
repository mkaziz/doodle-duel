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
    class Scoring
    {
        private Vector2 scorePos = new Vector2(20, 10);
 
        public SpriteFont Font { get; set; }
 
        public int Score { get; set; }
 
        public Scoring()
        {
        }
 
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the Score in the top-left of screen
            spriteBatch.DrawString(
                Font,                          // SpriteFont
                "Score: " + Score.ToString(),  // Text
                scorePos,                      // Position
                Color.White);                  // Tint
        }
    }
}
