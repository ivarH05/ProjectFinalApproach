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
        public Wall(TiledObject obj = null) : base(Level.getPath() + ((obj.Rotation == 90 || obj.Width > obj.Height) ? "WallH.png" : "WallV.png"), new BoxCollider(new Vec2(32, 196)), false)
        {
            if (obj.Rotation == 90)
            {
                SetCollider(new BoxCollider(new Vec2(196, 32)));
                obj.Rotation = 0;
                obj.Width = 196;
                obj.Height = 32;
                obj.X -= 196;
            }
            else if(obj.Width > obj.Height)
            {
                SetCollider(new BoxCollider(new Vec2(196, 32)));
                obj.Rotation = 0;
                obj.Width = 196;
                obj.Height = 32;
            }
            else
            {
                obj.Height = 196;
                obj.Width = 32;
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
            if(other.velocity.magnitude > 100)
                SoundManager.PlaySound("wallBump");

            if (other.isKinematic || triggerTimer > 0)
                return;
            //color = 0xffeeff;
        }
    }
}
