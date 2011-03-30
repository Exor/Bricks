using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XELibrary
{
    public interface IInputHandler 
    {
        KeyboardHandler Keyboard { get; }
        MouseHandler Mouse { get; }
    };

    public partial class InputHandler 
        : Microsoft.Xna.Framework.GameComponent, IInputHandler
    {

        KeyboardHandler keyboard;
        MouseHandler mouse;

        public InputHandler(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IInputHandler), this);

            keyboard = new KeyboardHandler();
            mouse = new MouseHandler();
            Game.IsMouseVisible = true;
        }


        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            keyboard.Update();
            mouse.Update();

            if (keyboard.IsKeyDown(Keys.Escape))
            {
                Game.Exit();
            }

            base.Update(gameTime);
        }

        public KeyboardHandler Keyboard
        {
            get { return (keyboard); }
        }

        public MouseHandler Mouse
        {
            get { return (mouse); }
        }
    }

    public class KeyboardHandler
    {
        KeyboardState previousKeyboardState;
        KeyboardState keyboardState;

        public KeyboardHandler()
        {
            previousKeyboardState = Keyboard.GetState();
        }

        public void Update()
        {
            previousKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
        }

        public bool IsKeyDown(Keys key)
        {
            return (keyboardState.IsKeyDown(key));
        }

        public bool IsHoldingKey(Keys key)
        {
            return (keyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyDown(key));
        }

        public bool WasKeyPressed(Keys key)
        {
            return (keyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key));
        }

        public bool WasKeyReleased(Keys key)
        {
            return (keyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key));
        }
    }

    public class MouseHandler
    {
        MouseState previousMouseState;
        MouseState mouseState;

        public MouseHandler()
        {
            previousMouseState = Mouse.GetState();
        }

        public void Update()
        {
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();
        }

        public bool WasMovedLeft()
        {
            return (previousMouseState.X > mouseState.X);
        }

        public bool WasMovedRight()
        {
            return (previousMouseState.X < mouseState.X);
        }

        public bool WasMovedUp()
        {
            return (previousMouseState.Y > mouseState.Y);
        }

        public bool WasMovedDown()
        {
            return (previousMouseState.Y < mouseState.Y);
        }
    }
}
