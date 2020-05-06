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
        private DateTime powerupActivated;
        private TimeSpan powerupTime = TimeSpan.FromSeconds(5);
        private bool isActive = false;

        public HammerPowerup(Vector2 position) : base()
        {
            this.position = position;
            LoadAnimation("spr_hammer", "powerup", true, 100);
            LoadAnimation("spr_hammer@8", "active", true, 0.04f);
        }

        public bool IsActive { get => isActive; set => isActive = value; }
        public DateTime PowerupActivated { get => powerupActivated; set => powerupActivated = value; }
        public TimeSpan PowerupTime { get => powerupTime; set => powerupTime = value; }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (isActive)
            {
                if (DateTime.UtcNow - powerupActivated < powerupTime)
                {
                    PlayAnimation("active");
                }
                else {
                    visible = false;
                }
            }
            else
            {
                PlayAnimation("powerup");
            }


        }
    }
}
