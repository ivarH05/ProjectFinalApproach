using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Background : GameObject
    {
        //if you want transform point to be relative to world position instead of screen position, uncomment this
        /*
        public override Vec2 TransformPoint(float x, float y)
        {
            return new Vec2(x, y);
        }
        */
    }
}
