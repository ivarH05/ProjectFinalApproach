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
        public override CollisionData GetCollision(BoxCollider other)
        {
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
