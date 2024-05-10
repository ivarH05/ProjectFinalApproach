using GXPEngine.Core;
using GXPEngine.Managers;
using System;
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
        public static PhysicsObject  physicsObject { get; private set; }
        
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
            physicsObject = new PhysicsObject();
            AddChild(physicsObject);
            AddChild(background);
            AddChild(workspace);
            AddChild(UILayer);
            AddChild(debugger);
            
            // tiledManager.LoadTiledMap("testmap.tmx");       //Uncomment this line to load a tiled map
        }

        void Update()
        {
            PhysicsManager.PhysicsUpdate();
            inputManager.Update();
            debugger.Update();
            workspace.Position = -cameraPosition;
            background.Position = -cameraPosition; // (* 0.75 for depth effect)
        }
    }
}
