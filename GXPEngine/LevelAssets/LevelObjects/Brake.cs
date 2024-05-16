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
    public class Brake : Rigidbody
    {
        float triggerTimer = 0;
        public Brake(TiledObject obj = null) : base(Level.getPath() + "Brake.png", new BoxCollider(new Vec2(96, 64)))
        {
            isTrigger = true;
            if(obj != null )
            {
                obj.Width =200;
                obj.Height = 150000;
            }
            width = 200;
            height = 150;
        }

        void Update()
        {
            triggerTimer -= Time.DeltaSeconds;
        }

        public override void OnTrigger(CollisionData collision)
        {
            Rigidbody other = collision.other.rigidbody;

            if (other.isKinematic)
                return;

            other.velocity = Vec2.Lerp(other.velocity, new Vec2(0, -245), Time.timeStep * 4);
            if (triggerTimer < 0)
            {
                SoundManager.PlaySound("speedDown");
            }
            triggerTimer = 0.5f;
        }
    }
}
