using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects
{
    class ScoreText : TextGameObject
    {

        private DateTime textActivated;
        private TimeSpan textDisplayTime = TimeSpan.FromSeconds(1);
        private const int scoreIncrease = 500;

        public static int ScoreIncrease => scoreIncrease;

        public ScoreText(Vector2 position) : base("ComicSans")
        {
            this.Position = position;
            Text = scoreIncrease.ToString();
            textActivated = DateTime.UtcNow;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!(DateTime.UtcNow - textActivated < textDisplayTime))
            {
                visible = false;
            }
        }
    }
}
