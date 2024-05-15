using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.LevelAssets.LevelObjects
{
    public class Wall : Rigidbody
    {
        float triggerTimer = 0;
        public Wall() : base("Square.png", new BoxCollider(new Vec2(32, 196)))
        {
            isKinematic = true;
        }

        void Update()
        {
            color = (uint)(color + (0xffffff - color) * Time.DeltaSeconds * 10);
            triggerTimer -= Time.DeltaSeconds;
        }

        public override void OnCollision(CollisionData collision)
        {
            Rigidbody other = collision.other.rigidbody;

            if (other.isKinematic || triggerTimer > 0)
                return;
            color = 0xffeeff;
        }
    }
}
