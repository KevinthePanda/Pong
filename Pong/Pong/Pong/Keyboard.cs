using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
//this class will manage all keyboard event
//within the pong game.

namespace Pong
{
    class Keyboard
    {
        //member variables
        public KeyboardState CurrentKeyboardState;
        public KeyboardState LastKeyboardState;

        public void Update()
        {
            //update the current and last state of the keyboard
            LastKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        }

        public bool IsKeyPress(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }

        public bool IsNewKeyPress(Keys key)
        {
            return(LastKeyboardState.IsKeyUp(key) && CurrentKeyboardState.IsKeyDown(key));            
        }
                  

    }
}
