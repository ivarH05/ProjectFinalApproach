﻿using GXPEngine.Core;
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

        public Scene(bool overrideSingleton = false)
        {
            if (_singleton != null && !overrideSingleton)
            {
                LateDestroy();
                return;
            }
            _singleton = this;

            background = new Background();
            workspace = new Workspace();
            UILayer = new UILayer();
            AddChild(new Rigidbody());
        }

        void Update()
        {
            workspace.Position = -cameraPosition;
            background.Position = -cameraPosition; // (* 0.75 for depth effect)

            PhysicsManager.PhysicsUpdate();
        }
    }
}
