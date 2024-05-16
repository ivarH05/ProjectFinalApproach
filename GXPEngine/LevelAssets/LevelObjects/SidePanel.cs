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
            Level.getPath() + "Bumper.png",
            Level.getPath() + "TriangleBumper.png",
            Level.getPath() + "Brake.png",
            Level.getPath() + "BoosterPreview.png",
            Level.getPath() + "Ice.png"
        };

        // use this later to spawn the physics collider objects 
        Dictionary<int, string> objectNames = new Dictionary<int, string>()
        {
            { 1,    "Bumper"    },
            { 2,    "Triangle"  },
            { 3,    "Brake"     },
            { 4,    "Booster"   },
            { 5,    "Ice"       }
        };

        //<GridIndex, ObjectCount>
        Dictionary<int, int> gridIndex = new Dictionary<int, int>()
        {
            { 1, 0 },   //Bumper
            { 2, 0 },   //Triangle
            { 3, 0 },   //Brake
            { 4, 0 },   //Booster
            { 5, 0 },   //Ice
        };

        public SidePanel() : base("square.png", false)
        {
            alpha = 0f;
        }

        public SidePanel(TiledObject obj = null) : base("square.png", false)
        {
            alpha = 0f;
            coords = new Vec2[]{ new Vec2(obj.X, obj.Y), new Vec2(obj.Width, obj.Height)  };

            // set count for objects            
            for ( int i = 1; i < gridIndex.Count + 1; i++ )
            {
                gridIndex[i] = obj.GetIntProperty(objectNames[i]);
            }

            gridPositions = new Vec2[gridIndex.Count];
            gridObjects = new PhysicsObject[gridIndex.Count];
            SetGridPositions();
        }

        void SetGridPositions()
        {
            Vec2 currentGridPosition = new Vec2(0, 0);
            int offset = 5;
            bool runOnce = true;

            for ( int i = 1; i < gridIndex.Count+1; i++ )
            {
                float newwidth = 64;
                PhysicsObject spriteToAdd;

                if ( gridIndex[i] == 0 ) spriteToAdd = new PhysicsObject(spriteLocations[i-1], true, gridIndex[i], objectNames[i]);
                else spriteToAdd = new PhysicsObject(spriteLocations[i-1], false, gridIndex[i], objectNames[i]);
                
                spriteToAdd.centerOrigin();

                if ( currentGridPosition.y == 0 && runOnce ) currentGridPosition = new Vec2( coords[0].x+newwidth/2+offset, coords[0].y+newwidth/2+offset );
                else currentGridPosition = new Vec2(currentGridPosition.x + newwidth, currentGridPosition.y);

                if ( currentGridPosition.x + newwidth >= width ) currentGridPosition = new Vec2( currentGridPosition.x+newwidth , currentGridPosition.y + newwidth);

                spriteToAdd.SetClickCollider( new Vec2( currentGridPosition.x, currentGridPosition.y ));
                spriteToAdd.SetXY( coords[0].x, coords[0].y-10000);
                gridPositions[i-1] = currentGridPosition;
                gridObjects[i-1] = spriteToAdd;
                Scene.workspace.AddChild(spriteToAdd);

                if ( runOnce ) runOnce = false;             
            }
        }

        void Update()
        {
        }
    }
}