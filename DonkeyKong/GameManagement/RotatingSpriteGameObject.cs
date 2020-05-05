using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DonkeyKong
{
    /// <summary>
    /// SpriteGameObject that handles rotated sprites. Overrides Draw method WITHOUT call to base.Draw.
    /// </summary>
    class RotatingSpriteGameObject : SpriteGameObject
    {
        protected GameObject targetObject;
        protected float offsetDegrees;

        private float radians;
        /// <summary>
        /// RotatingSpriteGameObject constructor.
        /// </summary>
        /// <param name="assetname">Assetname in Content project</param>
        /// <param name="layer">Layer for drawing order</param>
        /// <param name="id">Reference for finding object in a list.</param>
        /// <param name="sheetIndex">If asset is a spritesheet only use a certain frame.</param>
        public RotatingSpriteGameObject(string assetname) :
            base(assetname)
        {
        }

        /// <summary>
        /// Returns unit vector2 (length = 1) based on current angle.
        /// </summary>
        public Vector2 AngularDirection
        {
            get
            {
                // calculate angular direction based on sprite angle 
                return new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));
            }
            set
            {
                Angle = (float)Math.Atan2(value.Y, value.X);
            }
        }

        /// <summary>
        /// Returns / sets angle in radians (0 - 2*PI)
        /// </summary>
        public float Angle
        {
            get { return radians; }
            set { radians = value; }
        }

        /// <summary>
        /// Returns / sets angle in degrees (0 - 360)
        /// </summary>
        public float Degrees
        {
            get { return MathHelper.ToDegrees(Angle); }
            set { Angle = MathHelper.ToRadians(value); }
        }

        /// <summary>
        /// Draws sprite on angle.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!visible || sprite == null)
                return;

            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Mirror)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            spriteBatch.Draw(sprite.Sprite, GlobalPosition, null, Color.White, radians - MathHelper.ToRadians(offsetDegrees), Origin, sizeScalar, spriteEffects, 0);
        }

        /// <summary>
        /// Sets the target object so this object will always be pointing towards the target object
        /// </summary>
        /// <param name="targetObject">GameObject to look at</param>
        /// <param name="offsetDegrees">degrees to offset from calculated angle</param>
        public void LookAt(GameObject targetObject, float offsetDegrees = 0)
        {
            this.targetObject = targetObject;
            this.offsetDegrees = offsetDegrees;
        }

        /// <summary>
        /// Clears the targetObject so it won't point towards it anymore
        /// </summary>
        public void StopLookingAtTarget() {
            targetObject = null;
            this.offsetDegrees = 0;
        }
        
        /// <summary>
        /// Updates the angle based on the position and the position of the target object
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (targetObject != null)
            {
                Vector2 targetVector = targetObject.GlobalPosition - GlobalPosition;              
                AngularDirection = targetVector;
            }

            base.Update(gameTime);
        
        
        }

        public override Rectangle BoundingBox
        {
            get
            {
                int left = (int)(GlobalPosition.X - origin.X);
                int top = (int)(GlobalPosition.Y - origin.Y);
                return new Rectangle(left, top, (int)(Width * sizeScalar), (int)(Height * sizeScalar));
            }
        }

        public bool CollidesWith(RotatingSpriteGameObject obj)
        {
            if (!visible || !obj.visible || !BoundingBox.Intersects(obj.BoundingBox))
            {
                return false;
            }
            if (!PerPixelCollisionDetection)
            {
                return true;
            }
            Rectangle b = Collision.Intersection(BoundingBox, obj.BoundingBox);
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    int thisx = b.X - (int)(GlobalPosition.X - origin.X) + x;
                    int thisy = b.Y - (int)(GlobalPosition.Y - origin.Y) + y;
                    int objx = b.X - (int)(obj.GlobalPosition.X - obj.origin.X) + x;
                    int objy = b.Y - (int)(obj.GlobalPosition.Y - obj.origin.Y) + y;
                    if (sprite.IsTranslucent(thisx, thisy) && obj.sprite.IsTranslucent(objx, objy))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
