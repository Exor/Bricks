using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace XELibrary
{
    public class ScreenManager : DrawableGameComponent
    {
        #region Fields

        Screen screenToAdd;

        List<Screen> screens = new List<Screen>();
        List<Screen> screensToUpdate = new List<Screen>();

        InputHandler input;

        bool isInitialized;

        #endregion

        #region Properties

        SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        SpriteFont font;
        public SpriteFont Font
        {
            get { return font; }
        }

        Texture2D blankTexture;
        public Texture2D BlankTexture
        {
            get { return blankTexture; }
        }

        #endregion

        #region Initialization

        public ScreenManager(Game game)
            : base(game)
        {
            input = new InputHandler(game);
            game.Components.Add(input);
        }

        public override void Initialize()
        {
            base.Initialize();

            isInitialized = true;
        }

        protected override void LoadContent()
        {
            ContentManager content = Game.Content;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = content.Load<SpriteFont>("font");
            blankTexture = content.Load<Texture2D>("blank");

            foreach (Screen screen in screens)
            {
                screen.LoadContent();
            }
        }

        protected override void UnloadContent()
        {
            foreach (Screen screen in screens)
            {
                screen.UnloadContent();
            }
        }

        #endregion

        #region Update/Draw

        public override void Update(GameTime gameTime)
        {
            //Make a copy of all the screens
            screensToUpdate.Clear();

            foreach (Screen screen in screens)
            {
                screensToUpdate.Add(screen);
            }

            //If there is no screen then add the first one
            if (screenToAdd != null && screensToUpdate.Count == 0)
            {
                screens.Add(screenToAdd);
                screenToAdd = null;
            }

            //We assume the screen on top of the stack is active
            bool shouldTransitionOff = false;

            //If we have a screen waiting then the top screen should transition off
            if (screenToAdd != null)
                shouldTransitionOff = true;

            if (screensToUpdate.Count > 0)
            {
                //Do logic for the top screen only
                Screen topScreen = screensToUpdate[screensToUpdate.Count - 1];
                screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

                topScreen.Update(gameTime, shouldTransitionOff);

                //If the top screen has moved all the way off then add the next screen
                if (topScreen.ScreenState == ScreenState.Hidden
                    && screenToAdd != null)
                {
                    screens.Add(screenToAdd);
                    screenToAdd = null;
                }

                //If the top screen has moved all the way off then remove the screen
                //if (topScreen.ScreenState == ScreenState.Hidden
                //    && screenToRemove != null)
                //{
                //    screens.Remove(screenToRemove);
                //    screenToRemove = null;
                //}

                //Handle input for the top screen if it is active
                if (topScreen.ScreenState == ScreenState.Active
                    || topScreen.ScreenState == ScreenState.TransitionOn)
                {
                    topScreen.HandleInput(input);
                    shouldTransitionOff = true;
                }

                //We don't want to handle input if the screen is going away
                if (topScreen.ScreenState == ScreenState.TransitionOff)
                {
                    shouldTransitionOff = true;
                }
            }

            //Loop through the rest of the screens
            //while (screensToUpdate.Count > 0)
            //{
            //    //take the top screen
            //    Screen screen = screensToUpdate[screensToUpdate.Count - 1];
            //    screensToUpdate.RemoveAt(screensToUpdate.Count-1);

            //    //Update it
            //    screen.Update(gameTime, shouldTransitionOff);
            //}

#if DEBUG
            List<string> screenNames = new List<string>();
            foreach (Screen screen in screens)
                screenNames.Add(screen.GetType().Name);

            Debug.WriteLine(string.Join(", ", screenNames.ToArray()));
#endif
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Screen screen in screens)
            {
                //don't draw if the screen is hidden
                if (screen.ScreenState != ScreenState.Hidden)
                    screen.Draw(gameTime);
            }
        }

        #endregion

        #region Public Methods

        public void AddScreen(Screen screen)
        {
            screen.ScreenManager = this;
            screen.IsExiting = false;

            if (isInitialized)
            {
                screen.LoadContent();
            }

            screenToAdd = screen;
        }

        public void RemoveScreen(Screen screen)
        {
            if (isInitialized)
            {
                screen.UnloadContent();
            }

            screens.Remove(screen);
            screensToUpdate.Remove(screen);
        }


        #endregion
    }
}
