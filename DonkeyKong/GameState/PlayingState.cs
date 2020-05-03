using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonkeyKong.GameObjects;
using DonkeyKong.GameObjects.MarioMovementStrategy;
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
            this.Add(ladders);

            mario = new Mario(new Vector2(100, 900));
            this.Add(mario);


            SpriteSheet floorSpriteDimensions = new SpriteSheet("spr_floor");
            SpriteSheet ladderSpriteDimensions = new SpriteSheet("spr_ladder_piece");

            int floorVerticalStartOffset = 300;
            int floorSetsHeightDifference = 225;
            int floorIndividualOffset = 4;
            int floorsPerLayer = 18;
            int floorLayers = 4;

            int ladderVerticalStartOffset = 10;
            int baseLadderAmount = 5;

            ladders.Add(new Ladder(new Vector2(10, 10)));

            for (int iFloors = 0; iFloors < floorLayers; iFloors++)
            {
                for (int jFloors = 0; jFloors < floorsPerLayer; jFloors++)
                {
                    //create floors from right to left or left to right alternating
                    //floorSetsHeightDifference gives the difference in height between the first floor of two sets
                    //floorIndividualOffset gives the height each floor in a set goes down the next iteration
                    if (iFloors % 2 == 0) //right to left walk direction for these platforms
                    {
                        floors.Add(new Floor(new Vector2(
                          jFloors * floorSpriteDimensions.Width,
                          floorVerticalStartOffset + iFloors * floorSetsHeightDifference + jFloors * floorIndividualOffset)));

                        //add ladders to edges
                        if (jFloors == floorsPerLayer - 1)
                        {
                            for (int iLadder = 0; iLadder < baseLadderAmount; iLadder++)
                            {
                                ladders.Add(new Ladder(new Vector2(
                                    jFloors * floorSpriteDimensions.Width
                                    - ladderSpriteDimensions.Width / 2,
                                    ladderVerticalStartOffset + iLadder * ladderSpriteDimensions.Sprite.Height +
                                    floorVerticalStartOffset + iFloors * floorSetsHeightDifference + jFloors * floorIndividualOffset)));
                            }
                        }
                    }
                    else //left to right walkdirection for these platforms
                    {
                        //only spawn if the bottom layer is above the bottom of the screen. This is only present here because
                        //the bottom layer will always go left to right by design
                        if (floorVerticalStartOffset + iFloors * floorSetsHeightDifference + (jFloors + 1) * floorIndividualOffset + 48
                           < GameEnvironment.Screen.Y)
                        {
                            floors.Add(new Floor(new Vector2(
                            GameEnvironment.Screen.X - floorSpriteDimensions.Width - jFloors * floorSpriteDimensions.Width,
                            floorVerticalStartOffset + iFloors * floorSetsHeightDifference + jFloors * floorIndividualOffset)));
                        }
                        else
                        {
                            int kFloors = 0;
                            while (GameEnvironment.Screen.X - floorSpriteDimensions.Width - (jFloors + kFloors) * floorSpriteDimensions.Width > -floorSpriteDimensions.Width)
                            {
                                floors.Add(new Floor(new Vector2(
                                    GameEnvironment.Screen.X - floorSpriteDimensions.Width - (jFloors + kFloors) * floorSpriteDimensions.Width,
                                    floorVerticalStartOffset + iFloors * floorSetsHeightDifference + jFloors * floorIndividualOffset)));
                                kFloors++;
                            }
                            break;
                        }

                        //add ladders to edges
                        if (jFloors == floorsPerLayer - 2)
                        {
                            for (int iLadder = 0; iLadder < baseLadderAmount; iLadder++)
                            {
                                ladders.Add(new Ladder(new Vector2(
                                    GameEnvironment.Screen.X - floorSpriteDimensions.Width - jFloors * floorSpriteDimensions.Width
                                    - ladderSpriteDimensions.Width / 2,
                                    ladderVerticalStartOffset + iLadder * ladderSpriteDimensions.Sprite.Height +
                                    floorVerticalStartOffset + iFloors * floorSetsHeightDifference + jFloors * floorIndividualOffset)));
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
                    mario.MovementStrategy = new OnFloor();
                    mario.Position = new Vector2(mario.Position.X, -mario.Sprite.Height + floor.Position.Y + 1);
                    break;
                }
                else if (mario.MovementStrategy.GetType() == typeof(OnFloor))
                {
                    mario.MovementStrategy = new NormalMovement();
                }
            }

            foreach (Ladder ladder in ladders.Children)
            {
                if (ladder.CollidesWith(mario))
                {
                    mario.MovementStrategy = new OnLadder();
                    break;
                }
                if (mario.MovementStrategy.GetType() == typeof(OnLadder))
                {
                    mario.MovementStrategy = new NormalMovement();
                }
            }
        }
    }
}