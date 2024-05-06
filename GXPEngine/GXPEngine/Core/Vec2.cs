using System;

namespace GXPEngine.Core
{
    public struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        override public string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        public static Vector2 zero
        {
            get { return new Vector2(0, 0); }
        }

        public static Vector2 up
        {
            get { return new Vector2(0, -1); }
        }

        public static Vector2 down
        {
            get { return new Vector2(0, 1); }
        }
        public static Vector2 left
        {
            get { return new Vector2(-1, 0); }
        }
        public static Vector2 right
        {
            get { return new Vector2(1, 0); }
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(-a.x, -a.y);
        }

        public static Vector2 operator *(Vector2 a, float scalar)
        {
            return new Vector2(a.x * scalar, a.y * scalar);
        }
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        public static Vector2 operator *(float scalar, Vector2 a)
        {
            return new Vector2(a.x * scalar, a.y * scalar);
        }

        public static Vector2 operator /(Vector2 a, float scalar)
        {
            if (scalar == 0)
            {
                throw new ArgumentException("Cant divide by zero!");
            }
            return new Vector2(a.x / scalar, a.y / scalar);
        }

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.x == right.x && left.y == right.y;
        }

        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2)
            {
                Vector2 other = (Vector2)obj;
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
        /// turn this into a vector2 with magnitude 1
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
        /// get this vector2 with magnitude 1
        /// </summary>
        public Vector2 normalized
        {
            get
            {
                float mag = magnitude;
                if (mag == 0)
                    return new Vector2(0, 0);
                return new Vector2(x / mag, y / mag);
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
        /// get a vector2 with the angle angleDegrees
        /// </summary>
        /// <param name="angleDegrees">the angle in degrees</param>
        /// <returns></returns>
        public static Vector2 GetUnitVectorDeg(float angleDegrees)
        {
            float angleRad = Mathf.Deg2Rad(angleDegrees);
            return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }

        /// <summary>
        /// get a vector2 with the angle in angleradians
        /// </summary>
        /// <param name="angleRadians">the angle in radians</param>
        /// <returns></returns>
        public static Vector2 GetUnitVectorRad(float angleRadians)
        {
            return new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
        }

        /// <summary>
        /// get a random vector with magnitude 1
        /// </summary>
        /// <returns></returns>
        public static Vector2 RandomUnitVector()
        {
            return GetUnitVectorRad(Utils.Random(-1, 1f) * 2 * Mathf.PI);
        }

        /// <summary>
        /// lerp the vector2 start to vector2 end with factor t
        /// </summary>
        /// <param name="start">the start value</param>
        /// <param name="end">the end value</param>
        /// <param name="t">the time [0, 1] between start and end</param>
        /// <returns></returns>
        public static Vector2 Lerp(Vector2 start, Vector2 end, float t)
        {
            return start + (end - start) * Mathf.Clamp01(t);
        }
        /// <summary>
        /// lerp the vector2 start to vector2 end with factor t, unclamped
        /// </summary>
        /// <param name="start">the start value</param>
        /// <param name="end">the end value</param>
        /// <param name="t">the time [0, 1] between start and end</param>
        public static Vector2 LerpUnclamped(Vector2 start, Vector2 end, float t)
        {
            return start + (end - start) * t;
        }

        /// <summary>
        /// get the distance between vector a and vector b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float Distance(Vector2 a, Vector2 b)
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

        public void RotateDegrees(float angleDegrees)
        {
            float angleRad = Mathf.Deg2Rad(angleDegrees);
            float cos = Mathf.Cos(angleRad);
            float sin = Mathf.Sin(angleRad);
            float newX = x * cos - y * sin;
            float newY = x * sin + y * cos;
            x = newX;
            y = newY;
        }

        public void RotateRadians(float angleRadians)
        {
            float cos = Mathf.Cos(angleRadians);
            float sin = Mathf.Sin(angleRadians);
            float newX = x * cos - y * sin;
            float newY = x * sin + y * cos;
            x = newX;
            y = newY;
        }

        public void RotateAroundDegrees(Vector2 point, float angleDegrees)
        {
            this -= point;
            RotateDegrees(angleDegrees);
            this += point;
        }

        public void RotateAroundRadians(Vector2 point, float angleRadians)
        {
            this -= point;
            RotateRadians(angleRadians);
            this += point;
        }

        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        public static float Cross(Vector2 a, Vector2 b)
        {
            return a.x * b.y - a.y * b.x;
        }
        /// <summary>
        /// Reflect the vector2 among a certain normal
        /// </summary>
        /// <param name="Normal">the normal of the "mirror"</param>
        /// <returns></returns>
        public Vector2 Reflect(Vector2 Normal)
        {
            Vector2 In = normalized;
            Vector2 Out = In - 2 * In * Normal * Normal;
            return Out * magnitude;
        }
    }
}

