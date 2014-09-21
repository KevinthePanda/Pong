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

namespace Pong
{
    public enum GameState
    {
        Menu,
        Instructions,
        Settings,
        Play,
        Pause,
        GameOver, 
        Exit
    }
    public enum Difficulty
    {
        Easy,
        Hard
    }
    public enum Theme
    {
        Green,
        Black,
        Pink,
        Rave,
        Changing
      
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //member variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont fText;
        SpriteFont fInstructionMenu;
        SpriteFont fTitle;


        GameState gameState;    //game state
        public static Difficulty difficulty;
        Theme theme;
        public static Vector2 vWindowSize;    // size of window
        public static Vector2 vPaddle;
        public static Vector2 vPaddle2;
        Keyboard myKeyboard;
        public static Texture2D pixel;
        Ball ball; //ball
        Paddle player1; //first player paddle 
        Paddle player2;
        public static SoundEffect paddle;
        public static SoundEffect wall;
        bool fromMenu = true;
        int x;
        int y;
        int z;
        int a;
        int b;
        int c;
        int change = 0;
         
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            myKeyboard = new Keyboard();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameState = GameState.Menu;
            difficulty = Difficulty.Easy;
            vWindowSize = new Vector2((float)Window.ClientBounds.Width, (float)Window.ClientBounds.Height);
            vPaddle = new Vector2(10, (Window.ClientBounds.Height / 2) - 25);
            vPaddle2 = new Vector2((float)Window.ClientBounds.Width - 20, (float)(Window.ClientBounds.Height / 2) - 25);
     
                
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
           
            pixel = Content.Load<Texture2D>("WhitePixel");
            wall = Content.Load<SoundEffect>("Wall");
            paddle = Content.Load<SoundEffect>("Paddle");
            fTitle = Content.Load<SpriteFont>("Title");
            fText = Content.Load<SpriteFont>("Text");
            fInstructionMenu = Content.Load<SpriteFont>("InstructionsMenu");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();

            // TODO: Add your update logic here
            myKeyboard.Update();
            Random random = new Random();
            x = random.Next(0, 255);
            y = random.Next(0, 255);
            z = random.Next(0,255);

            if (change == 1)
            {
                a = random.Next(0, 200);
                b = random.Next(0, 200);
                c = random.Next(0, 200);
            }
            
            if (gameState == GameState.Menu)
            {
                fromMenu = true;
                player1 = new Paddle(vPaddle, Paddle.Player.Player1);
                player2 = new Paddle(vPaddle2, Paddle.Player.Player2);
                ball = new Ball();
                //check if enter present to set to play game 
                if (myKeyboard.IsNewKeyPress(Keys.Enter))
                {
                    gameState = GameState.Play;
                }
                else if (myKeyboard.IsNewKeyPress(Keys.I))
                {
                    gameState = GameState.Instructions;
                }
                //menu to settings
                else if (myKeyboard.IsNewKeyPress(Keys.S))
                {
                    gameState = GameState.Settings;

                }
                else if (myKeyboard.IsNewKeyPress(Keys.Escape))
                {
                    gameState = GameState.Exit;
                }
                    //initalize the ball and the player paddles    
                    
            }
            if (gameState == GameState.Instructions)
            {
                //instructions to main menu
                if (myKeyboard.IsKeyPress(Keys.M))
                {
                    gameState = GameState.Menu;

                }
            }
            //setting buttons
            if (gameState == GameState.Settings)
            {
                //settings to main menu
                if (myKeyboard.IsNewKeyPress(Keys.M))
                {
                    gameState = GameState.Menu;
                }
                if (myKeyboard.IsNewKeyPress(Keys.E))
                {
                    difficulty = Difficulty.Easy;
                }
                if (myKeyboard.IsNewKeyPress(Keys.H))
                {
                    difficulty = Difficulty.Hard;
                }
                if (myKeyboard.IsNewKeyPress(Keys.G))
                {
                    theme = Theme.Green;   
                }
                if (myKeyboard.IsNewKeyPress(Keys.B))
                {
                    theme = Theme.Black;
                }
                if (myKeyboard.IsNewKeyPress(Keys.P))
                {
                    theme = Theme.Pink;
                }
                if (myKeyboard.IsNewKeyPress(Keys.R))
                {
                    theme = Theme.Rave;
                }
                if (myKeyboard.IsNewKeyPress(Keys.C))
                {
                    theme = Theme.Changing;
                }
                

            }
            //in game buttons
            if (gameState == GameState.Play)
            {
                fromMenu = false;
                //pause game
                if (myKeyboard.IsNewKeyPress(Keys.P))
                {
                    gameState = GameState.Pause;
                }

                //exit game
                if (myKeyboard.IsNewKeyPress(Keys.Escape))
                {
                    gameState = GameState.Exit;
                }
            }

