using DonkeyKong.GameObjects.MarioMovementStrategy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects
{
    class Mario : RotatingSpriteGameObject
    {
        private MovementStrategy movementStrategy = new NormalMovement();

        public Mario(Vector2 position) : base("spr_mario")
        {
            this.position = position;
        }

        public MovementStrategy MovementStrategy { get => movementStrategy; set => movementStrategy = value; }

        public override void Update(GameTime gameTime)
        {
            if ((position.X < 0 && velocity.X <= 0 && acceleration.X <= 0) || 
                (position.X + sprite.Width > GameEnvironment.Screen.X && velocity.X >= 0 && acceleration.X >= 0))
            {
                velocity.X = 0;
                acceleration.X = 0;
            }
            base.Update(gameTime);
            acceleration.X = 0;
            acceleration.Y = 0;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            movementStrategy.HandleMovement(this, inputHelper);
            base.HandleInput(inputHelper);
        }
    }
}
