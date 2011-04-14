using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bricks
{
    class Player
    {
        public int Score { get; set; }
        public int Lives { get; set; }

        public Player(int lives)
        {
            Lives = lives;
        }
    }
}
