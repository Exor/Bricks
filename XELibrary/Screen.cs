using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XELibrary
{
    public enum ScreenState
    {
        TransitionOn,
        Active,
        TransitionOff,
        Hidden,
    }

    public abstract class Screen
    {
        #region Properties

        //Tells if the screen should be transitioning
        public bool ShouldUpdate { get; set; }

        //Time to transition a screen on or off

        public TimeSpan TransitionOnTime { get; protected set; }
        public TimeSpan TransitionOffTime { get; protected set; }

        //Percent of transition currently finished.
        //0 means it is fully hidden
        //1 means the screen is active

        public float TransitionPercent { get; set; }

        public ScreenState ScreenState { get; set; }

        //When this flag is set to true the screen will transition off and dispose of itself
        bool isExiting = false;
        public bool IsExiting { get; set; }



        public ScreenManager ScreenManager { get; set; }

        #endregion

        #region Initialize

        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }

        #endregion

        #region Update/Draw

        public virtual void Update(GameTime gameTime, bool shouldTransitionOff)
        {
            if (isExiting)
            {
                //Continue the transition off and remove the screen once finished
                ScreenState = ScreenState.TransitionOff;

                if (UpdateTransitionComplete(gameTime))
                {
                    ScreenState = ScreenState.Hidden;
                    ScreenManager.RemoveScreen(this);
                }
            }
            else if (shouldTransitionOff)
            {
                //Continue the transition off and hide the screen once finished
                ScreenState = ScreenState.TransitionOff;

                if (UpdateTransitionComplete(gameTime))
                {
                    ScreenState = ScreenState.Hidden;
                }
            }
            else
            {
                //The screen must be becoming active
                ScreenState = ScreenState.TransitionOn;

                if (UpdateTransitionComplete(gameTime))
                {
                    ScreenState = ScreenState.Active;
                }
            }
        }

        //Updates the current transition percent
        //Returns True when transition is complete
        //Returns False when transition is still in process
        bool UpdateTransitionComplete(GameTime gameTime)
        {
            float percentToTransition;
            if (ScreenState == ScreenState.TransitionOn)
            {
                //Find the percentage to transition by
                if (TransitionOnTime == TimeSpan.Zero)
                    percentToTransition = 1;
                else
                    percentToTransition = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / TransitionOnTime.TotalMilliseconds);

                //Do the transition
                TransitionPercent += percentToTransition;
            }
            if (ScreenState == ScreenState.TransitionOff)
            {
                //Find the percentage to transition by
                if (TransitionOffTime == TimeSpan.Zero)
                    percentToTransition = 1;
                else
                    percentToTransition = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / TransitionOffTime.TotalMilliseconds);

                //Do the transition
                TransitionPercent -= percentToTransition;
            }

            //See if transition is finished
            if (TransitionPercent <= 0 || TransitionPercent >= 1)
            {
                TransitionPercent = MathHelper.Clamp(TransitionPercent, 0, 1);
                return true; //Transition finished
            }

            //Still transitioning
            return false;
        }

        public virtual void Draw(GameTime gameTime)
        {
            Rectangle fullscreen = new Rectangle(0, 0, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height);

            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.Draw(ScreenManager.BlankTexture, fullscreen, Color.Black * (1-TransitionPercent));
            ScreenManager.SpriteBatch.End();
        }

        public virtual void HandleInput(InputHandler input) { }

        #endregion

        #region Public Methods
        
        //Tell the screen to transition off and dispose of itself
        public void ExitScreen()
        {
            if (TransitionOffTime == TimeSpan.Zero)
            {
                ScreenManager.RemoveScreen(this);
            }
            else
            {
                isExiting = true;
            }
        }

        #endregion

    }
}