            //pause buttons
            if (gameState == GameState.Pause)
            {
                //pause to resume
                if (myKeyboard.IsNewKeyPress(Keys.Enter))
                {
                    gameState = GameState.Play;
                }

                //pause to escape
                if (myKeyboard.IsNewKeyPress(Keys.Escape))
                {
                    gameState = GameState.Exit;
                }

                //pause to main menu
                if (myKeyboard.IsNewKeyPress(Keys.M))
                {
                    gameState = GameState.Menu;
                }
                if (myKeyboard.IsNewKeyPress(Keys.R))
                {
                    gameState = GameState.Play;
                    ball.points1 = 0;
                    ball.points2 = 0;
                    player1 = new Paddle(vPaddle, Paddle.Player.Player1);
                    player2 = new Paddle(vPaddle2, Paddle.Player.Player2);
                    ball.Reset();

                }
            }

            //exit buttons
            if (gameState == GameState.Exit)
            {
                //exit
                if (myKeyboard.IsNewKeyPress(Keys.Y))
                {
                    Exit();
                }

                //resume
                if (myKeyboard.IsNewKeyPress(Keys.N))
                {
                    if (fromMenu == false)
                    {
                        gameState = GameState.Play;
                    }
                    else
                    {
                        gameState = GameState.Menu;
                    }
                }
            }

            if (gameState == GameState.Play)
            {
                ball.Update();
                player1.Update(myKeyboard);
                player2.Update(myKeyboard);
               
            }
                                      
                         
            
                if(ball.Boundary.Intersects(player1.Boundary))
                {
               
                        if (ball.speed < 12)
                        {
                            ball.velocity.X *= -1.1f;
                            ball.speed = (float)ball.velocity.Length();
                            paddle.Play();
                        }
                        else
                        {
                            ball.velocity.X *= -1;
                            paddle.Play();
                        }                   

                }
            //
                if (ball.Boundary.Intersects(player2.Boundary))
                {
                    //paddle
                    if (ball.speed < 12)
                    {
                        ball.velocity.X *= -1.1f;
                        ball.speed = (float)ball.velocity.Length();
                        paddle.Play();
                    }
                    else
                    {
                        ball.velocity.X *= -1;
                        paddle.Play();
                    }

                }


                if (ball.points1 == 10 || ball.points2 == 10)
                {
                    gameState = GameState.GameOver;

                }

                if (gameState == GameState.GameOver)
                {
                    if (myKeyboard.IsNewKeyPress(Keys.R))
                    {
                        gameState = GameState.Play;
                        ball.points1 = 0;
                        ball.points2 = 0;
                        player1 = new Paddle(vPaddle, Paddle.Player.Player1);
                        player2 = new Paddle(vPaddle2, Paddle.Player.Player2);
                       
                        
                    }
                    if (myKeyboard.IsNewKeyPress(Keys.M))
                    {
                        gameState = GameState.Menu;
                    }
                    if (myKeyboard.IsNewKeyPress(Keys.Escape))
                    {
                        Exit();
                    }
                    

                }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (theme == Theme.Green)
            {
                GraphicsDevice.Clear(Color.DarkOliveGreen);
            }
            if (theme == Theme.Black)
            {
                GraphicsDevice.Clear(Color.Black);
            }
            if (theme == Theme.Pink)
            {
                GraphicsDevice.Clear(Color.HotPink);
            }
            if (theme == Theme.Rave)
            {
                Color background;
                background = new Color(x, y, z);
                GraphicsDevice.Clear(background);
                
            }
            if (theme == Theme.Changing)
            {
                Color background;
                background = new Color(a, b, c);
                GraphicsDevice.Clear(background);
                if (ball.Boundary.Intersects(player2.Boundary) || ball.Boundary.Intersects(player1.Boundary) || ball.Boundary.Top <= 0 || ball.Boundary.Bottom >= vWindowSize.Y)
                {
                    change = 1;
                }
                else
                {
                    change = 0;
                }

            }
            int points1 = ball.points1;
            int points2 = ball.points2;
            
 

