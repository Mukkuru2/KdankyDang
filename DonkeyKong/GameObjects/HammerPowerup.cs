using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects
{
    class HammerPowerup : AnimatedGameObject
    {
        private bool isActive = false;

        public HammerPowerup(Vector2 position) : base()
        {
            this.position = position;
            LoadAnimation("spr_hammer", "powerup", true, 100);
            LoadAnimation("spr_hammer@6", "active", true, 0.06f);
        }

        public bool IsActive { get => isActive; set => isActive = value; }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (isActive)
            {
                PlayAnimation("active");
            }
            else
            {
                PlayAnimation("powerup");
            }


        }
    }
}
