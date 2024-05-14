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
        int levelIndex;
        public static LevelProperties levelProperties;

        public static bool debugMode;

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

        private Rigidbody Ball;

        public Vec2[] verticies = new Vec2[] { new Vec2(289, 1048), new Vec2(300, 1018), new Vec2(115, 892), new Vec2(104, 871), 
            new Vec2(102, 374), new Vec2(111, 309), new Vec2(124, 273), new Vec2(141, 240), new Vec2(168, 206), new Vec2(204, 171), 
            new Vec2(246, 145), new Vec2(294, 128), new Vec2(346, 117), new Vec2(433, 116), new Vec2(505, 121), new Vec2(567, 133),
            new Vec2(592, 140), new Vec2(595, 115), new Vec2(609, 94), new Vec2(628, 83), new Vec2(650, 80), new Vec2(660, 80), 
            new Vec2(687, 91), 
            new Vec2(707, 115), new Vec2(709, 148), new Vec2(693, 174), new Vec2(662, 192), 
            new Vec2(672, 200), new Vec2(682, 213), new Vec2(692, 235), 
            new Vec2(699, 260), new Vec2(704, 303), new Vec2(704, 966), new Vec2(668, 966), new Vec2(668, 604), 
            new Vec2(644, 624), new Vec2(642, 633), new Vec2(651, 646), new Vec2(650, 658), new Vec2(633, 673), new Vec2(650, 693), 
            new Vec2(652, 854), new Vec2(648, 878), new Vec2(631, 898), new Vec2(457, 1020), new Vec2(475, 1054) };
        
        public Scene(bool overrideSingleton = false, int levelIndex = 1)
        {
            if (_singleton != null && !overrideSingleton)
            {
                _singleton.LateDestroy();
            }
            _singleton = this;
            this.levelIndex = levelIndex;

            tiledManager = new TiledManager( this );
            inputManager = new InputManager();
            debugger = new Debugger();
            background = new Background();
            workspace = new Workspace();
            UILayer = new UILayer();

            tiledManager.LoadTiledMap("map"+levelIndex+"/map"+levelIndex+".tmx");       //Uncomment this line to load a tiled map

            AddChild(background);
            AddChild(workspace);
            AddChild(UILayer);
            AddChild(debugger);
            PhysicsManager.setup();

            Ball = new Rigidbody("Circle.png", new CircleCollider(16));
            workspace.AddChild(Ball);
            Ball.Position = new Vec2(686, 948);

            for (int i = 0; i < verticies.Length - 1; i++)
            {
                Vec2 start = verticies[i];
                Vec2 end = verticies[i + 1];
                workspace.AddChild(new Rigidbody("", new LineCollider(start, end)));
            }
        }

        public bool SceneIsOver()
        {
            // transition to next scene and destroy this one        
            return false;
        }

        void Update()
        {
            PhysicsManager.PhysicsUpdate();
            inputManager.Update();
            debugger.Update();
            workspace.Position = -cameraPosition;
            background.Position = -cameraPosition; // (* 0.75 for depth effect)

            if (Input.GetKey(Key.NUMPAD_1))
            {
                Ball.Position = new Vec2(686, 948);
                Ball.velocity = new Vec2(0, -1500);
            }
            if (Input.GetKey(Key.NUMPAD_2))
            {
                Ball.Position = new Vec2(686, 948);
                Ball.velocity = new Vec2(0, -1825);
            }
            if (Input.GetKey(Key.NUMPAD_3))
            {
                Ball.Position = new Vec2(686, 948);
                Ball.velocity = new Vec2(0, -2250);
            }
        }
    }
}
