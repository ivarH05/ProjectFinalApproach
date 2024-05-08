using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class CollisionData
    {
        /// <summary>
        /// The point of impact of the collision
        /// </summary>
        public Vec2 point;
        /// <summary>
        /// The normal of the collision, this will be relative to object 2 A.K.A. other. 
        /// </summary>
        public Vec2 normal;

        /// <summary>
        /// The self object of the collision, object 1. 
        /// </summary>
        public Collider self;
        /// <summary>
        /// the other object of the collision, object 2
        /// </summary>
        public Collider other;

        /// <summary>
        /// The depth the object was at the time of FINDING the collision, not at the time of impact. 
        /// </summary>
        public float penetrationDepth;
        /// <summary>
        /// the time of the impact between colliders
        /// </summary>
        public float TimeOfImpact;

        public override string ToString()
        {
            return "Point of impact:   " + point +
                "\nNormal:            " + normal +
                "\nPenetration depth: " + penetrationDepth +
                "\nTime of impact:    " + TimeOfImpact;
        }
    }
}
