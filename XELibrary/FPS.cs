using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace XELibrary
{
    public sealed partial class FPS : Microsoft.Xna.Framework.DrawableGameComponent
    {
        float fps;
        float updateInterval = 1.0f;
        float timeSinceLastUpdate = 0.0f;
        float framecount = 0;

        public FPS(Game game)
            : this(game, false, false, game.TargetElapsedTime) { }

        public FPS(Game game, bool syncWithVerticalRetrace, bool isFixedTimeStep)
            : this(game, syncWithVerticalRetrace, isFixedTimeStep, game.TargetElapsedTime) { }

        public FPS(Game game, bool synchWithVerticalRetrace, bool isFixedTimeStep, TimeSpan targetElapsedTime)
            : base(game)
        {
            GraphicsDeviceManager graphics =
                (GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager));
                

            graphics.SynchronizeWithVerticalRetrace = synchWithVerticalRetrace;
            Game.IsFixedTimeStep = isFixedTimeStep;
            Game.TargetElapsedTime = targetElapsedTime;
        }

        public sealed override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        public sealed override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public sealed override void Draw(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            framecount++;
            timeSinceLastUpdate += elapsed;
            if (timeSinceLastUpdate > updateInterval)
            {
                fps = framecount / timeSinceLastUpdate;
                Game.Window.Title = "FPS: " + fps.ToString();
                framecount = 0;
                timeSinceLastUpdate -= updateInterval;
            }
            base.Draw(gameTime);
        }
    }
}