            // TODO: Add your drawing code here
            //if (gameState == GameState.Menu)
            switch (gameState)
            {

                case GameState.Menu:      
                string sTitle = "PONG";
                Vector2 vTitle = fTitle.MeasureString(sTitle);
                Vector2 tCenter = new Vector2((float)(vWindowSize.X - vTitle.X) / 2, (float)50);
                string sMainMenu = "Press Enter to begin game!\nPress I for instructions\nPress S for settings\nPress ESC to Exit";
                Vector2 vMainMenu = fText.MeasureString(sMainMenu);
                Vector2 vCentre = (vWindowSize - vMainMenu) / 2;                 
                //Draw the Main Menu on the screen 
                spriteBatch.Begin();
                spriteBatch.DrawString(fTitle, sTitle, tCenter, Color.PeachPuff);
                spriteBatch.DrawString(fText, sMainMenu, vCentre, Color.PeachPuff);
                spriteBatch.End();
                    break;
                case GameState.Play:
                    spriteBatch.Begin();                    
                    player1.Draw(spriteBatch);
                    player2.Draw(spriteBatch);
                    string sPoints2 = "" + points2;
                    string sPlayer1 = "Player 1";
                    string sPlayer2 = "Player 2";
                    Vector2 vPoints2 = fText.MeasureString(sPoints2);
                    Vector2 vPlayer1 = fText.MeasureString(sPlayer1);
                    Vector2 vPlayer2 = fText.MeasureString(sPlayer2);
                
                    ball.Draw(spriteBatch);
                    spriteBatch.DrawString(fText, "" + points1, new Vector2(75, 30), Color.White);
                    spriteBatch.DrawString(fText, sPoints2, new Vector2((float) vWindowSize.X - (75+ (float)vPoints2.X ), 30), Color.White);
                    spriteBatch.DrawString(fText, sPlayer1, new Vector2((75 - ((float)vPlayer1.X /2)), 0), Color.White);
                    spriteBatch.DrawString(fText, sPlayer2, new Vector2((float) vWindowSize.X - (75+ ((float)vPlayer2.X / 2) ), 0), Color.White);
                    spriteBatch.End();
                    break;
                case GameState.Pause:
                    sTitle = "PONG";
                    vTitle = fTitle.MeasureString(sTitle);
                    tCenter = new Vector2((float)(vWindowSize.X - vTitle.X) / 2, (float)50);
                    string sPauseMenu = "Press Enter to resume\nPress Escape to exit\n\nPress M to Return to the main menu\nPress R to restart";
                    Vector2 vPauseMenu = fText.MeasureString(sPauseMenu);
                    Vector2 pCentre = (vWindowSize - vPauseMenu) / 2;
                    //draw pause menu on screen
                    spriteBatch.Begin();
                    spriteBatch.DrawString(fTitle, sTitle, tCenter, Color.PeachPuff);
                    spriteBatch.DrawString(fText, sPauseMenu, pCentre, Color.PeachPuff);
                    spriteBatch.End();   
                    break;

                case GameState.GameOver:
                    string sGameOver = "Press R to restart \n\nPress M to return to the main menu\n\nPress ESC to exit";
                    string sWinner1 = "Player 1 Wins!";
                    string sWinner2 = "Player 2 Wins!";
                    string sGameOverTitle = "Game Over";
                    Vector2 vGameOverTitle = fTitle.MeasureString(sGameOverTitle);
                    Vector2 tGameOverCenter = new Vector2((float)(vWindowSize.X - vGameOverTitle.X) / 2, (float)50);
                    Vector2 vGameOver = fText.MeasureString(sGameOver);
                    Vector2 gCentre = (vWindowSize - vGameOver) / 2;
                    Vector2 vWinner = fTitle.MeasureString(sWinner1);
                    Vector2 wCenter = new Vector2((float)(vWindowSize.X - vWinner.X) /2, (float)100);
                    spriteBatch.Begin();
                    spriteBatch.DrawString(fTitle, sGameOverTitle, tGameOverCenter, Color.PeachPuff);
                    spriteBatch.DrawString(fText, sGameOver, gCentre, Color.PeachPuff);
                    if (ball.points2 == 10)
                    {
                        spriteBatch.DrawString(fTitle, sWinner1, wCenter, Color.PeachPuff);
                    }
                    if (ball.points1 == 10)
                    {
                        spriteBatch.DrawString(fTitle, sWinner2, wCenter, Color.PeachPuff);
                    }
                    spriteBatch.End();
                    break;

                case GameState.Exit:
                    string sExit = "Would you like to exit\nPress Y to exit and N to continue";
                    sTitle = "PONG";
                    vTitle = fTitle.MeasureString(sTitle);
                    tCenter = new Vector2((float)(vWindowSize.X - vTitle.X) / 2, (float)50);
                    Vector2 vExit = fText.MeasureString(sExit);
                    Vector2 cCentre = (vWindowSize - vExit) / 2;
                    //draw exit menu on screen
                    spriteBatch.Begin();
                    spriteBatch.DrawString(fTitle, sTitle, tCenter, Color.PeachPuff);
                    spriteBatch.DrawString(fText, sExit, cCentre, Color.PeachPuff);
                    spriteBatch.End();
                    break;

                case GameState.Instructions:                    
                    sTitle = "PONG";
                    vTitle = fTitle.MeasureString(sTitle);
                    tCenter = new Vector2((float)(vWindowSize.X - vTitle.X) / 2, (float)50);
                    string sInstructions = "INSTRUCTIONS:\nUse the paddle to hit to ball to your opponent\nIf the ball goes past your paddle your opponent gets a point\nThe ball gets faster every time it hits a paddle\nThe first player to 10 points wins!\n\nCONTROLS:\nUse the W and S keys to move Player 1\nUse Up and Down to move Player 2\nPress P to pause the game\nPress ESC to exit the game\n\nPress M to return to the menu";
                    Vector2 vInstructions = fInstructionMenu.MeasureString(sInstructions);
                    Vector2 iCentre = (vWindowSize - vInstructions) / 2;
                    //draw instructions on screen
                    spriteBatch.Begin();
                    spriteBatch.DrawString(fTitle, sTitle, tCenter, Color.PeachPuff);
                    spriteBatch.DrawString(fInstructionMenu, sInstructions, iCentre, Color.PeachPuff);
                    spriteBatch.End();
                    break;

                case GameState.Settings:
                    sTitle = "PONG";
                    vTitle = fTitle.MeasureString(sTitle);
                    tCenter = new Vector2((float)(vWindowSize.X - vTitle.X) / 2, (float)50);
                    string sSettings = "Press E for easy and H for hard\nPress M to return to the menu\n\nPress P for Pink Theme\nPress B for Black Theme\nPress G for Green Theme\nPress R for Rave\nPress C for Chaning Colors";
                    string sEasy = "EASY";
                    string sHard = "HARD";
                    Vector2 vEasy = fTitle.MeasureString(sEasy);
                    Vector2 vHard = fTitle.MeasureString(sHard);
                    Vector2 vSettings = fText.MeasureString(sSettings);
                    Vector2 sCentre = (vWindowSize - vSettings) / 2;
                    //draw settings on screen
                    spriteBatch.Begin();
                    spriteBatch.DrawString(fTitle, sTitle, tCenter, Color.PeachPuff);
                    spriteBatch.DrawString(fText, sSettings, sCentre, Color.PeachPuff);
                    if (difficulty == Difficulty.Easy)
                    {
                        spriteBatch.DrawString(fTitle, sEasy, new Vector2 (20, (float)vWindowSize.Y - (float)vEasy.Y), Color.Yellow);
                        spriteBatch.DrawString(fTitle, sHard, new Vector2((float)vWindowSize.X - (float)vHard.X, (float)vWindowSize.Y - (float) vHard.Y ), Color.PeachPuff);
                    }
                    if (difficulty == Difficulty.Hard)
                    {
                        spriteBatch.DrawString(fTitle, sEasy, new Vector2(20, (float)vWindowSize.Y - (float)vEasy.Y), Color.PeachPuff);
                        spriteBatch.DrawString(fTitle, sHard, new Vector2((float)vWindowSize.X - (float)vHard.X, (float)vWindowSize.Y - (float)vHard.Y), Color.Yellow);
                    }
                    
                    spriteBatch.End();
                    break;
            }


            base.Draw(gameTime);
        }
    }
}
