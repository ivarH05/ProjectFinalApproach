using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class LineBumper : Rigidbody
    {
        float triggerTimer = 0;
        public LineBumper(Vec2 start, Vec2 end) : base("", new LineCollider(start, end))
        {
            isTrigger = true;
        }

        public override void OnTrigger(CollisionData collision)
        {
            Rigidbody other = collision.other.rigidbody;

            if (other.isKinematic || triggerTimer > 0)
                return;

            other.velocity = collision.normal * 1000;
            triggerTimer = 0.25f;
            ObjectiveManager.UpdateScore(ScoreType.Score, 500);
        }
    }
}
