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
        //string[] colliderTypes = new string[] { "Box", "Circle", "Line" };
        new BoxCollider collider;
        string spriteLocation;
        bool isStatic = false;
        bool isDragging = false;
        Vec2 originalPosition;
        bool isOriginalPosition;
        bool isValidPlacement = false;
        bool isUIObject = false;
        bool levelJustStarted = true;
        int placementCount;
        EasyDraw showPlacementCount;

        public static bool AlreadyDragging = false;

        int offset = 40;

        public PhysicsObject(string spriteLocation, bool isStatic, int placementCount, string colliderType) : base(spriteLocation, false)
        {
            this.spriteLocation = spriteLocation;
            this.isStatic = isStatic;
            this.isUIObject = true;
            this.placementCount = placementCount;
            this.colliderType = colliderType;
            this.showPlacementCount = new EasyDraw(100, 100, false);

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

        void DrawPlacementCount()
        {
            showPlacementCount.Clear(Color.Transparent);
            showPlacementCount.Fill(255, 255, 255);
            showPlacementCount.TextFont(Utils.LoadFont("Assets/WishMF.ttf", 20));
            showPlacementCount.Text(placementCount.ToString(), 50, 50);
            AddChild(showPlacementCount);
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
                && Input.mouseX > x - offset
                && Input.mouseX < x + offset
                && Input.mouseY > y - offset
                && Input.mouseY < y + offset
                )
            {
                return true;
            }
            return false;
        }

        void DragObject()
        {
            if (ObjectiveManager.isActive)
            {
                MoveBackToOriginalPosition();
                return;
            }

            if (Input.GetMouseButton(0)
                && AlreadyDragging == false
                && Input.mouseX  > x - offset
                && Input.mouseX  < x + offset
                && Input.mouseY > y - offset
                && Input.mouseY < y + offset
                )
            {
                AlreadyDragging = true;
                isDragging = true;
                x = x + (Input.mouseX - x) * 0.1f;
                y = y + (Input.mouseY - y) * 0.1f; 
            } else if (isDragging && !Input.GetMouseButton(0))
            {
                AlreadyDragging = false;
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
            GameObject rb = null;
            switch (colliderType)
            {
                case "Bumper":
                    Bumper newCollider = new Bumper();
                    newCollider.SetXY(x, y);
                    Scene.workspace.AddChild(rb = newCollider);
                    break;
                case "Triangle":
                    Triangle newTriangle = new Triangle();
                    newTriangle.SetXY(x, y);
                    Scene.workspace.AddChild(rb = newTriangle);
                    break;
                case "Brake":
                    Brake newBrake = new Brake();
                    newBrake.SetXY(x, y);
                    Scene.workspace.AddChild(rb = newBrake); 
                    break;
                case "Booster":   
                    Booster newBooster = new Booster();
                    newBooster.SetXY(x, y);
                    Scene.workspace.AddChild(rb = newBooster);
                    break;
                case "Ice":
                    Ice newIce = new Ice();
                    newIce.SetXY(x, y);
                    Scene.workspace.AddChild(rb = newIce);
                    break;
                default:
                    Console.WriteLine("No collider type found");
                    break;
            }
            if (rb == null)
                return;

            if(rb is Triangle t)
            {
                if (t.isColliding())
                {
                    t.LateDestroy();
                    isValidPlacement = false;
                    return;
                }
            }
            if(rb is Rigidbody r)
            {
                if(r.CalculateCollisions().Count > 0)
                {
                    r.LateDestroy();
                    isValidPlacement = false;
                    return;
                }
            }

            placementCount--;
            if (placementCount == 0) 
            {
                SetColor(0,0,0);
                isStatic = true;
            }
            isValidPlacement = false;
            DrawPlacementCount();
        }

        void SetStatic(bool isStatic)
        {
            this.isStatic = isStatic;
        }

        void LevelStart()
        {
            MoveBackToOriginalPosition();
            DrawPlacementCount();
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