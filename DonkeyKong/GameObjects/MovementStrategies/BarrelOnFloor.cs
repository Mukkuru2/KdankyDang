﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects.MovementStrategies
{
    class BarrelOnFloor : BarrelNormalMovement
    {
        public BarrelOnFloor(Vector2 velocity) : base(velocity)
        {
        }

        public override void HandleMovement(Barrel barrel)
        {
            onFloor = true;
            base.HandleMovement(barrel);
        }
    }
}
