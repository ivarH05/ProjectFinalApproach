using GXPEngine.Core;
using GXPEngine.LevelAssets.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO animate spring once when shot is taken

namespace GXPEngine
{
    public class Level : Scene
    {
        private static Rigidbody Ball;
        AnimationSprite spring;
        private static int type = 1;
        public static string getPath()
        {
            return "assets/Style" + type + "/";
        }
        float nextLevelTimer = 0;

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
            type = (levelIndex - 1) / 10 + 1;
            UILayer.LateDestroy();
            UILayer = new GameUI();
            AddChild(UILayer);
            PhysicsManager.setup();
            tiledManager.LoadTiledMap("Levels Project 4/" + ((int)((levelIndex - 1) / 10 + 1)) + "-" + ((levelIndex - 1) % 10 + 1) + ".tmx");
            AddCat();
            int frameCountLevel = ((int)((levelIndex - 1) / 10 + 1)) == 3 ? 10 : 11;
            workspace.AddChildAt(spring = new AnimationSprite(getPath() + "Spring.png", frameCountLevel, 1), 1); // 11 for maps 1-10, 2-10 and 10 for 3-10 
            spring.width = 200;
            spring.height = 500;
            spring.Position = new Vec2(780, 600);
            spring.SetCycle(0, frameCountLevel, 60);
            //tiledManager.LoadTiledMap("map" + levelIndex + "/map" + levelIndex + ".tmx");

            Console.WriteLine("Opening level from Levels Project 4/" + ((int)((levelIndex - 1) / 10 + 1)) + "-" + ((levelIndex - 1) % 10 + 1) + ".tmx");



            Ball = new Player();
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
            if (type == 3)
            {
                PhysicsManager.GravityMultiplier = 0;
                PhysicsManager.AirFriction = 0f;
            }
            else
            {
                PhysicsManager.GravityMultiplier = 1;
                PhysicsManager.AirFriction = 0.2f;
            }
            MainGame.LevelMusic();
        }
        AnimationSprite cat;
        void AddCat()
        {
            int framecount = type == 1 ? 5 : 9;
            workspace.AddChildAt(cat = new AnimationSprite(getPath() + "Cat.png", framecount, 1), 1);
            cat.SetCycle(0, framecount, 180);
            cat.Position = new Vec2(1390, 360);
            cat.width =  cat.height = type == 3 ? 730 : 500;
            if (type == 1)
            {
                cat.height = 500;
                cat.width = 370;
            }
        }

        override public void Update()
        {
            if(cat != null) 
                cat.Animate(Time.deltaTime);
            if (spring != null && InputManager.isShot)
                spring.Animate(Time.deltaTime);
            if (spring.currentFrame == spring.frameCount - 1)
            {
                spring.SetFrame(0); 
                InputManager.isShot = false;
            }
            base.Update();
            if (Ball.Position.y > 1080)
                ObjectiveManager.Complete();
            if (!ObjectiveManager.isActive)
            {
                if (InputManager.IsShotDefault())
                {
                    Ball.Position = new Vec2(686, 948);
                    Ball.velocity = new Vec2(0, -1500);
                    ObjectiveManager.isCounting = true;
                }
                else if (InputManager.IsShotTwo())
                {
                    Ball.Position = new Vec2(686, 948);
                    Ball.velocity = new Vec2(0, -1825);
                    ObjectiveManager.isCounting = true;
                }
                else if (InputManager.IsShotThree())
                {
                    Ball.Position = new Vec2(686, 948);
                    Ball.velocity = new Vec2(0, -2250);
                    ObjectiveManager.isCounting = true;
                }
            }
            ObjectiveManager.UpdateScore(ScoreType.PlayerSpeed, Ball.velocity.magnitude);
            if(nextLevelTimer > 0)
            {
                nextLevelTimer += Time.deltaTime;
                if (nextLevelTimer > 1)
                    ((MainGame)parent).ChangeLevel();

            }
            else if(ObjectiveManager.GetObjectiveState() == ObjectiveState.completed)
            {
                nextLevelTimer += Time.deltaTime;
            }

            if (Input.GetKeyUp(Key.R) || ObjectiveManager.GetObjectiveState() == ObjectiveState.failed)
            {
                parent.AddChild(new Level(levelIndex));
                spring.Destroy();
                InputManager.isShot = false;
            }
        }
    }
}
