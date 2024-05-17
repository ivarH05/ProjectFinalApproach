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
        public Ice(TiledObject obj = null) : base(Level.getPath() + "iceBreaking.png", 13, 1, new BoxCollider(new Vec2(64, 64)))
        {
            isTrigger = true;
            if (obj != null)
            {
                obj.Width = 64;
                obj.Height = 64;
            }
            width = 64;
            height = 64;
        }
        float animTime = 0.5f;

        void Update()
        {
            color = (uint)(color + (0xffffff - color) * Time.DeltaSeconds * 10);
            triggerTimer -= Time.DeltaSeconds;
            scale = Mathf.Lerp(scale, 1, Time.DeltaSeconds * 35);
            if(triggerTimer > -100)
            {
                if (triggerTimer < animTime)
                    SetFrame(13 - (int)((triggerTimer / animTime) * 13));
                if(triggerTimer < -0.05)
                    LateDestroy();
            }
        }

        public override void OnTrigger(CollisionData collision)
        {
            Rigidbody other = collision.other.rigidbody;

            if (other.isKinematic || triggerTimer > -100)
                return;

            other.velocity += collision.normal * -1000;
            triggerTimer = animTime;
            ObjectiveManager.UpdateScore(ScoreType.BumperCount, 1);
            ObjectiveManager.UpdateScore(ScoreType.Score, 500);
            SoundManager.PlaySound("wallBump");
        }
    }
}
