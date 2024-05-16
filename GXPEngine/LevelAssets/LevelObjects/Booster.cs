using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    public class Booster : Rigidbody
    {
        float triggerTimer = 0;
        public Booster(TiledObject obj = null) : base(Level.getPath() + "Booster.png", 2, 1, new BoxCollider(new Vec2(64, 150)))
        {
            isTrigger = true;
            if (obj != null)
            {
                obj.Width = 64;
                obj.Height = 150;
            }
            SetCycle(0, 2, 100);
        }

        void Update()
        {
            triggerTimer -= Time.DeltaSeconds;
            Animate(Time.deltaTime);
        }

        public override void OnTrigger(CollisionData collision)
        {
            Rigidbody other = collision.other.rigidbody;

            if (other.isKinematic)
                return;

            other.velocity += new Vec2(0, -10000 * Time.timeStep);
            if (triggerTimer < 0)
            {
                SoundManager.PlaySound("speedUp");
            }
            triggerTimer = 0.5f;
        }
    }
}
