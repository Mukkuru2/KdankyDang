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
        private static Vector2 barrelStartVelocity = new Vector2(200, -500);
        private BarrelMovementStrategy movementStrategy = new BarrelNormalMovement(barrelStartVelocity);
        private Dictionary<Ladder, bool> ladderDict = new Dictionary<Ladder, bool>();

        public Barrel(Vector2 position, Vector2 velocity, Dictionary<Ladder, bool> ladderDict) : base("spr_barrel")
        {
            this.position = position;
            this.velocity = velocity;
            this.ladderDict = ladderDict;
            Origin = Center;
        }

        public BarrelMovementStrategy MovementStrategy { get => movementStrategy; set => movementStrategy = value; }
        public static Vector2 BarrelStartVelocity { get => barrelStartVelocity; set => barrelStartVelocity = value; }
        internal Dictionary<Ladder, bool> LadderDict { get => ladderDict; set => ladderDict = value; }

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
