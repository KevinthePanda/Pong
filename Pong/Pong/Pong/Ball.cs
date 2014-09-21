using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Pong
{

    class Ball
    {
        //initial/default ball speed and size 

        private const float BALLSPEED = 4;
        private const int BALLSIZE = 10;

        //private member variables
        public float speed;
        private Vector2 position;
        public Vector2 velocity;
        public Rectangle Boundary { get; set; }


        private int time;

        public int points1;
        public int points2;

        //constructor
        public Ball()
        {
            //Reset the ball
            Reset();
        }

        //Reset Method
        //This method initializes the ball speed, position, angle, velocity and boundary
        public void Reset()
        {
            //set initial speed
            speed = BALLSPEED;

            if (Game1.difficulty == Difficulty.Hard)
            {
                speed = 7
                    ;
            }
            Random random = new Random();
            Random random2 = new Random();
            //double x = random.NextDouble();
            //if (x < 1)
            //{
            //    x = x + 1;
            //}
            //double y = random2.NextDouble();
            //if (y < 1)
            //{
            //    y = y + 1;
            //}

            int x = 1;
            int y = 1;
            int number = random.Next(1,5);


            if (number == 2)
            {
                x = x * -1;
                y = y * -1;
            }
            else if (number == 3)
            {
                x = x * -1;
            }
            else if (number == 4)
            {
                y = y * -1;
            }



            velocity = new Vector2((float) x, (float) y);


            velocity = speed * velocity;

            //position in centre of screen
            position = new Vector2((Game1.vWindowSize.X - BALLSIZE) / 2, (Game1.vWindowSize.Y - BALLSIZE) / 2);  //use window size and ball size

            Boundary = new Rectangle((int)position.X, (int)position.Y, BALLSIZE, BALLSIZE);

            time = 0;
        }

        //Update gets called in the Game1 update method
        public void Update()
        {
            //Check if the ball is to bounce off the top or bottom portion of screen and change the velocity (velocity.Y = velocity.Y * -1)
            //If the ball is past the left edge or past the right edge of the screen then award a point to respective player (don't forget to reset after a goal)
            //Put this code here
            time++;
 

            if (Boundary.Top <= 0 || Boundary.Bottom >= Game1.vWindowSize.Y)
            {
                velocity.Y *= -1;
                Game1.wall.Play();
            }

            if (Boundary.Left <= 0) 
            {
                points1++;
                Game1.paddle.Play(1,1,0);
                Reset();
            }
            if (Boundary.Right >= Game1.vWindowSize.X)
            {
                points2++;
                Game1.paddle.Play(1, 1, 0);
                Reset();
            }
            
            

            //get new position and boundary
            if (time >= 120)
            {
                position += velocity;
            }
            Boundary = new Rectangle((int)position.X, (int)position.Y, BALLSIZE, BALLSIZE);

        }

        //call this method in the draw method of Game1 class
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Ball
            spriteBatch.Draw(Game1.pixel, Boundary, Color.White);
        }


    }
}
