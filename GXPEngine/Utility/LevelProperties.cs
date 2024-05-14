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
        }


        public LevelProperties(TiledObject obj=null) : base("square.png", false)
        {
            Console.WriteLine("LevelProperties created");
            alpha = 0f;
        }
    }    
}