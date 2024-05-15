using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    public class Bumper : Rigidbody
    {
        float triggerTimer = 0;
        public Bumper(TiledObject obj = null) : base("Circle.png", new CircleCollider(32))
        {
            isTrigger = true;
        }

        void Update()
        {
            color = (uint)(color + (0xffffff - color) * Time.DeltaSeconds * 10);
            triggerTimer -= Time.DeltaSeconds;
            scale = Mathf.Lerp(scale, 1, Time.DeltaSeconds * 35);
        }

        public override void OnTrigger(CollisionData collision)
        {
            Rigidbody other = collision.other.rigidbody;

            if (other.isKinematic || triggerTimer > 0)
                return;

            scale = 2f;
            color = 0xFF0000;
            other.velocity = collision.normal * 1000;
            triggerTimer = 0.25f;
            ObjectiveManager.UpdateScore(ScoreType.BumperCount, 1);
            ObjectiveManager.UpdateScore(ScoreType.Score, 500);
        }
    }
}
