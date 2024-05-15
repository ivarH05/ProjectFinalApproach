using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    public class Triangle : Sprite
    {
        public Vec2[] verticies = new Vec2[] { new Vec2(10, 20), new Vec2(10, -80), new Vec2(-70, 65) };
        float triggerTimer = 0;

        Rigidbody[] edges = new Rigidbody[3];
        //public Vec2[] verticies = new Vec2[] { new Vec2(-70, 65), new Vec2(10, -80), new Vec2(10, 20) };
        public Triangle(TiledObject obj = null) : base("Assets/Triangle.png", false, false)
        {
            centerOrigin();
            float r = rotation;
            for (int i = 0; i < verticies.Length; i++)
            {
                Vec2 start = verticies[i].RotateAroundDegrees(Position, r);
                Vec2 end = verticies[(i + 1) % verticies.Length].RotateAroundDegrees(Position, r);
                Rigidbody a = (Rigidbody)AddChild(new Rigidbody("", new LineCollider(start, end), true));
                edges[i] = a;
            }
        }

        void Update()
        {
            foreach (Rigidbody edge in edges)
            {
                foreach (CollisionData collision in edge.GetCollisions())
                {
                    Rigidbody other = collision.other.rigidbody;

                    if (other.isKinematic || triggerTimer > 0)
                        break;

                    scale = 2f;
                    color = 0xFF0000;
                    other.velocity = collision.normal * 1000;
                    triggerTimer = 0.25f;
                    ObjectiveManager.UpdateScore(ScoreType.BumperCount, 1);
                    ObjectiveManager.UpdateScore(ScoreType.Score, 500);
                    SoundManager.PlaySound("triangleBump");
                }
                edge.ClearCollisions();
            }


            color = (uint)(color + (0xffffff - color) * Time.DeltaSeconds * 10);
            triggerTimer -= Time.DeltaSeconds;
            scale = Mathf.Lerp(scale, 1, Time.DeltaSeconds * 35);
        }
    }
}
