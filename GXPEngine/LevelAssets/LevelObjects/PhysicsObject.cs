using GXPEngine.Core;
using GXPEngine.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using TiledMapParser;
using System.Drawing;

namespace GXPEngine
{
    public class PhysicsObject : Sprite
    {
        string colliderType;
        string spriteLocation;
        bool isStatic = false;
        bool isDragging = false;
        Vec2 originalPosition;
        bool isOriginalPosition;
        bool isValidPlacement = false;
        bool isUIObject = false;
        bool levelJustStarted = true;
        int placementCount;

        public PhysicsObject(string spriteLocation, bool isStatic, int placementCount, string colliderType) : base(spriteLocation, false)
        {
            this.spriteLocation = spriteLocation;
            this.isStatic = isStatic;
            this.isUIObject = true;
            this.placementCount = placementCount;
            this.colliderType = colliderType;

            if (isStatic) SetColor(0,0,0);
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
        }

        void MoveBackToOriginalPosition()
        {
            x = x + (originalPosition.x - x) * 0.1f;
            y = y + (originalPosition.y - y) * 0.1f;
            if ((int)x == (int)originalPosition.x && (int)y == (int)originalPosition.y) isOriginalPosition = true;
            // if (isOriginalPosition) this.Destroy();    //NOTE uncomment this line to destroy object when it is back to original position
        }

        void MoveToOriginalPosition()
        {
            x = originalPosition.x;
            y = originalPosition.y;
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
                    if (isUIObject) SpawnPhysicsObject();
                    MoveToOriginalPosition();
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
            switch (colliderType)
            {
                case "Bumper":
                    Bumper newCollider = new Bumper();
                    newCollider.SetXY(x, y);
                    Scene.workspace.AddChild(newCollider);
                    break;
                case "Triangle":
                    Triangle newTriangle = new Triangle();
                    newTriangle.SetXY(x, y);
                    Scene.workspace.AddChild(newTriangle);
                    break;
                case "Brake":
                    Brake newBrake = new Brake();
                    newBrake.SetXY(x, y);
                    Scene.workspace.AddChild(newBrake); 
                    break;
                case "Booster":   
                    Booster newBooster = new Booster();
                    newBooster.SetXY(x, y);
                    Scene.workspace.AddChild(newBooster);
                    break;
                case "Ice":
                    Ice newIce = new Ice();
                    newIce.SetXY(x, y);
                    Scene.workspace.AddChild(newIce);
                    break;
                default:
                    Console.WriteLine("No collider type found");
                    break;
            }

            placementCount--;
            if (placementCount == 0) 
            {
                SetColor(0,0,0);
                isStatic = true;
            }
            isValidPlacement = false;
        }

        void SetStatic(bool isStatic)
        {
            this.isStatic = isStatic;
        }

        void LevelStart()
        {
            MoveBackToOriginalPosition();
            levelJustStarted = isOriginalPosition ? false : true;
        }

        void Update()
        {
            if (!isStatic) DragObject();
            if (isUIObject && levelJustStarted) LevelStart();
            DebugToggle();
        }
    }

}