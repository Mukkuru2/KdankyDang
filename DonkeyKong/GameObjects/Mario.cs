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
        private bool onPlatform;
        private readonly Dictionary<Keys, Vector2> AccelerationDict = new Dictionary<Keys, Vector2>();
        private readonly float leftRightAccelerationModifier = 10000;
        private readonly float jumpAccelerationModifier = 15000;
        private readonly float leftRightResistance = 0.9f;
        private readonly float fallResistance = 0.98f;
        private float gravity = 3000;
        private DateTime jumpTimeStart;
        private TimeSpan totalJumpTime = TimeSpan.FromMilliseconds(100);

        public bool OnPlatform { get => onPlatform; set => onPlatform = value; }

        public Mario() : base("spr_mario")
        {
            this.position = new Vector2(200, 200);
            AccelerationDict.Add(Keys.Left, new Vector2(-1 * leftRightAccelerationModifier, 0));
            AccelerationDict.Add(Keys.A, new Vector2(-1 * leftRightAccelerationModifier, 0));
            AccelerationDict.Add(Keys.Right, new Vector2(1 * leftRightAccelerationModifier, 0));
            AccelerationDict.Add(Keys.D, new Vector2(1 * leftRightAccelerationModifier, 0));
        }

        public override void Update(GameTime gameTime)
        {
            velocity.X *= leftRightResistance;
            velocity.Y *= fallResistance;

            base.Update(gameTime);

            acceleration.X = 0;
            acceleration.Y = 0;

            if (!onPlatform)
            {
                acceleration.Y = gravity;
            }
            else {
                acceleration.Y = 0;
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            foreach (KeyValuePair<Keys, Vector2> kvp in AccelerationDict)
            {
                if (inputHelper.IsKeyDown(kvp.Key))
                {

                    acceleration += kvp.Value;
                }
            }
            if (inputHelper.IsKeyDown(Keys.Up) && (onPlatform || DateTime.UtcNow - jumpTimeStart < totalJumpTime)) {
                if (onPlatform == true) {
                    jumpTimeStart = DateTime.UtcNow;
                }
                acceleration.Y -= jumpAccelerationModifier;
            }

        }
    }
}
