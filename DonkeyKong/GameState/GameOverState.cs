using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonkeyKong.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DonkeyKong
{
    class GameOverState : GameObjectList
    {

        FinalScoreText finalScoreText;
        private static int finalScore;
        public GameOverState()
        {
            this.Add(new SpriteGameObject("LoseScreen"));
            Reset();
        }

        public static int FinalScore { get => finalScore; set => finalScore = value; }

        public override void Reset()
        {
            finalScoreText = new FinalScoreText(new Vector2(1200, 100), "Final score:         " + finalScore.ToString());
            finalScoreText.Color = Color.Black;
            this.Add(finalScoreText);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.KeyPressed(Keys.Space))
            {
                GameEnvironment.GameStateManager.SwitchTo("StartState");
            }
        }
    }
}  
