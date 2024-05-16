using GXPEngine.Core;
using GXPEngine.LevelAssets.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Level : Scene
    {
        private static Rigidbody Ball;

        public Vec2[] verticies = new Vec2[] { new Vec2(289, 1048), new Vec2(300, 1018), new Vec2(115, 892), new Vec2(104, 871),
            new Vec2(102, 374), new Vec2(111, 309), new Vec2(124, 273), new Vec2(141, 240), new Vec2(168, 206), new Vec2(204, 171),
            new Vec2(246, 145), new Vec2(294, 128), new Vec2(346, 117), new Vec2(433, 116), new Vec2(505, 121), new Vec2(567, 133),
            new Vec2(592, 140), new Vec2(595, 115), new Vec2(609, 94), new Vec2(628, 83), new Vec2(650, 80), new Vec2(660, 80),
            new Vec2(687, 91), new Vec2(707, 115), new Vec2(709, 148), new Vec2(693, 174), new Vec2(662, 192), new Vec2(672, 200),
            new Vec2(682, 213), new Vec2(692, 235), new Vec2(699, 260), new Vec2(704, 303), new Vec2(704, 966), new Vec2(668, 966),
            new Vec2(668, 604), new Vec2(644, 624), new Vec2(642, 633), new Vec2(651, 646), new Vec2(650, 658), new Vec2(633, 673),
            new Vec2(650, 693), new Vec2(652, 854), new Vec2(648, 878), new Vec2(631, 898), new Vec2(457, 1020), new Vec2(475, 1054) };

        int levelIndex;
        public Level(int levelIndex) : base(true)
        {
            UILayer.LateDestroy();
            UILayer = new GameUI();
            AddChild(UILayer);
            PhysicsManager.setup();
            tiledManager.LoadTiledMap("Levels Project 4/" + ((int)((levelIndex - 1) / 10 + 1)) + "-" + ((levelIndex - 1) % 10 + 1) + ".tmx");

            //tiledManager.LoadTiledMap("map" + levelIndex + "/map" + levelIndex + ".tmx");

            Console.WriteLine("Opening level from Levels Project 4/" + ((int)((levelIndex - 1) / 10 + 1)) + "-" + ((levelIndex - 1) % 10 + 1) + ".tmx");



            Ball = new Rigidbody("Circle.png", new CircleCollider(16));
            workspace.AddChild(Ball);
            Ball.Position = new Vec2(686, 948);

            for (int i = 0; i < verticies.Length - 1; i++)
            {
                Vec2 start = verticies[i];
                Vec2 end = verticies[i + 1];
                workspace.AddChild(new Rigidbody("", new LineCollider(start, end)));
            }
            Console.WriteLine(levelIndex);
            MainGame.singleton.levelIndex = levelIndex;
            this.levelIndex = levelIndex;
            ObjectiveManager.isCounting = false;
        }

        override public void Update()
        {
            base.Update();
            if (Ball.Position.y > 1080)
                ObjectiveManager.Complete();
            if (InputManager.IsShotDefault())
            {
                Ball.Position = new Vec2(686, 948);
                Ball.velocity = new Vec2(0, -1500);
                ObjectiveManager.isCounting = true;
            }
            if (InputManager.IsShotTwo())
            {
                Ball.Position = new Vec2(686, 948);
                Ball.velocity = new Vec2(0, -1825);
                ObjectiveManager.isCounting = true;
            }
            if (InputManager.IsShotThree())
            {
                Ball.Position = new Vec2(686, 948);
                Ball.velocity = new Vec2(0, -2250);
                ObjectiveManager.isCounting = true;
            }
            ObjectiveManager.UpdateScore(ScoreType.PlayerSpeed, Ball.velocity.magnitude);

            if(Input.GetKey(Key.R))
            {
                parent.AddChild(new Level(levelIndex));
            }
        }
    }
}
