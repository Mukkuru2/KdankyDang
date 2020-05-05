using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects.MovementStrategies
{
    class MarioNormalMovement : MarioMovementStrategy
    {
        private readonly Dictionary<Keys, Vector2> MovementDict = new Dictionary<Keys, Vector2>();

        protected readonly float leftRightAccelerationModifier = 1500;
        protected readonly float jumpAccelerationModifier = -3000;
        protected readonly float leftRightResistance = 0.9f;
        protected readonly float fallResistance = 0.98f;

        private bool doGravity = true;
        private float gravity = 800;

        static protected DateTime jumpTimeStart;
        protected TimeSpan totalJumpTime = TimeSpan.FromMilliseconds(150);

        public bool DoGravity { get => doGravity; set => doGravity = value; }

        public MarioNormalMovement() {
            MovementDict.Add(Keys.Left, new Vector2(-1 * leftRightAccelerationModifier, 0));
            MovementDict.Add(Keys.A, new Vector2(-1 * leftRightAccelerationModifier, 0));
            MovementDict.Add(Keys.Right, new Vector2(1 * leftRightAccelerationModifier, 0));
            MovementDict.Add(Keys.D, new Vector2(1 * leftRightAccelerationModifier, 0));
        }

        public override void HandleMovement(Mario mario, InputHelper inputHelper)
        {
            mario.Acceleration = new Vector2();
            mario.Velocity = new Vector2(mario.Velocity.X * leftRightResistance, mario.Velocity.Y * fallResistance);

            foreach (KeyValuePair<Keys, Vector2> kvp in MovementDict)
            {
                if (inputHelper.IsKeyDown(kvp.Key))
                {
                    mario.Acceleration += kvp.Value;
                }
            }

            if (inputHelper.IsKeyDown(Keys.Up) && DateTime.UtcNow - jumpTimeStart < totalJumpTime)
            {
                mario.Acceleration = new Vector2(mario.Acceleration.X, mario.Acceleration.Y + jumpAccelerationModifier);
            }

            if (doGravity)
            {
                mario.Acceleration = new Vector2(mario.Acceleration.X, mario.Acceleration.Y + gravity);
            }
        }
    }
}
