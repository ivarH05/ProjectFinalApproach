using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    public class Wall : Rigidbody
    {
        float triggerTimer = 0;
        public Wall(TiledObject obj = null) : base("Square.png", new BoxCollider(new Vec2(32, 196)), false)
        {
            if (obj != null && obj.Width > obj.Height)
            {
                collider = new BoxCollider(new Vec2(196, 32));
                width = 192;
                height = 32;
            }
            else
            {
                width = 32;
                height = 192;
            }
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
            SoundManager.PlaySound("wallBump");

            if (other.isKinematic || triggerTimer > 0)
                return;
            color = 0xffeeff;
        }
    }
}
