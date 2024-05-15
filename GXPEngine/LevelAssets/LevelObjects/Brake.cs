using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Brake : Rigidbody
    {
        public Brake() : base("Square.png", new BoxCollider(new Vec2(96, 96)))
        {
            isTrigger = true;
        }

        public override void OnTrigger(CollisionData collision)
        {
            Rigidbody other = collision.other.rigidbody;

            if (other.isKinematic)
                return;

            other.velocity = Vec2.Lerp(other.velocity, Vec2.Zero, Time.timeStep);
        }
    }
}
