using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    class Paddle
    {
         
        //constants
        const int WIDTH = 10;
        const int HEIGHT = 50;
        const int SPEED = 5;

        //members
        Vector2 position;   
        public Rectangle Boundary { get; set; }
        public enum Player { Player1, Player2 }

        Player player;
        //constructor
        public Paddle(Vector2 Position, Player Playernumber)
        {
            //set members
            position = Position;
            Boundary = new Rectangle((int)position.X, (int)position.Y, WIDTH, HEIGHT);
            player = Playernumber;
        }

        
        public void Update(Keyboard keyboard)
        {

            if (player == Player.Player1)
            {
                if (keyboard.IsKeyPress(Keys.W))
                {
                    position.Y = position.Y - SPEED;
                    Boundary = new Rectangle((int)position.X, (int)position.Y, 10, 50);
                }
            }
            if (player == Player.Player1)
            {
                if (keyboard.IsKeyPress(Keys.S))
                {
                    position.Y = position.Y + SPEED;
                    Boundary = new Rectangle((int)position.X, (int)position.Y, 10, 50);
                }
            }
            if (Boundary.Top <= 0)
            {
                position.Y = 0;
            }
            if (Boundary.Bottom >= Game1.vWindowSize.Y)
            {
                position.Y = Game1.vWindowSize.Y - HEIGHT;
            }

            //
            if (player == Player.Player2)
            {
                if (keyboard.IsKeyPress(Keys.Up))
                {
                    position.Y = position.Y - SPEED;
                    Boundary = new Rectangle((int)position.X, (int)position.Y, 10, 50);
                }
            }
            if (player == Player.Player2)
            {
                if (keyboard.IsKeyPress(Keys.Down))
                {
                    position.Y = position.Y + SPEED;
                    Boundary = new Rectangle((int)position.X, (int)position.Y, 10, 50);

                    if (Boundary.Top <= 0)
                    {
                        position.Y = 0;
                    }
                    if (Boundary.Bottom >= Game1.vWindowSize.Y)
                    {
                        position.Y = Game1.vWindowSize.Y - HEIGHT;
                    }

                }
            }
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw paddle            
            spriteBatch.Draw(Game1.pixel, Boundary, Color.White);
    

        }



    }
}
