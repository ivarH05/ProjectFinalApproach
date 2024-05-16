using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Player : Rigidbody
    {
        float timer = 0;
        public Player() : base("assets/Ball.png", new CircleCollider(16))
        {

        }

        void Update()
        {
            timer -= Time.DeltaSeconds;
        }

        public override void OnCollision(CollisionData collision)
        {
            if(timer < 0 && collision.other is LineCollider && velocity.magnitude > 100)
            {
                SoundManager.PlaySound("wallBump");
                timer = 0.1f;
            }
        }
    }
}
