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
    public class LevelProperties : Sprite
    {

        public LevelProperties() :base("square.png", false)
        {
            alpha = 0f;
            Scene.levelProperties = this;
        }


        public LevelProperties(TiledObject obj=null) : base("square.png", false)
        {
            alpha = 0f;
            Scene.levelProperties = this;
        }
    }    
}