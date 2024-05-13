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
        public Vec2 _start
        {
            get
            {
                return start.RotateDegrees(rotation) + Position;
            }
        }
        public Vec2 _end
        {
            get
            {
                return end.RotateDegrees(rotation) + Position;
            }
        }
        public Vec2 start;
        public Vec2 end;

        public LineCollider(Vec2 start, Vec2 end)
        {
            this.start = start;
            this.end = end;
        }

        public override CollisionData GetCollision(CircleCollider other)
        {
            Vec2 p = GetClosestPointOnLine(other.Position);
            float dist = Vec2.Distance(p, other.Position);

            Vec2 line = (_end - _start);


            if (dist > other.radius)
                return null;

            Vec2 normal = line.Normal;
            if (Vec2.Dot(normal, other.Position - p) > 0)
                normal *= -1;

            CollisionData result = new CollisionData
            {
                point = p,
                normal = normal,
                self = this,
                other = other,
                penetrationDepth = dist - other.radius,
                TimeOfImpact = 0
            };

            return result;
        }

        public override CollisionData GetCollision(BoxCollider other)
        {

            return CollisionData.flip(other.GetCollision(this));
        }

        public override CollisionData PredictCollision(CircleCollider other)
        {
            Vec2 nextPosition = Position + Velocity * Time.timeStep;
            Vec2 nextOtherPosition = other.Position + other.Velocity * Time.timeStep;

            Vec2 p = GetClosestPointOnLine(nextOtherPosition);
            float dist = Vec2.Distance(p, other.Position);

            Vec2 line = (_end - _start);


            if (dist > other.radius)
                return null;

            Vec2 normal = line.Normal;
            if (Vec2.Dot(normal, other.Position - p) > 0)
                normal *= -1;

            CollisionData result = new CollisionData
            {
                point = p,
                normal = normal,
                self = this,
                other = other,
                penetrationDepth = dist - other.radius,
                TimeOfImpact = 0
            };

            return result;
        }
        public override CollisionData PredictCollision(BoxCollider other)
        {
            return CollisionData.flip(other.PredictCollision(this));

        }

        public Vec2 GetClosestPointOnLine(Vec2 p)
        {
            Vec2 v = _start;
            Vec2 w = _end;
            float l2 = (v - w).SqrMagnitude;
            if (l2 == 0.0) return v;
            float t = Mathf.Max(0, Mathf.Min(1, Vec2.Dot(p - v, w - v) / l2));
            Vec2 projection = v + t * (w - v);
            return projection;
        }

        public override void Draw()
        {
            base.Draw();
            PhysicsManager.debugCanvas.Line(_start.x, _start.y, _end.x, _end.y);
        }
    }
}
