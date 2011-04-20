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
            spriteBatch.DrawString(font, "Score: " + score.ToString(), new Vector2(scorePosition.X-1, scorePosition.Y-1), Color.White);
            spriteBatch.DrawString(font, "Score: " + score.ToString(), scorePosition, Color.MidnightBlue);
            spriteBatch.DrawString(font, "Balls: " + lives.ToString(), new Vector2(livesPosition.X-1, livesPosition.Y-1), Color.White);
            spriteBatch.DrawString(font, "Balls: " + lives.ToString(), livesPosition, Color.MidnightBlue);
            spriteBatch.DrawString(font, "Level " + levelName.ToString(), new Vector2(levelNamePosition.X-1, levelNamePosition.Y-1), Color.White);
            spriteBatch.DrawString(font, "Level " + levelName.ToString(), levelNamePosition, Color.MidnightBlue);
        }
    }
}
