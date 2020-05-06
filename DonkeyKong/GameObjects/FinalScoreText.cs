using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong.GameObjects
{
    class FinalScoreText : TextGameObject
    {

        public FinalScoreText(Vector2 position, string text) : base("ComicSans")
        {
            this.position = position;
            this.Text = text;
        }

    }
}
