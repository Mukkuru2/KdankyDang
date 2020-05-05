using Microsoft.Xna.Framework;

namespace DonkeyKong.GameObjects.MovementStrategies
{
    class BarrelNormalMovement : BarrelMovementStrategy
    {
        protected readonly float leftRightResistance = 1;
        protected readonly float fallResistance = 0.97f;

        private float gravity = 800;
        protected bool onFloor = false;

        public override void HandleMovement(Barrel barrel)
        {
            barrel.Acceleration = new Vector2(0, 0);

            if (!onFloor)
            {
                barrel.Acceleration = new Vector2(barrel.Acceleration.X, barrel.Acceleration.Y + gravity);
            }

            barrel.Velocity = new Vector2(barrel.Velocity.X * leftRightResistance, barrel.Velocity.Y * fallResistance);
        }
    }
}
