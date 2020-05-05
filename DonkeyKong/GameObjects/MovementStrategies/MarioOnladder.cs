using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects.MovementStrategies
{
    class MarioOnLadder : MarioNormalMovement
    {
        private readonly float ladderSpeed = -1200;
        private readonly float leftRightClimbSpeed = 400;

        private readonly float YResistance = 0.8f;

        private readonly Dictionary<Keys, Vector2> MovementDict = new Dictionary<Keys, Vector2>();

        public MarioOnLadder() 
        {
            MovementDict.Add(Keys.Left, new Vector2(-1 * leftRightClimbSpeed, 0));
            MovementDict.Add(Keys.A, new Vector2(-1 * leftRightClimbSpeed, 0));
            MovementDict.Add(Keys.Right, new Vector2(1 * leftRightClimbSpeed, 0));
            MovementDict.Add(Keys.D, new Vector2(1 * leftRightClimbSpeed, 0));
            MovementDict.Add(Keys.Up, new Vector2(0, ladderSpeed));
            MovementDict.Add(Keys.Down, new Vector2(0, -ladderSpeed));
        }

        public override void HandleMovement(Mario mario, InputHelper inputHelper)
        {
            mario.Acceleration = new Vector2();
            mario.Velocity = new Vector2(mario.Velocity.X * leftRightResistance, mario.Velocity.Y * YResistance);
            foreach (KeyValuePair<Keys, Vector2> kvp in MovementDict)
            {
                if (inputHelper.IsKeyDown(kvp.Key))
                {
                    mario.Acceleration += kvp.Value;
                }
            }
        }
    }
}
