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

        BoxCollider collider;
        string spriteLocation;
        int gridIndex;
        bool isStatic = false;

        public PhysicsObject(string spriteLocation = "square.png") : base(spriteLocation, false)
        {
            this.spriteLocation = spriteLocation;
            // Empty
        }

        // make the object draggable 
        void DragObject()
        {
            if (Input.GetMouseButton(0))
            {
                x = Input.mouseX;
                y = Input.mouseY;
            }
        }

        public void SetStatic(bool isStatic)
        {
            this.isStatic = isStatic;
        }

        void Update()
        {
            if (!isStatic) DragObject();
            // Empty
        }
    }

}