using GXPEngine.Core;
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

        Rigidbody ball1 = new Rigidbody("Square.png", new BoxCollider(new Vec2(64, 64)));
        Rigidbody ball2 = new Rigidbody("Square.png", new BoxCollider(new Vec2(64, 64)));

        Rigidbody ball3 = new Rigidbody("Circle.png", new CircleCollider(8));

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
            AddChild(ball1);
            ball1.Position = new Vec2(800, 450);
            ball1.isKinematic = true;
            ball2.useGravity = false;
            AddChild(ball2);


            ball3.isKinematic = true;
            ball3.width = 8; ball3.height = 8;
            AddChild(ball3);
        }

        void Update()
        {
            workspace.Position = -cameraPosition;
            background.Position = -cameraPosition; // (* 0.75 for depth effect)

            PhysicsManager.PhysicsUpdate();
            ball2.Position = Input.getMouseWorldPosition();
            ball2.velocity = new Vec2(0, 1000);

            CollisionData dat = ball2.collider.PredictCollision(ball1.collider);
            Console.WriteLine(dat);

            if (Input.GetKey(Key.SPACE)) ball2.velocity = new Vec2();
            ball1.rotation += 1;
        }
    }
}
