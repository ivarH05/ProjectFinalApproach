using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class BoxCollider : Collider
    {
        public Vec2 size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
                _halfSize = value / 2;
            }
        }
        private Vec2 _size;
        private Vec2 _halfSize;

        public BoxCollider(Vec2 size)
        {
            this.size = size;
        }

        //future me, you need to calculate the correct top left and bottom right corners. 
        public override CollisionData GetCollision(BoxCollider other)
        {
            Vec2 offsetPos = _halfSize.RotateDegrees(rotation %45);
            Vec2 BL1 = Position - offsetPos;
            Vec2 TR1 = Position + offsetPos;

            Vec2 otherOffsetPos = other._halfSize.RotateDegrees(other.rotation % 45);
            Vec2 BL2 = other.Position - otherOffsetPos;
            Vec2 TR2 = other.Position + otherOffsetPos;

            if (BL1.x <= TR2.x && TR1.x >= BL2.x &&
                BL1.y <= TR2.y && TR1.y >= BL2.y)
            {
                return new CollisionData();
            }
            return null;
        }
        public override CollisionData GetCollision(CircleCollider other)
        {
            return null;
        }
        public override CollisionData IsOverlapping(Vec2 point)
        {
            return null;
        }
    }
}
