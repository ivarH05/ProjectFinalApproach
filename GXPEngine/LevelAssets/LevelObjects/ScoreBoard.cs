using GXPEngine.Core;
using GXPEngine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using TiledMapParser;

namespace GXPEngine
{
    public class ScoreBoard : Sprite
    {
        public static Vec2[] coords;

        public ScoreBoard() : base("square.png", false)
        {
            alpha = 0f;
        }

        public ScoreBoard(TiledObject obj = null) : base("square.png", false)
        {
            alpha = 0f;
            coords = new Vec2[]{ new Vec2(obj.X, obj.Y), new Vec2(obj.Width, obj.Height)  };
        }
    }
}