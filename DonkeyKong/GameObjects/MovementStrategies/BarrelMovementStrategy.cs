using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects.MovementStrategies
{
    abstract class BarrelMovementStrategy : GameObject
    {
        public abstract void HandleMovement(Barrel barrel);
    }
}

