using GXPEngine.Core;
using GXPEngine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class PhysicsObject : Sprite
    {
        public PhysicsObject() : base("square.png", false)
        {
            // Empty
        }

        // a drag and drop function 
        void DragObject()
        {
            if (Input.GetMouseButton(0))
            {
                x = Input.mouseX;
                y = Input.mouseY;
            }
        }

        void Update()
        {
            DragObject();
            // Empty
        }
    }

}