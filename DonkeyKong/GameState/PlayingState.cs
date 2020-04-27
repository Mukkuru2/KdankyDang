using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DonkeyKong
{
    class PlayingState : GameObjectList
    {


        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.KeyPressed(Keys.Space))
            {
                GameEnvironment.GameStateManager.SwitchTo("GameOverState");
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        private void SetCollisionMask(SpriteGameObject obj)
        {
            Color[] colorData = new Color[obj.Sprite.Sprite.Width * obj.Sprite.Sprite.Height];
            bool[] tempCollisionMask = new bool[(int)(obj.Sprite.Sprite.Width * obj.Sprite.Sprite.Height * Math.Pow(obj.objScalar, 2))];
            obj.Sprite.Sprite.GetData(colorData);
            for (int i = 0; i < colorData.Length; ++i)
            {
                tempCollisionMask[(int)(i * obj.objScalar)] = colorData[i].A != 0;
            }
            obj.Sprite.CollisionMask = tempCollisionMask;
        }
    }
}