﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects
{
    class Floor : SpriteGameObject
    {
        private bool marioHasBeenAbove;

        public Floor(Vector2 position) : base("spr_floor") {
            this.position = position;
            marioHasBeenAbove = false;
        }

        public bool MarioHasBeenAbove { get => marioHasBeenAbove; set => marioHasBeenAbove = value; }
    }
}
