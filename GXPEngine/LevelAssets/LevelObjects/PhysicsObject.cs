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
        bool isValidPlacement = false;
        bool isUIObject = false;

        public PhysicsObject(string spriteLocation, bool isStatic) : base(spriteLocation, false)
        {
            this.spriteLocation = spriteLocation;
            this.isStatic = isStatic;
            this.isUIObject = true;
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
            }
        }

        public void SetClickCollider( Vec2 pos)
        {
            originalPosition = pos;
            isOriginalPosition = false;
        }

        void MoveBackToOriginalPosition()
        {
            x = x + (originalPosition.x - x) * 0.1f;
            y = y + (originalPosition.y - y) * 0.1f;
            if (x == originalPosition.x && y == originalPosition.y) isOriginalPosition = true;
            // if (isOriginalPosition) this.Destroy();    //NOTE uncomment this line to destroy object when it is back to original position
        }

        bool CanBeClickedOn()
        {
            if (Input.GetMouseButton(0)
                && Input.mouseX > x - width / 2 
                && Input.mouseX < x + width / 2
                && Input.mouseY > y - height / 2
                && Input.mouseY < y + height / 2
                )
            {
                return true;
            }
            return false;
        }

        void DragObject()
        {
            if (Input.GetMouseButton(0)
                && Input.mouseX  > x - width / 2 
                && Input.mouseX  < x + width / 2
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

        void SpawnPhysicsObject()
        {
            Console.WriteLine("Spawning object");
        }

        void SetStatic(bool isStatic)
        {
            this.isStatic = isStatic;
        }

        void Update()
        {
            if (!isStatic) DragObject();
            if (isUIObject && !isOriginalPosition) MoveBackToOriginalPosition();
            if (isUIObject && CanBeClickedOn()) SpawnPhysicsObject();
            DebugToggle();
        }
    }

}