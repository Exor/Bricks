using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Bricks
{
    class Hud
    {
        SpriteFont font;
        string levelName;
        Vector2 levelNamePosition;
        int score;
        Vector2 scorePosition;
        int lives;
        Vector2 livesPosition;

        public Hud(ContentManager content, string LevelName, int remainingLives, int currentScore)
        {
            levelNamePosition = new Vector2(550, 0);
            scorePosition = Vector2.Zero;
            livesPosition = new Vector2(0, 20);

            levelName = LevelName;
            lives = remainingLives;
            score = currentScore;

            LoadContent(content);
        }

        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Font");
        }

        public void Update(GameTime gameTime, int CurrentScore, int remainingLives, string currentLevelName)
        {
            score = CurrentScore;
            lives = remainingLives;
            levelName = currentLevelName;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Score: " + score.ToString(), scorePosition, Color.Black);
            spriteBatch.DrawString(font, "Balls: " + lives.ToString(), livesPosition, Color.Black);
            spriteBatch.DrawString(font, "Level " + levelName.ToString(), levelNamePosition, Color.Black);
        }
    }
}
