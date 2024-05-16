using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    public class Ice : Rigidbody
    {
        float triggerTimer = -100;
        public Ice(TiledObject obj = null) : base("Square.png", new BoxCollider(new Vec2(64, 64)))
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
            other.velocity = collision.normal * -1000;
            triggerTimer = 0.25f;
            ObjectiveManager.UpdateScore(ScoreType.BumperCount, 1);
            ObjectiveManager.UpdateScore(ScoreType.Score, 500);
            SoundManager.PlaySound("wallBump");
        }
    }
}
