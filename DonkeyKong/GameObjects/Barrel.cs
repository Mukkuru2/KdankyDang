using DonkeyKong.GameObjects.MovementStrategies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects
{
    class Barrel : RotatingSpriteGameObject
    {
        private BarrelMovementStrategy movementStrategy = new BarrelNormalMovement();

        public Barrel(Vector2 position, Vector2 velocity) : base("spr_barrel")
        {
            this.position = position;
            this.velocity= velocity;
            Origin = Center;
        }

        public BarrelMovementStrategy MovementStrategy { get => movementStrategy; set => movementStrategy = value; }

        public override void Update(GameTime gameTime)
        {
            Degrees++;
            movementStrategy.HandleMovement(this);
            base.Update(gameTime);

        }
    }
}
