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
        object[] panelChildren;

        public SidePanel() : base("square.png", false)
        {
            alpha = 0f;
        }


        public SidePanel(TiledObject obj = null) : base("square.png", false)
        {
            alpha = 0f;
            coords = new Vec2[]{ new Vec2(obj.X, obj.Y), new Vec2(obj.Width, obj.Height)  };
        }

        void GetPanelChildren()
        {
            // get all children of the panel
            
        }

    }
}