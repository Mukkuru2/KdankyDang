using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects.MarioMovementStrategy
{
    class NormalMovement : MovementStrategy
    {
        private readonly Dictionary<Keys, Vector2> MovementDict = new Dictionary<Keys, Vector2>();

        protected readonly float leftRightAccelerationModifier = 2000;
        protected readonly float jumpAccelerationModifier = -4400;
        protected readonly float leftRightResistance = 0.9f;
        protected readonly float fallResistance = 0.98f;

        private float gravity = 1000;

        static protected DateTime jumpTimeStart;
        protected TimeSpan totalJumpTime = TimeSpan.FromMilliseconds(150);

        public NormalMovement() {
            MovementDict.Add(Keys.Left, new Vector2(-1 * leftRightAccelerationModifier, 0));
            MovementDict.Add(Keys.A, new Vector2(-1 * leftRightAccelerationModifier, 0));
            MovementDict.Add(Keys.Right, new Vector2(1 * leftRightAccelerationModifier, 0));
            MovementDict.Add(Keys.D, new Vector2(1 * leftRightAccelerationModifier, 0));
        }

        public override void HandleMovement(Mario mario, InputHelper inputHelper)
        {
            mario.Velocity = new Vector2(mario.Velocity.X * leftRightResistance, mario.Velocity.Y * fallResistance);

            foreach (KeyValuePair<Keys, Vector2> kvp in MovementDict)
            {
                if (inputHelper.IsKeyDown(kvp.Key))
                {
                    mario.Accelleration += kvp.Value;
                }
            }

            Console.WriteLine(jumpTimeStart);

            if (inputHelper.IsKeyDown(Keys.Up) && DateTime.UtcNow - jumpTimeStart < totalJumpTime)
            {
                mario.Accelleration = new Vector2(mario.Accelleration.X, mario.Accelleration.Y + jumpAccelerationModifier);
            }

            mario.Accelleration = new Vector2(mario.Accelleration.X, mario.Accelleration.Y + gravity);
        }
    }
}
