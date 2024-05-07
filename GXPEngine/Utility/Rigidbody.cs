using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Utility
{
    public class Rigidbody : Sprite
    {
        public Vec2 Velocity;

        public bool isKinematic = false;
        public bool useGravity = true;

        public float mass;
        public float friction;
        public float bounciness;

        public Rigidbody(string spritePath = "Square.png") : base(spritePath, false, false)
        {

        }

        public void PhysicsUpdate()
        {
            if (isKinematic)
                return;

            if (useGravity)
                Velocity += new Vec2(0, 98.1f) * Time.timeStep;
        }
    }
}
