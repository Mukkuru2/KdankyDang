using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects.MarioMovementStrategy
{
    abstract class MovementStrategy : GameObject
    {
        public abstract void HandleMovement(Mario mario, InputHelper inputHelper);
    }
}
