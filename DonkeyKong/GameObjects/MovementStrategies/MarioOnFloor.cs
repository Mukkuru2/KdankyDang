using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects.MovementStrategies
{
    class MarioOnFloor : MarioNormalMovement
    {
        private readonly Dictionary<Keys, Vector2> MovementDict = new Dictionary<Keys, Vector2>();

        public MarioOnFloor()
        {
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
                    mario.Acceleration += kvp.Value;
                }
            }

            if (inputHelper.IsKeyDown(Keys.Up) || DateTime.UtcNow - jumpTimeStart < totalJumpTime)
            {
                jumpTimeStart = DateTime.UtcNow;
                mario.Acceleration = new Vector2(mario.Acceleration.X, mario.Acceleration.Y + jumpAccelerationModifier);
            }
        }
    }
}
