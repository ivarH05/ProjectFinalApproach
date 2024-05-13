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
        bool isStatic = true;
        bool isUIBox;

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
            isUIBox = obj.GetStringProperty("UI") == "true";
            if (isUIBox) this.alpha = 0f;
        }


        public int gridIndex{get; set;}

        public void DebugToggle() 
        {
            if (!isUIBox) return;
            if (!Scene.debugMode) this.alpha = 0f;
            else this.alpha = 0.5f;
        }

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
            DebugToggle();
        }
    }

}