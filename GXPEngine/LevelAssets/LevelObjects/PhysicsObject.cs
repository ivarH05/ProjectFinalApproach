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

    /* make constructor with following arguments
        newSprite = (Sprite)_callingAssembly.CreateInstance(obj.Type,false,BindingFlags.Default,null, new object[] { obj }, null, null);
    */
        BoxCollider collider;
        string spriteLocation;
        bool isStatic = true;

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

        public PhysicsObject(TiledObject obj=null) : base( obj.GetStringProperty("Sprite") != null ? obj.GetStringProperty("Sprite") : "square.png" , false)
        {
            spriteLocation = "square.png";
            if ( obj.GetStringProperty("Sprite") != null ) this.spriteLocation = obj.GetStringProperty("Sprite");
        }


        public int gridIndex{get; set;}

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