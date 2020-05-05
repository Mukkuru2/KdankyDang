using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects
{
    class Pauline : SpriteGameObject
    {
        public Pauline(Vector2 position) : base("spr_pauline")
        {
            Origin = new Vector2(Center.X, sprite.Height);
            this.position = position;
        }
    }
}
