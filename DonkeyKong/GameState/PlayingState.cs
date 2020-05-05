using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DonkeyKong.GameObjects;
using DonkeyKong.GameObjects.MovementStrategies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DonkeyKong
{
    class PlayingState : GameObjectList
    {
        Mario mario;
        Pauline pauline;
        KdankeyDang kdankyDang;
        GameObjectList floors;
        GameObjectList ladders;
        GameObjectList barrels;

        private Random random = new Random();

        private readonly int floorVerticalStartOffset = 300;
        private readonly int floorSetsHeightDifference = 225;
        private readonly int floorIndividualOffset = 4;
        private readonly int floorsPerLayer = 18;
        private readonly int floorLayers = 4;

        private Dictionary<Ladder, bool> ladderDict = new Dictionary<Ladder, bool>();

        private readonly int ladderVerticalStartOffset = 5;
        private readonly int baseLadderAmount = 5;

        //the variables for the miscelaneous ladders
        private readonly int ladderAmountMisc1 = 3;
        private readonly int ladderFloorMisc1 = 12;
        private readonly int ladderFloorSetMisc1 = 1;

        private readonly int ladderAmountMisc2 = 5;
        private readonly int ladderFloorMisc2 = 3;
        private readonly int ladderFloorSetMisc2 = 0;

        private readonly int ladderAmountMisc3 = 2;
        private readonly int ladderFloorMisc3 = 6;
        private readonly int ladderFloorSetMisc3 = 2;

        private readonly int ladderAmountMisc4 = 2;
        private readonly int ladderFloorMisc4 = 13;
        private readonly int ladderFloorSetMisc4 = 0;

        private readonly int paulineMaxLadders = 6;

        //distance Mario has to be from center of ladder to 
        private readonly int ladderDistanceTrigger = 20;

        //distance the barrels have to be from mario to go offscreen
        private readonly int BarrelOffScreenDifference = 300;
        //distance objects have to go offscreen to despawn;
        private readonly int despawnArea = 10;


        public PlayingState()
        {
        }

        public override void Reset()
        {
            children.Clear();

            floors = new GameObjectList();
            this.Add(floors);

            ladders = new GameObjectList();
            this.Add(ladders);

            mario = new Mario(new Vector2(100, 900));
            this.Add(mario);

            pauline = new Pauline(new Vector2(800, 150));
            this.Add(pauline);

            barrels = new GameObjectList();
            this.Add(barrels);


            SpriteSheet floorSpriteDimensions = new SpriteSheet("spr_floor");
            SpriteSheet ladderSpriteDimensions = new SpriteSheet("spr_ladder_piece");

            for (int iFloors = 0; iFloors < floorLayers; iFloors++)
            {
                for (int jFloors = 0; jFloors < floorsPerLayer; jFloors++)
                {
                    //add Kdanky Dang at first 2 floors
                    if (iFloors == 0 && jFloors == 1)
                    {
                        kdankyDang = new KdankeyDang(new Vector2(jFloors * floorSpriteDimensions.Width,
                          floorVerticalStartOffset + iFloors * floorSetsHeightDifference + jFloors * floorIndividualOffset));
                        kdankyDang.Origin = new Vector2(kdankyDang.Center.X, kdankyDang.Sprite.Height);
                        this.Add(kdankyDang);
                    }

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
                            GameObjectList _ladders = new GameObjectList();
                            for (int iLadder = 0; iLadder < baseLadderAmount; iLadder++)
                            {
                                _ladders.Add(new Ladder(new Vector2(
                                    jFloors * floorSpriteDimensions.Width
                                    - ladderSpriteDimensions.Width / 2,
                                    ladderVerticalStartOffset + iLadder * ladderSpriteDimensions.Sprite.Height +
                                    floorVerticalStartOffset + iFloors * floorSetsHeightDifference + jFloors * floorIndividualOffset)));
                            }
                            ladders.Add(_ladders);
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
                        if (jFloors == floorsPerLayer - 1)
                        {
                            GameObjectList _ladders = new GameObjectList();
                            for (int iLadder = 0; iLadder < baseLadderAmount; iLadder++)
                            {
                                _ladders.Add(new Ladder(new Vector2(
                                    GameEnvironment.Screen.X - floorSpriteDimensions.Width - (jFloors - 1) * floorSpriteDimensions.Width
                                    - ladderSpriteDimensions.Width / 2,
                                    ladderVerticalStartOffset + iLadder * ladderSpriteDimensions.Sprite.Height +
                                    floorVerticalStartOffset + iFloors * floorSetsHeightDifference + jFloors * floorIndividualOffset)));
                            }
                            ladders.Add(_ladders);
                        }
                    }

                    //add misc ladders
                    if (jFloors == ladderFloorMisc1 && iFloors == ladderFloorSetMisc1)
                    {
                        GameObjectList _ladders = new GameObjectList();
                        for (int iLadder = 0; iLadder < ladderAmountMisc1; iLadder++)
                        {
                            _ladders.Add(new Ladder(new Vector2(
                                    GameEnvironment.Screen.X - floorSpriteDimensions.Width - (jFloors - 1) * floorSpriteDimensions.Width
                                    - ladderSpriteDimensions.Width / 2,
                                    ladderVerticalStartOffset + iLadder * ladderSpriteDimensions.Sprite.Height +
                                    floorVerticalStartOffset + iFloors * floorSetsHeightDifference + jFloors * floorIndividualOffset)));
                        }
                        ladders.Add(_ladders);
                    }

                    if (jFloors == ladderFloorMisc2 && iFloors == ladderFloorSetMisc2)
                    {
                        GameObjectList _ladders = new GameObjectList();
                        for (int iLadder = 0; iLadder < ladderAmountMisc2; iLadder++)
                        {
                            _ladders.Add(new Ladder(new Vector2(
                                    jFloors * floorSpriteDimensions.Width
                                    - ladderSpriteDimensions.Width / 2,
                                    ladderVerticalStartOffset + iLadder * ladderSpriteDimensions.Sprite.Height +
                                    floorVerticalStartOffset + iFloors * floorSetsHeightDifference + jFloors * floorIndividualOffset)));
                        }
                        ladders.Add(_ladders);
                    }

                    if (jFloors == ladderFloorMisc3 && iFloors == ladderFloorSetMisc3)
                    {
                        GameObjectList _ladders = new GameObjectList();
                        for (int iLadder = 0; iLadder < ladderAmountMisc3; iLadder++)
                        {
                            _ladders.Add(new Ladder(new Vector2(
                                    jFloors * floorSpriteDimensions.Width
                                    - ladderSpriteDimensions.Width / 2,
                                    ladderVerticalStartOffset + iLadder * ladderSpriteDimensions.Sprite.Height +
                                    floorVerticalStartOffset + iFloors * floorSetsHeightDifference + jFloors * floorIndividualOffset)));
                        }
                        ladders.Add(_ladders);
                    }

                    if (jFloors == ladderFloorMisc4 && iFloors == ladderFloorSetMisc4)
                    {
                        GameObjectList _ladders = new GameObjectList();
                        for (int iLadder = 0; iLadder < ladderAmountMisc4; iLadder++)
                        {
                            _ladders.Add(new Ladder(new Vector2(
                                    jFloors * floorSpriteDimensions.Width
                                    - ladderSpriteDimensions.Width / 2,
                                    ladderVerticalStartOffset + iLadder * ladderSpriteDimensions.Sprite.Height +
                                    floorVerticalStartOffset + iFloors * floorSetsHeightDifference + jFloors * floorIndividualOffset)));
                        }
                        ladders.Add(_ladders);
                    }

                }
            }

            //Add misc platforms

            //Pauline platforms
            floors.Add(new Floor(new Vector2(pauline.Position.X, pauline.Position.Y)));
            floors.Add(new Floor(new Vector2(pauline.Position.X - floorSpriteDimensions.Width, pauline.Position.Y)));

            //pauline ladders

            GameObjectList paulineLadders1 = new GameObjectList(0, "1");
            GameObjectList paulineLadders2 = new GameObjectList(0, "1");

            for (int iLadder = 0; iLadder < paulineMaxLadders; iLadder++)
            {
                paulineLadders1.Add(new Ladder(new Vector2(pauline.Position.X + floorSpriteDimensions.Width - ladderSpriteDimensions.Width,
                    pauline.Position.Y + ladderVerticalStartOffset + iLadder * ladderSpriteDimensions.Height)));
                paulineLadders2.Add(new Ladder(new Vector2(pauline.Position.X - floorSpriteDimensions.Width,
                    pauline.Position.Y + ladderVerticalStartOffset + iLadder * ladderSpriteDimensions.Height)));
            }

            ladders.Add(paulineLadders1);
            ladders.Add(paulineLadders2);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (mario.CollidesWith(pauline))
            {
                GameEnvironment.GameStateManager.SwitchTo("WinState");
            }

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

                //floor with mario collision
                if (floor.CollidesWith(mario)
                    && floor.MarioHasBeenAbove == true
                    && mario.Velocity.Y >= 0)
                {
                    mario.Velocity = new Vector2(mario.Velocity.X, 0);
                    mario.Acceleration = new Vector2(mario.Acceleration.X, 0);
                    mario.MovementStrategy = new MarioOnFloor();
                    mario.Position = new Vector2(mario.Position.X, -mario.Sprite.Height + floor.Position.Y + 1);
                    break;
                }
                //reset movementstrategy if mario's movementstrategy is still "OnFloor"
                else if (!floor.CollidesWith(mario)
                    && mario.MovementStrategy.GetType() == typeof(MarioOnFloor))
                {
                    mario.MovementStrategy = new MarioNormalMovement();
                }
            }



            bool breakflag = false;
            foreach (GameObjectList ladderList in ladders.Children)
            {
                foreach (Ladder ladder in ladderList.Children)
                {
                    //ladder with mario collision
                    if (ladder.CollidesWith(mario) && Math.Abs((ladder.Position.X + ladder.Center.X) - (mario.Position.X + mario.Center.X)) < ladderDistanceTrigger)
                    {
                        mario.MovementStrategy = new MarioOnLadder();
                        breakflag = true;
                        break;

                    }
                    else if (mario.MovementStrategy.GetType() == typeof(MarioOnLadder))
                    {
                        mario.MovementStrategy = new MarioNormalMovement();
                    }
                }
                if (breakflag)
                    break;
            }


            foreach (Barrel barrel in barrels.Children)
            {

                //floor collision
                foreach (Floor floor in floors.Children)
                {
                    if (floor.CollidesWith(barrel)
                        && barrel.Velocity.Y >= 0
                        && barrel.MovementStrategy.GetType() != typeof(BarrelGoingDown))
                    {
                        barrel.Velocity = new Vector2(barrel.Velocity.X, -barrel.Velocity.Y);
                        if (barrel.MovementStrategy.GetType() != typeof(BarrelOnFloor) && Math.Abs(barrel.Velocity.Y) <= 50)
                        {
                            barrel.Velocity = new Vector2(barrel.Velocity.X, 0);
                            barrel.MovementStrategy = new BarrelOnFloor(barrel.Velocity);
                        }
                        break;

                    }

                    if (!floor.CollidesWith(barrel)
                        && barrel.MovementStrategy.GetType() == typeof(BarrelOnFloor))
                    {
                        barrel.MovementStrategy = new BarrelNormalMovement(barrel.Velocity);
                    }

                }

                //Bounce off walls

                if ((barrel.Position.X - barrel.Sprite.Width / 2.0f < 0 && barrel.Velocity.X <= 0 && barrel.Acceleration.X <= 0) ||
                    (barrel.Position.X + barrel.Sprite.Width / 2.0f > GameEnvironment.Screen.X && barrel.Velocity.X >= 0 && barrel.Acceleration.X >= 0))
                {
                    if (!(barrel.Position.Y - BarrelOffScreenDifference >= mario.Position.Y))
                    {
                        barrel.Velocity = new Vector2(-barrel.Velocity.X, barrel.Velocity.Y);
                        barrel.Acceleration = new Vector2(-barrel.Acceleration.X, barrel.Acceleration.Y);
                    }
                }

                //remove if outside of screen
                if (barrel.Position.X + barrel.Center.X < -despawnArea || barrel.Position.X - barrel.Center.X > GameEnvironment.Screen.X + despawnArea)
                {
                    this.Remove(barrel);
                }

                //Barrels have a chance to go down the stairs
                barrel.Position = new Vector2(barrel.Position.X, barrel.Position.Y + barrel.Sprite.Height);
                Dictionary<Ladder, bool> _ladderDict = new Dictionary<Ladder, bool>(barrel.LadderDict);
                foreach (KeyValuePair<Ladder, bool> kvp in barrel.LadderDict)
                {
                    if (barrel.CollidesWith(kvp.Key) 
                        && kvp.Value != true
                        && Math.Abs((barrel.Position.X) - (kvp.Key.Position.X + kvp.Key.Center.X)) < 5)
                    {
                        _ladderDict.Remove(kvp.Key);
                        _ladderDict.Add(kvp.Key, true);
                        if (random.NextDouble() < 0.2) {
                            barrel.MovementStrategy = new BarrelGoingDown(barrel.Velocity);
                        }
                    }
                }
                barrel.LadderDict = _ladderDict;
                barrel.Position = new Vector2(barrel.Position.X, barrel.Position.Y - barrel.Sprite.Height);

                //Mario loses when he touches a barrel
                if (barrel.CollidesWith(mario))
                {
                    GameEnvironment.GameStateManager.SwitchTo("GameOverState");
                }
            }

            //Donkey kong keeps throwing barrels
            if (random.NextDouble() <= 0.015)
            {
                //create ladderDict
                ladderDict = new Dictionary<Ladder, bool>();
                foreach (GameObjectList ladderList in ladders.Children)
                {
                    if (ladderList.Id != "1")
                    {
                        ladderDict.Add((Ladder)ladderList.Children[0], false);
                    }
                }

                barrels.Add(new Barrel(
                    new Vector2(kdankyDang.Position.X, kdankyDang.Position.Y - 20),
                    Barrel.BarrelStartVelocity,
                    ladderDict));
            }

        }
    }
}