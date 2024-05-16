using GXPEngine.Core;
using GXPEngine.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GXPEngine
{
    public class Scene : GameObject
    {
        public static LevelProperties levelProperties;

        public static bool debugMode;

        /// <summary>
        /// Singleton will make sure there is only one scene at a time. 
        /// </summary>
        private static Scene _singleton;
        /// <summary>
        /// The position of the camera based on the corner of the screen.
        /// </summary>
        public static Vec2 cameraPosition = new Vec2(960, 540);

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
        public static UILayer UILayer { get; internal set; }

        public static TiledManager tiledManager { get; private set; }
        public static Debugger debugger { get; private set; }
        public static InputManager inputManager { get; private set; }

        
        public Scene(bool overrideSingleton = true)
        {
            if (_singleton != null && overrideSingleton)
            {
                UILayer.LateDestroy();
                debugger.LateDestroy();
                background.LateDestroy();
            }
            _singleton = this;

            inputManager = new InputManager();
            debugger = new Debugger();
            background = new Background();
            workspace = new Workspace();
            UILayer = new UILayer();
            tiledManager = new TiledManager(workspace);


            AddChild(background);
            AddChild(workspace);
            AddChild(UILayer);
            AddChild(debugger);
            PhysicsManager.setup();

        }

        public bool SceneIsOver()
        {
            // transition to next scene and destroy this one        
            return false;
        }

        virtual public void Update()
        {
            if (_singleton != this)
                return;
            PhysicsManager.PhysicsUpdate();
            ObjectiveManager.Update();
            inputManager.Update();
            debugger.Update();

        }
    }
}
