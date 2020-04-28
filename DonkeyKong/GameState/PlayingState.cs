using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonkeyKong.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DonkeyKong
{
    class PlayingState : GameObjectList
    {
        Mario mario;
        GameObjectList floors;

        public PlayingState()
        {
            mario = new Mario();
            this.Add(mario);

            floors = new GameObjectList();
            this.Add(floors);

            for (int iFloors = 0; iFloors < 10; iFloors++)
            {
                floors.Add(new Floor(new Vector2(iFloors * 200, 1000)));
            }
        }

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

            foreach (Floor floor in floors.Children)
            {
                if (floor.CollidesWith(mario))
                {
                    mario.Velocity = new Vector2(0, 0);
                    mario.Accelleration = new Vector2(0, 0);
                    mario.OnPlatform = true;
                    mario.Position = new Vector2(mario.Position.X, - mario.Sprite.Height + floor.Position.Y);
                    break;
                }
                else { 
                    mario.OnPlatform = false;
                }
            }
        }

        private void SetCollisionMask(SpriteGameObject obj)
        {
            Color[] colorData = new Color[obj.Sprite.Sprite.Width * obj.Sprite.Sprite.Height];
            bool[] tempCollisionMask = new bool[(int)(obj.Sprite.Sprite.Width * obj.Sprite.Sprite.Height * Math.Pow(obj.SizeScalar, 2))];
            obj.Sprite.Sprite.GetData(colorData);
            for (int i = 0; i < colorData.Length; ++i)
            {
                tempCollisionMask[(int)(i * obj.SizeScalar)] = colorData[i].A != 0;
            }
            obj.Sprite.CollisionMask = tempCollisionMask;
        }
    }
}