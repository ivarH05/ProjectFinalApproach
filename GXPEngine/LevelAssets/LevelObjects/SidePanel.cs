using GXPEngine.Core;
using GXPEngine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using TiledMapParser;
using System.Dynamic;

namespace GXPEngine
{
    public class SidePanel : Sprite
    {
        public static Vec2[] coords;

        //<GridIndex, ObjectCount>
        Dictionary<int, int> gridIndex = new Dictionary<int, int>()
        {
            { 1, 0 },
            { 2, 0 },
            { 3, 0 },
            { 4, 0 },
            { 5, 0 },
            { 6, 0 },
        };

        public SidePanel() : base("square.png", false)
        {
            alpha = 0f;
        }

        public SidePanel(TiledObject obj = null) : base("square.png", false)
        {
            alpha = 0f;
            coords = new Vec2[]{ new Vec2(obj.X, obj.Y), new Vec2(obj.Width, obj.Height)  };
            
            for ( int i = 0; i < obj.GetIntProperty("GridSize"); i++ )
            {
                Console.WriteLine("GridProp "+(i+1)+": "+obj.GetIntProperty("Object_"+i) );
                gridIndex[i+1] = obj.GetIntProperty("Object_"+i);
            }
        }

        PhysicsObject GeneratePhysicsObject()
        {
            return new PhysicsObject("square.png", new Vec2(x, y));
        }
    }
}