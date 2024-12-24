using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameproject.Managers
{
    internal class GameStateManager
    {


        public enum GameState
        {
            StartScreen,
            Playing,
            GameOver,
            Victory
        }

        
            public static GameState CurrentState { get; set; } = GameState.StartScreen;
        

    }
}
