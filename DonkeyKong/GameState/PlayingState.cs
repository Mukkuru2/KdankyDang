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
        GameObjectList ladders;

        public PlayingState()
        {
        }

        public override void Reset()
        {
            floors = new GameObjectList();
            this.Add(floors);

            ladders = new GameObjectList();

            mario = new Mario();
            this.Add(mario);

            int floorVerticalStartOffset = 300;
            int floorTextureWidth = 96;
            int floorSetsHeightDifference = 210;
            int floorIndividualOffset = 3;
            int totalFloors = 18;

            int ladderHeight = 24;
            int ladderVerticalStartOffset = 20;
            for (int iFloors = 0; iFloors < 4; iFloors++)
            {
                for (int jFloors = 0; jFloors < totalFloors; jFloors++)
                {
                    //create floors from right to left or left to right alternating
                    //floorSetsHeightDifference gives the difference in height between the first floor of two sets
                    //floorIndividualOffset gives the height each floor in a set goes down the next iteration
                    if (iFloors % 2 == 0)
                    {
                        floors.Add(new Floor(new Vector2(
                            jFloors * floorTextureWidth,
                            floorVerticalStartOffset + iFloors * floorSetsHeightDifference + jFloors * floorIndividualOffset)));

                        if (jFloors == totalFloors - 1)
                        {
                            for (int iLadder = 0; iLadder < 5; iLadder++)
                            {
                                ladders.Add(new Ladder(new Vector2(
                                    jFloors * floorTextureWidth,
                                    floorVerticalStartOffset + iFloors * floorSetsHeightDifference + jFloors * floorIndividualOffset)));
                            }
                        }
                    }
                    else
                    {
                        floors.Add(new Floor(new Vector2(
                            GameEnvironment.Screen.X - floorTextureWidth - jFloors * floorTextureWidth,
                            floorVerticalStartOffset + iFloors * floorSetsHeightDifference + jFloors * floorIndividualOffset)));

                        if (jFloors == totalFloors - 1)
                        {
                            for (int iLadder = 0; iLadder < 5; iLadder++)
                            {


                            }
                        }
                    }

                }
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
                if (mario.Position.Y + mario.Sprite.Height - 10 <= floor.Position.Y)
                {
                    floor.MarioHasBeenAbove = true;
                }
                else
                {
                    floor.MarioHasBeenAbove = false;
                }

                if (floor.CollidesWith(mario)
                    && floor.MarioHasBeenAbove == true
                    && mario.Velocity.Y >= 0)
                {
                    mario.Velocity = new Vector2(mario.Velocity.X, 0);
                    mario.Accelleration = new Vector2(mario.Accelleration.X, 0);
                    mario.OnPlatform = true;
                    mario.Position = new Vector2(mario.Position.X, -mario.Sprite.Height + floor.Position.Y + 1);
                    break;
                }
                else
                {
                    mario.OnPlatform = false;
                }
            }
        }
    }
}