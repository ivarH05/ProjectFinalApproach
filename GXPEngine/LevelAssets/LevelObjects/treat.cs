using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    public class Treat : Rigidbody
    {
        float triggerTimer = -100;
        public Treat(TiledObject obj = null) : base(Level.getPath() + "Treat.png", new CircleCollider(32))
        {
            isTrigger = true;
            if (obj != null)
            {
                obj.Width = 64;
                obj.Height = 64;
            }
        }

        void Update()
        {
            color = (uint)(color + (0xffffff - color) * Time.DeltaSeconds * 10);
            triggerTimer -= Time.DeltaSeconds;
            scale = Mathf.Lerp(scale, 1 + Mathf.Sin(Time.time / 100f) / 6, Time.DeltaSeconds * 35);
            if (triggerTimer > -100 && triggerTimer < -0.35)
            {
                LateDestroy();
            }
        }

        public override void OnTrigger(CollisionData collision)
        {
            Rigidbody other = collision.other.rigidbody;

            if (other.isKinematic || triggerTimer > -100)
                return;

            color = 0x0000ff;
            triggerTimer = 0.25f;
            ObjectiveManager.UpdateScore(ScoreType.TreatsCount, 1);
        }
    }
}
