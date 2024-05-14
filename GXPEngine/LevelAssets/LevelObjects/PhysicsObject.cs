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
    public class PhysicsObject : Sprite
    {

        BoxCollider collider;
        string spriteLocation;
        bool isStatic = false;
        bool isDragging = false;
        string objectName;
        Vec2 originalPosition;
        bool isOriginalPosition;
        bool isValidPlacement = false;

        public PhysicsObject(string spriteLocation = "square.png") : base(spriteLocation, false)
        {
            this.spriteLocation = spriteLocation;
        }

        public PhysicsObject(TiledObject obj=null, string spriteLocation = "square.png") : base(spriteLocation, false)
        {
            this.spriteLocation = spriteLocation;
        }

        public PhysicsObject(TiledObject obj=null) : base( obj.GetStringProperty("Sprite") != "" ? obj.GetStringProperty("Sprite") : "square.png" , false)
        {
            spriteLocation = obj.GetStringProperty("Sprite") != "" ? obj.GetStringProperty("Sprite") : "square.png";
            objectName = obj.Name;
            originalPosition = new Vec2( obj.X+obj.Width/2, obj.Y+obj.Height/2);
        }


        public int gridIndex{get; set;}

        public void DebugToggle() 
        {
            // place debug methods here
        }


        void MoveBackToOriginalPosition()
        {
            x = x + (originalPosition.x - x) * 0.1f;
            y = y + (originalPosition.y - y) * 0.1f;
            if (x == originalPosition.x && y == originalPosition.y) isOriginalPosition = true;
        }


        void DragObject()
        {
            if (Input.GetMouseButton(0)
                && Input.mouseX > x - width / 2
                && Input.mouseX < x + width / 2
                && Input.mouseY > y - height / 2
                && Input.mouseY < y + height / 2
                )
            {
                isDragging = true;
                x = x + (Input.mouseX - x) * 0.1f;
                y = y + (Input.mouseY - y) * 0.1f; 
            } else if (isDragging && !Input.GetMouseButton(0))
            {
                isDragging = false;
                if ( x > PinBallMachine.coords[0].x && 
                        x < PinBallMachine.coords[1].x &&
                        y > PinBallMachine.coords[0].y &&
                        y < PinBallMachine.coords[1].y
                    ) {
                    isValidPlacement = true;
                    isOriginalPosition = false;
                } else {
                    isValidPlacement = false;
                    MoveBackToOriginalPosition();
                }

            }   else if (isDragging)
            {
                x = x + (Input.mouseX - x) * 0.1f;
                y = y + (Input.mouseY - y) * 0.1f;
            } else if (!isValidPlacement)
            {
                MoveBackToOriginalPosition();
            }
        }

        public void SetStatic(bool isStatic)
        {
            this.isStatic = isStatic;
        }

        void Update()
        {
            if (!isStatic) DragObject();
            DebugToggle();
        }
    }

}