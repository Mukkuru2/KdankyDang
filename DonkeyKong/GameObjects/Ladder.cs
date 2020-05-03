using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects
{
    class Ladder : SpriteGameObject
    {
        public Ladder(Vector2 position) : base("spr_ladder_piece")
        {
            this.position = position;
        }
    }
}
