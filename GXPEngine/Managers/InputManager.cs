using GXPEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Managers
{
    public class InputManager 
    {
        int shootKey = Key.SPACE;           // also UI select
        int leftBumper = Key.LEFT;          // also UI left
        int rightBumper = Key.RIGHT;        // also UI right
        bool bothBumpers = false;           // use when both bumpers are pressed

        public InputManager () 
        {
        }


        public void Update()
        {
        }
    }
}