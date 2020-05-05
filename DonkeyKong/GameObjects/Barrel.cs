using DonkeyKong.GameObjects.MovementStrategies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects
{
    class Barrel : RotatingSpriteGameObject
    {
        private BarrelMovementStrategy movementStrategy = new BarrelNormalMovement();
        private static Vector2 barrelStartVelocity = new Vector2(200, -800);

        public Barrel(Vector2 position, Vector2 velocity) : base("spr_barrel")
        {
            this.position = position;
            this.velocity = velocity;
            Origin = Center;
        }

        public BarrelMovementStrategy MovementStrategy { get => movementStrategy; set => movementStrategy = value; }
        public static Vector2 BarrelStartVelocity { get => barrelStartVelocity; set => barrelStartVelocity = value; }

        public override void Update(GameTime gameTime)
        {
            if (velocity.X > 0)
            {
                Degrees += 3;
            }
            else if (velocity.X < 0)
            {
                Degrees -= 3;
            }
            movementStrategy.HandleMovement(this);
            base.Update(gameTime);

        }
    }
}
