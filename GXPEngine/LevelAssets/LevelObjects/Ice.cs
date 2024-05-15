using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.LevelAssets.LevelObjects
{
    public class Ice : Rigidbody
    {
        float triggerTimer = -100;
        public Ice() : base("Circle.png", new CircleCollider(32))
        {
            isTrigger = true;
        }

        void Update()
        {
            color = (uint)(color + (0xffffff - color) * Time.DeltaSeconds * 10);
            triggerTimer -= Time.DeltaSeconds;
            scale = Mathf.Lerp(scale, 1, Time.DeltaSeconds * 35);
            if(triggerTimer > -100 && triggerTimer < -0.35)
            {
                LateDestroy();
            }
        }

        public override void OnTrigger(CollisionData collision)
        {
            Rigidbody other = collision.other.rigidbody;

            if (other.isKinematic || triggerTimer > -100)
                return;

            scale = 2f;
            color = 0x00FF00;
            other.velocity = collision.normal * 1000;
            triggerTimer = 0.25f;
        }
    }
}
