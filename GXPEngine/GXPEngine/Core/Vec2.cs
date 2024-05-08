using System;

namespace GXPEngine.Core
{
    public struct Vec2
    {
        public float x;
        public float y;

        public Vec2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        override public string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        public static Vec2 zero
        {
            get { return new Vec2(0, 0); }
        }

        public static Vec2 up
        {
            get { return new Vec2(0, -1); }
        }

        public static Vec2 down
        {
            get { return new Vec2(0, 1); }
        }
        public static Vec2 left
        {
            get { return new Vec2(-1, 0); }
        }
        public static Vec2 right
        {
            get { return new Vec2(1, 0); }
        }

        public static Vec2 operator +(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x + b.x, a.y + b.y);
        }

        public static Vec2 operator -(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x - b.x, a.y - b.y);
        }

        public static Vec2 operator -(Vec2 a)
        {
            return new Vec2(-a.x, -a.y);
        }

        public static Vec2 operator *(Vec2 a, float scalar)
        {
            return new Vec2(a.x * scalar, a.y * scalar);
        }
        public static Vec2 operator *(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x * b.x, a.y * b.y);
        }

        public static Vec2 operator *(float scalar, Vec2 a)
        {
            return new Vec2(a.x * scalar, a.y * scalar);
        }

        public static Vec2 operator /(Vec2 a, float scalar)
        {
            if (scalar == 0)
            {
                throw new ArgumentException("Cant divide by zero!");
            }
            return new Vec2(a.x / scalar, a.y / scalar);
        }

        public static bool operator ==(Vec2 left, Vec2 right)
        {
            return left.x == right.x && left.y == right.y;
        }

        public static bool operator !=(Vec2 left, Vec2 right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Vec2)
            {
                Vec2 other = (Vec2)obj;
                return this == other;
            }
            return false;
        }

        /// <summary>
        /// the magnitude (length) of a vector
        /// </summary>
        public float magnitude
        {
            get
            {
                return Mathf.Sqrt(x * x + y * y);
            }
            set
            {
                float mag = magnitude;
                if (mag == 0)
                    return;
                this = this / magnitude * value;
            }
        }

        /// <summary>
        /// turn this into a Vec2 with magnitude 1
        /// </summary>
        public void Normalize()
        {
            float mag = magnitude;
            if (mag != 0)
            {
                x /= mag;
                y /= mag;
            }
        }

        /// <summary>
        /// get this Vec2 with magnitude 1
        /// </summary>
        public Vec2 normalized
        {
            get
            {
                float mag = magnitude;
                if (mag == 0)
                    return new Vec2(0, 0);
                return new Vec2(x / mag, y / mag);
            }
        }

        public Vec2 Normal
        {
            get
            {
                return new Vec2(-y, x).normalized;
            }
        }

        /// <summary>
        /// set the x and y coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetXY(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// get a Vec2 with the angle angleDegrees
        /// </summary>
        /// <param name="angleDegrees">the angle in degrees</param>
        /// <returns></returns>
        public static Vec2 GetUnitVectorDeg(float angleDegrees)
        {
            float angleRad = Mathf.Deg2Rad(angleDegrees);
            return new Vec2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }

        /// <summary>
        /// get a Vec2 with the angle in angleradians
        /// </summary>
        /// <param name="angleRadians">the angle in radians</param>
        /// <returns></returns>
        public static Vec2 GetUnitVectorRad(float angleRadians)
        {
            return new Vec2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
        }

        /// <summary>
        /// get a random vector with magnitude 1
        /// </summary>
        /// <returns></returns>
        public static Vec2 RandomUnitVector()
        {
            return GetUnitVectorRad(Utils.Random(-1, 1f) * 2 * Mathf.PI);
        }

        /// <summary>
        /// lerp the Vec2 start to Vec2 end with factor t
        /// </summary>
        /// <param name="start">the start value</param>
        /// <param name="end">the end value</param>
        /// <param name="t">the time [0, 1] between start and end</param>
        /// <returns></returns>
        public static Vec2 Lerp(Vec2 start, Vec2 end, float t)
        {
            return start + (end - start) * Mathf.Clamp01(t);
        }
        /// <summary>
        /// lerp the Vec2 start to Vec2 end with factor t, unclamped
        /// </summary>
        /// <param name="start">the start value</param>
        /// <param name="end">the end value</param>
        /// <param name="t">the time [0, 1] between start and end</param>
        public static Vec2 LerpUnclamped(Vec2 start, Vec2 end, float t)
        {
            return start + (end - start) * t;
        }

        /// <summary>
        /// get the distance between vector a and vector b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float Distance(Vec2 a, Vec2 b)
        {
            return (b - a).magnitude;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="angleDegrees"></param>
        public void SetAngleDegrees(float angleDegrees)
        {
            float angleRad = Mathf.Deg2Rad(angleDegrees);
            float mag = magnitude;
            x = Mathf.Cos(angleRad) * mag;
            y = Mathf.Sin(angleRad) * mag;
        }

        public void SetAngleRadians(float angleRadians)
        {
            float mag = magnitude;
            x = Mathf.Cos(angleRadians) * mag;
            y = Mathf.Sin(angleRadians) * mag;
        }

        public float GetAngleRadians()
        {
            return Mathf.Atan2(y, x);
        }

        public float GetAngleDegrees()
        {
            return Mathf.Rad2Deg(GetAngleRadians());
        }

        public Vec2 RotateDegrees(float angleDegrees)
        {
            float angleRad = Mathf.Deg2Rad(angleDegrees);
            float cos = Mathf.Cos(angleRad);
            float sin = Mathf.Sin(angleRad);
            float newX = x * cos - y * sin;
            float newY = x * sin + y * cos;
            return new Vec2(newX, newY);
        }

        public Vec2 RotateRadians(float angleRadians)
        {
            float cos = Mathf.Cos(angleRadians);
            float sin = Mathf.Sin(angleRadians);
            float newX = x * cos - y * sin;
            float newY = x * sin + y * cos;
            return new Vec2(newX, newY);
        }

        public void RotateAroundDegrees(Vec2 point, float angleDegrees)
        {
            this -= point;
            RotateDegrees(angleDegrees);
            this += point;
        }

        public void RotateAroundRadians(Vec2 point, float angleRadians)
        {
            this -= point;
            RotateRadians(angleRadians);
            this += point;
        }

        public static float Dot(Vec2 a, Vec2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        public static float Cross(Vec2 a, Vec2 b)
        {
            return a.x * b.y - a.y * b.x;
        }
        /// <summary>
        /// Reflect the Vec2 among a certain normal
        /// </summary>
        /// <param name="Normal">the normal of the "mirror"</param>
        /// <returns></returns>
        public Vec2 Reflect(Vec2 Normal)
        {
            Vec2 In = normalized;
            Vec2 Out = In - 2 * In * Normal * Normal;
            return Out * magnitude;
        }
    }
}

