﻿using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public override CollisionData GetCollision(BoxCollider other)
        {
            Vec2[] corners1 = GetRotatedCorners(Position, _halfSize, rotation);
            Vec2[] corners2 = GetRotatedCorners(other.Position, other._halfSize, other.rotation);
            Vec2[] axes = new Vec2[4];

            axes[0] = (corners1[0] - corners1[1]).Normal;
            axes[1] = (corners1[1] - corners1[2]).Normal;
            axes[2] = (corners2[0] - corners2[1]).Normal;
            axes[3] = (corners2[1] - corners2[2]).Normal;

            float penetrationDepth = float.MaxValue;

            for (int i = 0; i < axes.Length; i++)
            {
                float overlap = IsOverlapOnAxis(axes[i], corners1, corners2);
                if (overlap == float.MaxValue)
                    return null;
                else if (overlap < penetrationDepth)
                {
                    penetrationDepth = overlap;
                }
            }

            Vec2 normal = (other.Position - Position).RotateDegrees(-other.rotation);
            normal = (Mathf.Abs(normal.x) > Mathf.Abs(normal.y) ? -new Vec2(normal.x, 0) : -new Vec2(0, normal.y)).RotateDegrees(other.rotation).normalized;

            Vec2 PointOfImpact = (GetClosestPointOnSquare(corners2, Position) + GetClosestPointOnSquare(corners1, other.Position)) / 2;

            CollisionData result = new CollisionData
            {
                point = PointOfImpact,
                normal = normal,
                self = this,
                other = other,
                penetrationDepth = penetrationDepth,
                TimeOfImpact = 0
            };

            return result;
        }

        public override CollisionData GetCollision(CircleCollider other)
        {
            Vec2[] corners1 = GetRotatedCorners(Position, _halfSize, rotation);
            Vec2 PointOfImpact = GetClosestPointOnSquare(corners1, other.Position);

            Vec2 normal = (other.Position - PointOfImpact);
            float dist = normal.magnitude;

            if (dist > other.radius)
                return null;

            normal = (Mathf.Abs(normal.x) > Mathf.Abs(normal.y) ? -new Vec2(normal.x, 0) : -new Vec2(0, normal.y)).RotateDegrees(rotation).normalized;

            return new CollisionData
            {
                point = PointOfImpact,
                normal = normal,
                self = this,
                other = other,
                penetrationDepth = dist - other.radius,
                TimeOfImpact = 0
            };
        }

        public override CollisionData GetCollision(LineCollider other)
        {
            Vec2[] corners1 = GetRotatedCorners(Position, _halfSize, rotation);
            Vec2[] corners2 = new Vec2[] {other._start, other._end };
            Vec2[] axes = new Vec2[3];

            axes[0] = (corners1[1] - corners1[0]).Normal;
            axes[1] = (corners1[2] - corners1[1]).Normal;
            axes[2] = (corners2[1] - corners2[0]).Normal;

            for (int i = 0; i < axes.Length; i++)
            {
                float overlap = IsOverlapOnAxis(axes[i], corners1, corners2);
                if (overlap == float.MaxValue)
                    return null;
            }
            Vec2 linePoint = other.GetClosestPointOnLine(Position);
            Vec2 rectPoint = GetClosestPointOnSquare(corners1, linePoint);
            float penetrationDepth = (rectPoint - linePoint).magnitude;


            Vec2 PointOfImpact = linePoint + rectPoint / 2;

            Vec2 normal = (other._start - other._end).Normal;
            if (Vec2.Dot(normal, Position - other.Position) < 0)
                normal *= -1;

            CollisionData result = new CollisionData
            {
                point = PointOfImpact,
                normal = normal,
                self = this,
                other = other,
                penetrationDepth = penetrationDepth,
                TimeOfImpact = 0
            };
            return result;
        }

        public override CollisionData PredictCollision(BoxCollider other)
        {
            Vec2 nextPosition = Position + Velocity * Time.timeStep;
            Vec2 nextOtherPosition = other.Position + other.Velocity * Time.timeStep;

            Vec2[] corners1 = GetRotatedCorners(nextPosition, _halfSize, rotation + rigidbody.angularVelocity * Time.timeStep);
            Vec2[] corners2 = GetRotatedCorners(nextOtherPosition, other._halfSize, other.rotation + other.rigidbody.angularVelocity * Time.timeStep);
            Vec2[] axes = new Vec2[4];

            axes[0] = (corners1[0] - corners1[1]).Normal;
            axes[1] = (corners1[1] - corners1[2]).Normal;
            axes[2] = (corners2[0] - corners2[1]).Normal;
            axes[3] = (corners2[1] - corners2[2]).Normal;

            float penetrationDepth = float.MaxValue;

            for (int i = 0; i < axes.Length; i++)
            {
                float overlap = IsOverlapOnAxis(axes[i], corners1, corners2);
                if (overlap == float.MaxValue)
                    return null;
                else if (overlap < penetrationDepth)
                {
                    penetrationDepth = overlap;
                }
            }


            Vec2 normal = (other.Position - Position).RotateDegrees(-other.rotation);
            normal = (Mathf.Abs(normal.x) > Mathf.Abs(normal.y) ? -new Vec2(normal.x, 0) : -new Vec2(0, normal.y)).RotateDegrees(other.rotation).normalized;

            Vec2 PointOfImpact = (GetClosestPointOnSquare(corners2, Position) + GetClosestPointOnSquare(corners1, other.Position)) / 2;
            float TimeOfImpact = (Vec2.Distance(PointOfImpact, other.Position) / (other.Velocity - Velocity).magnitude);

            CollisionData result = new CollisionData
            {
                point = PointOfImpact,
                normal = normal,
                self = this,
                other = other,
                penetrationDepth = penetrationDepth,
                TimeOfImpact = TimeOfImpact
            };
            return result;
        }

        public override CollisionData PredictCollision(CircleCollider other)
        {
            Vec2 nextPosition = Position + Velocity * Time.timeStep;
            Vec2 nextOtherPosition = other.Position + other.Velocity * Time.timeStep;

            Vec2[] corners1 = GetRotatedCorners(nextPosition, _halfSize, rotation + rigidbody.angularVelocity * Time.timeStep);
            Vec2 PointOfImpact = GetClosestPointOnSquare(corners1, nextOtherPosition);

            float dist = Vec2.Distance(PointOfImpact, nextOtherPosition);

            if (dist > other.radius)
                return null;

            Vec2 normal = (other.Position - Position).RotateDegrees(rotation);
            normal = (Mathf.Abs(normal.x) > Mathf.Abs(normal.y) ? -new Vec2(normal.x, 0) : -new Vec2(0, normal.y)).RotateDegrees(rotation).normalized;

            float TimeOfImpact = (Vec2.Distance(PointOfImpact, other.Position) - other.radius) / (Velocity - other.Velocity).magnitude;

            return new CollisionData
            {
                point = PointOfImpact,
                normal = normal,
                self = this,
                other = other,
                penetrationDepth = other.radius - dist,
                TimeOfImpact = TimeOfImpact
            };
        }

        public override CollisionData PredictCollision(LineCollider other)
        {
            Vec2 nextPosition = Position + Velocity * Time.timeStep;
            Vec2 nextOtherPosition = other.Position + other.Velocity * Time.timeStep;
            Vec2[] corners1 = GetRotatedCorners(nextPosition, _halfSize, rotation + rigidbody.angularVelocity * Time.timeStep);
            Vec2[] corners2 = new Vec2[] { other.start + nextOtherPosition, other.end + nextOtherPosition };
            Vec2[] axes = new Vec2[3];

            axes[0] = (corners1[1] - corners1[0]).Normal;
            axes[1] = (corners1[2] - corners1[1]).Normal;
            axes[2] = (corners2[1] - corners2[0]).Normal;

            for (int i = 0; i < axes.Length; i++) //FYI tomorrow ivar, you dont calculate the penetration depth because the line has a depth of 0. 
            {
                float overlap = IsOverlapOnAxis(axes[i], corners1, corners2);
                if (overlap == float.MaxValue)
                    return null;
            }
            Vec2 linePoint = other.GetClosestPointOnLine(Position);
            Vec2 rectPoint = GetClosestPointOnSquare(corners1, linePoint);
            float penetrationDepth = (rectPoint - linePoint).magnitude;


            Vec2 PointOfImpact = linePoint + rectPoint / 2;

            Vec2 normal = (other._start - other._end).Normal;
            if (Vec2.Dot(normal, Position - other.Position) < 0)
                normal *= -1;

            CollisionData result = new CollisionData
            {
                point = PointOfImpact,
                normal = normal,
                self = this,
                other = other,
                penetrationDepth = penetrationDepth,
                TimeOfImpact = 0
            };
            return result;
        }

        public override CollisionData IsOverlapping(Vec2 point)
        {
            return null;
        }
        private Vec2 GetClosestPointOnSquare(Vec2[] corners, Vec2 point)
        {
            Vec2 closestPoint = Vec2.Zero;
            float minDistance = float.MaxValue;
            for (int i = 0; i < corners.Length; i++)
            {
                Vec2 c1 = corners[i];
                Vec2 c2 = corners[(i + 1) % corners.Length];
                Vec2 p = GetClosestPointOnLine(c1, c2, point);
                float dist = Vec2.Distance(p, point);
                if (dist < minDistance)
                {
                    closestPoint = p;
                    minDistance = dist;
                }
            }
            return closestPoint;
        }

        private Vec2 GetClosestPointOnLine(Vec2 v, Vec2 w, Vec2 p)
        {
            float l2 = (v - w).SqrMagnitude;
            if (l2 == 0.0) return v;
            float t = Mathf.Max(0, Mathf.Min(1, Vec2.Dot(p - v, w - v) / l2));
            Vec2 projection = v + t * (w - v);
            return projection;
        }


        private Vec2[] GetRotatedCorners(Vec2 position, Vec2 halfSize, float rotation)
        {
            Vec2[] corners = new Vec2[4];

            float sinTheta = Mathf.Sin(Mathf.Deg2Rad(rotation));
            float cosTheta = Mathf.Cos(Mathf.Deg2Rad(rotation));

            Vec2[] localCorners = new Vec2[]
            {
            new Vec2(-halfSize.x, -halfSize.y),
            new Vec2(halfSize.x, -halfSize.y),
            new Vec2(halfSize.x, halfSize.y),
            new Vec2(-halfSize.x, halfSize.y)
            };

            for (int i = 0; i < 4; i++)
            {
                corners[i] = new Vec2(
                    localCorners[i].x * cosTheta - localCorners[i].y * sinTheta + position.x,
                    localCorners[i].x * sinTheta + localCorners[i].y * cosTheta + position.y
                );
            }

            return corners;
        }

        private float IsOverlapOnAxis(Vec2 axis, Vec2[] corners1, Vec2[] corners2)
        {
            float[] projections1 = ProjectOnAxis(axis, corners1);
            float[] projections2 = ProjectOnAxis(axis, corners2);

            if (projections1[1] >= projections2[0] && projections2[1] >= projections1[0])
                return Math.Min(projections1[1], projections2[1]) - Math.Max(projections1[0], projections2[0]);
            else
                return float.MaxValue;
        }

        private float[] ProjectOnAxis(Vec2 axis, Vec2[] corners)
        {
            float min = Vec2.Dot(corners[0], axis);
            float max = min;

            for (int i = 1; i < corners.Length; i++)
            {
                float projection = Vec2.Dot(corners[i], axis);
                if (projection < min)
                    min = projection;
                else if (projection > max)
                    max = projection;
            }

            return new float[] { min, max };
        }

        public override void Draw()
        {
            PhysicsManager.debugCanvas.Rect(Position.x, Position.y, size.x, size.y);
            base.Draw();
        }
    }
}
