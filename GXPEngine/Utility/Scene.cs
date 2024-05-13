using GXPEngine.Core;
using GXPEngine.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Scene : GameObject
    {
        /// <summary>
        /// Singleton will make sure there is only one scene at a time. 
        /// </summary>
        private static Scene _singleton;

        /// <summary>
        /// The position of the camera based on the corner of the screen.
        /// </summary>
        public static Vec2 cameraPosition;

        /// <summary>
        /// The background of the scene, these objects cannot be interacted with
        /// </summary>
        public static Background background {get; private set;}

        /// <summary>
        /// The main hierachy of the scene, these are all objects that can be interacted with / that are on the same layer as the player
        /// </summary>
        public static Workspace workspace { get; private set; }

        /// <summary>
        /// The UI, here all user interface objects that should not move with the camera can be placed. 
        /// </summary>
        public static UILayer UILayer { get; private set; }

        public static TiledManager tiledManager { get; private set; }
        public static Debugger debugger { get; private set; }
        public static InputManager inputManager { get; private set; }
        
        public Scene(bool overrideSingleton = false)
        {
            if (_singleton != null && !overrideSingleton)
            {
                LateDestroy();
                return;
            }
            _singleton = this;

            tiledManager = new TiledManager( this );
            inputManager = new InputManager();
            debugger = new Debugger();
            background = new Background();
            workspace = new Workspace();
            UILayer = new UILayer();
            AddChild(background);
            AddChild(workspace);
            AddChild(UILayer);
            AddChild(debugger);
            PhysicsManager.setup();

            // tiledManager.LoadTiledMap("testmap.tmx");       //Uncomment this line to load a tiled map


            Vec2[] verts = new Vec2[] { new Vec2(400, 1200), new Vec2(0, 800), new Vec2(700, 600), new Vec2(400, 0),
            new Vec2(000, 200)};

            for (int i = 0; i < verts.Length; i++)
            {
                Vec2 start = verts[i];
                Vec2 end = verts[(i + 1) % verts.Length];

                workspace.AddChild(new Rigidbody("", new LineCollider(start, end)));
            }
            Rigidbody c = new Rigidbody("Circle.png", new CircleCollider(32));
            workspace.AddChild(c);
            c.Position = new Vec2(500, 500);

            ball = new Rigidbody("Circle.png", new CircleCollider(32));
            workspace.AddChild(ball);
            ball.isKinematic = true;
        }

        public Rigidbody ball;

        void Update()
        {
            PhysicsManager.PhysicsUpdate();
            inputManager.Update();
            debugger.Update();
            workspace.Position = -cameraPosition;
            background.Position = -cameraPosition; // (* 0.75 for depth effect)


            ball.velocity = (Input.getMouseWorldPosition() - ball.Position) / Time.timeStep;
            ball.Position = Input.getMouseWorldPosition();
        }
    }
}
