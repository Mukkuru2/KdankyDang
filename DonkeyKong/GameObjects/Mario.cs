﻿using DonkeyKong.GameObjects.MovementStrategies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects
{
    class Mario : SpriteGameObject
    {
        private MarioMovementStrategy movementStrategy = new MarioNormalMovement();

        public Mario(Vector2 position) : base("spr_mario")
        {
            this.position = position;
        }

        public MarioMovementStrategy MovementStrategy { get => movementStrategy; set => movementStrategy = value; }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);

            if ((position.X < 0 && velocity.X <= 0 && acceleration.X <= 0) ||
                (position.X + sprite.Width > GameEnvironment.Screen.X && velocity.X >= 0 && acceleration.X >= 0))
            {
                velocity.X = 0;
                acceleration.X = 0;
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            movementStrategy.HandleMovement(this, inputHelper);
            base.HandleInput(inputHelper);
        }
    }
}
