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
        string colliderType;
        string[] colliderTypes = new string[] { "Box", "Circle", "Line" };
        BoxCollider collider;
        string spriteLocation;
        bool isStatic = false;
        bool isDragging = false;
        Vec2 originalPosition;
        bool isOriginalPosition;
        Vec2 realPosition;
        bool isValidPlacement = false;

        public PhysicsObject(string spriteLocation, bool isStatic) : base(spriteLocation, false)
        {
            this.spriteLocation = spriteLocation;
            this.isStatic = isStatic;
        }

        public PhysicsObject(string spriteLocation, Vec2 originalPosition) : base(spriteLocation, false)
        {
            this.spriteLocation = spriteLocation;
            this.originalPosition = originalPosition;
        }

        public PhysicsObject(TiledObject obj=null) : base( obj.GetStringProperty("Sprite") != "" ? obj.GetStringProperty("Sprite") : "square.png" , false)
        {
            spriteLocation = obj.GetStringProperty("Sprite") != "" ? obj.GetStringProperty("Sprite") : "square.png";
            originalPosition = new Vec2( obj.X+obj.Width/2, obj.Y+obj.Height/2);
            isStatic = true;
        }


        public int gridIndex{get; set;}

        public void DebugToggle() 
        {
            // place debug methods here
            if ( Scene.debugMode )
            {
                isStatic = false;
                // Console.WriteLine("x: " + x + " y: " + y);
            }
        }

        public void SetClickCollider( Vec2 pos, Vec2 size )
        {
            originalPosition = pos;
            width = (int)size.x;
            height = (int)size.y;
            Console.WriteLine(originalPosition.x + " " + originalPosition.y);
        }

        void MoveBackToOriginalPosition()
        {
            x = x + (originalPosition.x - x) * 0.1f;
            y = y + (originalPosition.y - y) * 0.1f;
            if (x == originalPosition.x && y == originalPosition.y) isOriginalPosition = true;
            // if (isOriginalPosition) this.Destroy();    //NOTE uncomment this line to destroy object when it is back to original position
        }

        void IsClickedOn()
        {
            Vec2 mousePos = InverseTransformPoint(Input.mouseX, Input.mouseY);
            // Console.WriteLine(mousePos);
            if (Input.GetMouseButton(0)
                && mousePos.x > x - width / 2 
                && mousePos.x < x + width / 2
                && mousePos.y > y - height / 2
                && mousePos.y < y + height / 2
                )
            {
                Console.WriteLine("Clicked on object");
            }
        }

        void DragObject()
        {
            Vec2 mousePos = InverseTransformPoint(Input.mouseX, Input.mouseY);
            // Console.WriteLine(mousePos);
            if (Input.GetMouseButton(0)
                && mousePos.x > x - width / 2 
                && mousePos.x < x + width / 2
                && mousePos.y > y - height / 2
                && mousePos.y < y + height / 2
                )
            {
                isDragging = true;
                mousePos *= 2;
                x = x + (mousePos.x - x) * 0.1f;
                y = y + (mousePos.y - y) * 0.1f; 
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
                x = x + (mousePos.x  - x) * 0.1f;
                y = y + (mousePos.y - y) * 0.1f;
            } else if (!isValidPlacement)
            {
                MoveBackToOriginalPosition();
            }
        }

        void SetStatic(bool isStatic)
        {
            this.isStatic = isStatic;
        }

        void Update()
        {
            IsClickedOn();
            if (!isStatic) DragObject();
            DebugToggle();
        }
    }

}