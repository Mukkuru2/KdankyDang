using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects
{
    class KdankeyDang : SpriteGameObject
    {
        public KdankeyDang(Vector2 position) : base("spr_kdanky_dang")
        {
            this.position = position;
        }
    }
}
