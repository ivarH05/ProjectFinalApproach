using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Collider
    {
        /// <summary>
        /// The offset of the collider
        /// </summary>
        public Vec2 Center;

        /// <summary>
        /// get the collision info if the collider type is unknown. 
        /// </summary>
        /// <param name="other">any type of collider</param>
        /// <returns>returns null if no collision found, otherwise the collision data</returns>
        /// <exception cref="Exception"></exception>
        public CollisionData GetCollision(Collider other)
        {
            if (other is BoxCollider)
                return GetCollision((BoxCollider)other);
            if (other is CircleCollider)
                return GetCollision((CircleCollider)other);

            throw new Exception("Collider type not known");
        }

        public virtual CollisionData GetCollision(BoxCollider other) { return null; }

        public virtual CollisionData GetCollision(CircleCollider other) { return null; }

        public virtual CollisionData IsOverlapping(Vec2 point) { return null; }
    }
}
