using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.LevelAssets.LevelObjects
{
    public class Triangle : Sprite
    {
        public Vec2[] verticies = new Vec2[] { new Vec2(-70, 65), new Vec2(10, -80), new Vec2(10, 20) };
        public Triangle() : base("Assets/Triangle.png", false, false)
        {
            centerOrigin();
            float r = rotation;
            for (int i = 0; i < verticies.Length; i++)
            {
                Vec2 start = verticies[i].RotateAroundDegrees(Position, r);
                Vec2 end = verticies[(i + 1) % verticies.Length].RotateAroundDegrees(Position, r);
                AddChild(new Rigidbody("", new LineCollider(start, end)));
            }
        }

        void Update()
        {

        }
    }
}
