using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Core;

namespace GXPEngine
{
    public class CircleCollider : Collider
    {
        public float radius = 16;

        public CircleCollider(float radius)
        {
            this.radius = radius;
        }

        public override CollisionData GetCollision(BoxCollider other)
        {
            return null;
        }
        public override CollisionData GetCollision(CircleCollider other)
        {
            Vec2 position = (rigidbody.Position + center);
            Vec2 otherPosition = other.rigidbody.Position + other.center;
            Vec2 relPos = otherPosition - position;

            float distance = relPos.magnitude;
            float minDistance = radius + other.radius;

            if (distance > minDistance)
                return null;

            CollisionData result = new CollisionData()
            {
                point = position + otherPosition / 2,
                normal = relPos.normalized,
                self = this,
                other = other,
                penetrationDepth = minDistance - distance,
                TimeOfImpact = 0
            };

            return result;
        }

        public override CollisionData PredictCollision(BoxCollider other)
        {
            return null;
        }

        public override CollisionData PredictCollision(CircleCollider other)
        {
            Vec2 relPos = other.Position - Position;
            Vec2 relVel = other.Velocity - Velocity;

            float distance = relPos.magnitude;
            float minDistance = radius + other.radius;

            float a = Vec2.Dot(relVel, relVel);
            float b = 2 * Vec2.Dot(relVel, relPos);
            float c = Vec2.Dot(relPos, relPos) - minDistance * minDistance;

            if ((c < 0 && a <= 0) || a == 0)
                return null;

            float d = b * b - 4 * a * c;
            if (d < 0)
                return null;

            float TimeOfImpact = (-b - Mathf.Sqrt(d)) / (2 * a);
            if (TimeOfImpact > Time.timeStep || (TimeOfImpact < 0 && minDistance < distance))
                return null;

            Vec2 point = TimeOfImpact * Velocity;

            CollisionData result = new CollisionData()
            {
                point = point,
                normal = (other.Position - other.Velocity * TimeOfImpact - point).normalized,
                self = this,
                other = other,
                penetrationDepth = minDistance - distance,
                TimeOfImpact = TimeOfImpact
            };

            return result;
        }
        public override CollisionData IsOverlapping(Vec2 point)
        {
            return null;
        }
    }
}
