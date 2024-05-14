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
    public class SidePanel : Sprite
    {
        public SidePanel() : base("square.png", false)
        {
            alpha = 0f;
        }


        public SidePanel(TiledObject obj = null) : base("square.png", false)
        {
            alpha = 0f;
        }
    }
}