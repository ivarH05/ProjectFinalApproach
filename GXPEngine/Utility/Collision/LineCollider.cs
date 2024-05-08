using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class LineCollider : Collider
    {
        public Vec2 start;
        public Vec2 end;

        public LineCollider(Vec2 start, Vec2 end)
        {
            this.start = start;
            this.end = end;
            rigidbody.isKinematic = true;
        }

        public override CollisionData GetCollision(CircleCollider other)
        {
            Vec2 p = GetClosestPointOnLine(start, end, other.Position);
            float dist = Vec2.Distance(p, Position);

            if(dist > other.radius)
                return null;

            CollisionData result = new CollisionData
            {
                point = p,
                normal = (end - start).Normal,
                self = this,
                other = other,
                penetrationDepth = dist - other.radius,
                TimeOfImpact = 0
            };

            return result;
        }

        private Vec2 GetClosestPointOnLine(Vec2 v, Vec2 w, Vec2 p)
        {
            float l2 = (v - w).SqrMagnitude;
            if (l2 == 0.0) return v;
            float t = Mathf.Max(0, Mathf.Min(1, Vec2.Dot(p - v, w - v) / l2));
            Vec2 projection = v + t * (w - v);
            return projection;
        }
    }
}
