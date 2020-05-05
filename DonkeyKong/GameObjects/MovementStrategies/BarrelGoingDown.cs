using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects.MovementStrategies
{
    class BarrelGoingDown : BarrelNormalMovement
    {
        private Vector2 tempVelocity;
        public BarrelGoingDown(Vector2 velocity) : base(velocity)
        {
            tempVelocity = velocity;
        }

        public override void HandleMovement(Barrel barrel)
        {
            barrel.Velocity = new Vector2(0, barrel.Velocity.Y);
            base.HandleMovement(barrel);
        }
    }
}
