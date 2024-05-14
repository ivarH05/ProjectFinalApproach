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
        bool isUIBox;
        string objectName;
        Vec2 originalPosition;
        bool isOriginalPosition;
        bool isValidPlacement = false;
        bool isTransparent = false;

        public PhysicsObject(string spriteLocation = "square.png") : base(spriteLocation, false)
        {
            this.spriteLocation = spriteLocation;
            // empty
        }

        public PhysicsObject(TiledObject obj=null, string spriteLocation = "square.png") : base(spriteLocation, false)
        {
            this.spriteLocation = spriteLocation;
            // Empty
        }

        public PhysicsObject(TiledObject obj=null) : base( obj.GetStringProperty("Sprite") != "" ? obj.GetStringProperty("Sprite") : "square.png" , false)
        {
            spriteLocation = obj.GetStringProperty("Sprite") != "" ? obj.GetStringProperty("Sprite") : "square.png";
            objectName = obj.Name;
            isStatic = isUIBox = obj.GetStringProperty("UI") == "true";
            originalPosition = new Vec2( obj.X+obj.Width/2, obj.Y+obj.Height/2);
            this.alpha = isUIBox ? 0f : 1f;
        }


        public int gridIndex{get; set;}

        public void DebugToggle() 
        {
            if (isUIBox && !Scene.debugMode) 
                this.alpha = 0f;
            else if (this.alpha == 0f)
                this.alpha = 0.5f;
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
                if ( x > 1000 ) {
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