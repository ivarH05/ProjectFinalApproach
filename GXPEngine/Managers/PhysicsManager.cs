using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public static class PhysicsManager
    {
        /// <summary>
        /// A list of all Rigidbodies that should be taken into account
        /// </summary>
        private static List<Rigidbody> _bodies = new List<Rigidbody>();
        private static float updateTimer = 0;

        /// <summary>
        /// update all physics including collision. 
        /// </summary>
        public static void PhysicsUpdate()
        {
            updateTimer += Time.DeltaSeconds;
            while (updateTimer > Time.timeStep)
            {
                updateTimer -= Time.timeStep;
                foreach(Rigidbody b in _bodies)
                    b.PhysicsUpdate();
            }
        }

        /// <summary>
        /// check if there is any object overlapping with that coordinate. 
        /// </summary>
        /// <param name="point"> the coordinate to check </param>
        /// <returns>null if no overlapping objects are found, otherwise the output collisiondata</returns>
        public static CollisionData IsOverlapping(Vec2 point)
        {
            foreach(Rigidbody b in _bodies)
            {
                CollisionData dat = b.collider.IsOverlapping(point);
                if (dat != null) 
                    return dat;
            }
            return null;
        }

        /// <summary>
        /// add a new rigidbody to be accounted for
        /// </summary>
        /// <param name="body"></param>
        public static void AddBody(Rigidbody body)
        {
            _bodies.Add(body);
        }

        /// <summary>
        /// remove the rigidbody, it will no longer be updated nor taken into account for collisions.
        /// </summary>
        /// <param name="body"></param>
        public static void RemoveBody(Rigidbody body)
        {
            _bodies.Remove(body);
        }
    }
}
