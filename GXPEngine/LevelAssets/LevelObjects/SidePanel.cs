using GXPEngine.Core;
using GXPEngine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using TiledMapParser;
using System.Dynamic;
using System.Runtime.InteropServices;

namespace GXPEngine
{
    public class SidePanel : Sprite
    {
        public static Vec2[] coords;
        Vec2[] gridPositions;
        PhysicsObject[] gridObjects;

        string[] spriteLocations = new string[] 
        {    
            "square.png", 
            "square.png", 
            "square.png", 
            "square.png", 
            "square.png" 
        };

        //<GridIndex, ObjectCount>
        Dictionary<int, int> gridIndex = new Dictionary<int, int>()
        {
            { 1, 0 },
            { 2, 0 },
            { 3, 0 },
            { 4, 0 },
            { 5, 0 },
        };

        public SidePanel() : base("square.png", false)
        {
            alpha = 0f;
        }

        public SidePanel(TiledObject obj = null) : base("square.png", false)
        {
            alpha = 0f;
            coords = new Vec2[]{ new Vec2(obj.X, obj.Y), new Vec2(obj.Width, obj.Height)  };
            
            for ( int i = 0; i < obj.GetIntProperty("GridSize"); i++ )
            {
                // Console.WriteLine("GridProp "+(i+1)+": "+obj.GetIntProperty("Object_"+i) );
                gridIndex[i+1] = obj.GetIntProperty("Object_"+i);
            }

            gridPositions = new Vec2[gridIndex.Count];
            gridObjects = new PhysicsObject[gridIndex.Count];
            SetGridPositions();
        }

        void IsClickedOn()
        {
            if (Input.GetMouseButton(0) && Input.mouseX > coords[0].x && Input.mouseX < coords[0].x + coords[1].x && Input.mouseY > coords[0].y && Input.mouseY < coords[0].y + coords[1].y)
            {
                Console.WriteLine("Clicked on SidePanel");
            }
        }

        void SetGridPositions()
        {
            Vec2 currentGridPosition = new Vec2(0, 0);
            int offset = 5;
            bool runOnce = true;

            for ( int i = 0; i < gridIndex.Count; i++ )
            {
                PhysicsObject spriteToAdd = new PhysicsObject(spriteLocations[i], true);
                spriteToAdd.SetOrigin(spriteToAdd.width/2, spriteToAdd.height/2);

                if ( currentGridPosition.y == 0 && runOnce ) currentGridPosition = new Vec2( coords[0].x+spriteToAdd.width/2+offset, coords[0].y+spriteToAdd.height/2+offset );
                else currentGridPosition = new Vec2(currentGridPosition.x + spriteToAdd.width, currentGridPosition.y);

                if ( currentGridPosition.x + spriteToAdd.width >= width ) currentGridPosition = new Vec2( currentGridPosition.x+spriteToAdd.height , currentGridPosition.y + spriteToAdd.height);

                spriteToAdd.SetClickCollider( new Vec2( currentGridPosition.x, currentGridPosition.y ));
                gridPositions[i] = currentGridPosition;
                gridObjects[i] = spriteToAdd;
                Scene.workspace.AddChild(spriteToAdd);

                if ( runOnce ) runOnce = false;             
            }
        }


        PhysicsObject GeneratePhysicsObject()
        {
            return new PhysicsObject("square.png", new Vec2(x, y));
        }

        void CheckIfClicked()
        {
            if (Input.GetMouseButton(0) && Input.mouseX > x - width / 2 && Input.mouseX < x + width / 2 && Input.mouseY > y - height / 2 && Input.mouseY < y + height / 2)
            {
                PhysicsObject newObject = GeneratePhysicsObject();
                newObject.SetXY(Input.mouseX, Input.mouseY);
                newObject.gridIndex = 1;
                newObject.DebugToggle();
            }
        }


        void Update()
        {
            IsClickedOn();
            // CheckIfClicked();
        }
    }
}