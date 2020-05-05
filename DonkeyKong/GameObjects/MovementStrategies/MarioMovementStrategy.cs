using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects.MovementStrategies
{
    abstract class MarioMovementStrategy : GameObject
    {
        public abstract void HandleMovement(Mario mario, InputHelper inputHelper);
    }
}
