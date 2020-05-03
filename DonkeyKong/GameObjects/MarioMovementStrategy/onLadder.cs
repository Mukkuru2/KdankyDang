using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects.MarioMovementStrategy
{
    class OnLadder : NormalMovement
    {
        private readonly float ladderSpeed = -200;

        public override void HandleMovement(Mario mario, InputHelper inputHelper)
        {
            base.HandleMovement(mario, inputHelper);
            if (inputHelper.IsKeyDown(Keys.Up))
            {
                mario.Velocity = new Vector2(0, ladderSpeed);
            }
        }
    }
}
