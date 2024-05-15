using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Booster : Rigidbody
    {
        public Booster() : base("Square.png", new BoxCollider(new Vec2(64, 150)))
        {
            isTrigger = true;
        }

        public override void OnTrigger(CollisionData collision)
        {
            Rigidbody other = collision.other.rigidbody;

            if (other.isKinematic)
                return;

            other.velocity += new Vec2(0, -2000 * Time.timeStep);
        }
    }
}
