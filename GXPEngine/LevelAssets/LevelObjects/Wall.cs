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
            obj.Height = 196;
            obj.Width = 32;
            if (obj.Rotation == 90)
            {
                SetCollider(new BoxCollider(new Vec2(196, 32)));
                obj.Rotation = 0;
                obj.Width = 196;
                obj.Height = 32;
                obj.X -= 196;
            }
            isKinematic = true;
        }
        public Wall(bool sideways = false) : base("Square.png", new BoxCollider(new Vec2(32, 196)), false, false, false)
        {
            isKinematic = true;
            SetOrigin(width, 0);
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
