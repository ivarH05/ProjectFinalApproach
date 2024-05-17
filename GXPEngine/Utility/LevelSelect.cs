using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class LevelSelect : Scene
    {
        EasyDraw[] levels;

        Vec2 offset = new Vec2(325, 235);
        int size = 235;
        int margin = 25;

        int maxlevel;
        int unlockedlevel;
        int levelOffset;
        bool stopped;
        public LevelSelect(int MaxLevel, int unlockedLevel) : base(true)
        {
            maxlevel = MaxLevel;
            unlockedlevel = unlockedLevel;
            ToLevels(0);
            Sprite background = (Sprite)AddChild(new Sprite("assets/UI/background.png", false, false));
            background.Position = new Vec2(0, 0);
            background.width = 1920;
            background.height = 1080;
            UILayer.AddChild(background);
            ToWorldSelect();
        }
        private Sprite SelectBackground;

        void ToWorldSelect()
        {
            foreach (EasyDraw level in levels)
            {
                level.LateDestroy();
            }
            levels = new EasyDraw[10];
            if(SelectBackground != null) 
                SelectBackground.LateDestroy();

            Sprite s = (Sprite)AddChild(new Sprite("assets/UI/LevelSelectBase.png", false, false));
            s.centerOrigin();
            s.Position = new Vec2(960, 600);
            s.width = 1336;
            s.height = 800;
            UILayer.AddChild(s);
            SelectBackground = s;
        }

        void ToLevels(int levelOffset = 0)
        {
            levels = new EasyDraw[10];
            if (SelectBackground != null)
                SelectBackground.LateDestroy();

            this.levelOffset = levelOffset;

            Sprite s = (Sprite)AddChild(new Sprite("assets/UI/PanelHorizontal.png", false, false));
            s.centerOrigin();
            s.Position = new Vec2(960, 600);
            s.width = 1336;
            s.height = 800;
            UILayer.AddChild(s);

            int rowLength = (int)(s.width - 2 * margin) / size;
            for (int i = 0; i < maxlevel; i++)
            {
                EasyDraw LevelIcon = new EasyDraw(size, (int)(size * 1.5f), false);
                LevelIcon.Clear(100, 25, 125);
                LevelIcon.NoFill();
                LevelIcon.Stroke(255);
                LevelIcon.StrokeWeight(20);
                LevelIcon.Rect(size / 2, size * 0.75f, size, size * 1.5f);
                LevelIcon.Fill(255);
                LevelIcon.TextAlign(CenterMode.Center, CenterMode.Center);
                LevelIcon.TextFont(Utils.LoadFont("Assets/WishMF.ttf", 56));
                LevelIcon.Text("" + ((int)((i + levelOffset) / 10 + 1)) + "-" + ((i + levelOffset) % 10 + 1), size / 2, size * 0.75f + 6);

                UILayer.AddChild(LevelIcon);
                levels[i] = LevelIcon;
                Vec2 position = new Vec2(i % rowLength, (int)i / rowLength * 1.45f);
                LevelIcon.Position = position * (size + margin) + offset;
                if (i + levelOffset < unlockedlevel)
                    LevelIcon.color = 0x771598;
                else
                    LevelIcon.color = 0x330554;
            }
        }

        void Update()
        {
            if (stopped) return;
            for (int i = 0; i < maxlevel && i < levels.Length; i++)
            {
                if (levels[i] == null)
                    break;
                EasyDraw LevelIcon = levels[i];
                Vec2 mousepos = Input.getMouseScreenPosition();
                if(i + levelOffset >= unlockedlevel)
                    LevelIcon.color = 0x330554;
                else if (LevelIcon.x < mousepos.x && LevelIcon.x + size > mousepos.x
                    && LevelIcon.y < mousepos.y && LevelIcon.y + size * 1.5 > mousepos.y)
                {
                    if(Input.GetMouseButtonUp(0))
                        MainGame.singleton.AddChild(new Level(i + levelOffset + 1));
                    LevelIcon.color = 0xB059D3;
                }
                else
                    LevelIcon.color = 0x771598;
            }

            if (SelectBackground != null)
            {
                SelectBackground.LateDestroy();

                bool changed = false;
                for (int i = 0; i < 3; i++)
                {
                    Vec2 pos = new Vec2(400, i * 202 + 345);
                    Vec2 dimentions = new Vec2(1400, 177);
                    Vec2 mousepos = Input.getMouseScreenPosition();
                    if (!(pos.x < mousepos.x && pos.x + dimentions.x > mousepos.x
                        && pos.y < mousepos.y && pos.y + dimentions.y > mousepos.y))
                        continue;
                    changed = true;
                    Sprite s = (Sprite)AddChild(new Sprite($"assets/UI/LevelSelect{i + 1}.png", true, false));
                    s.centerOrigin();
                    s.Position = new Vec2(960, 600);
                    s.width = 1336;
                    s.height = 800;
                    UILayer.AddChild(s);
                    SelectBackground = s;
                    if (Input.GetMouseButtonUp(0))
                        ToLevels(i * 10);
                }
                if (!changed)
                {
                    Sprite s = (Sprite)AddChild(new Sprite("assets/UI/LevelSelectBase.png", true, false));
                    s.centerOrigin();
                    s.Position = new Vec2(960, 600);
                    s.width = 1336;
                    s.height = 800;
                    UILayer.AddChild(s);
                    SelectBackground = s;
                }
            }
        }
        public override void Clear()
        {
            foreach (EasyDraw level in levels)
            {
                level.LateDestroy();
            }
            if (SelectBackground != null)
                SelectBackground.LateDestroy();
            stopped = true;
        }
    }
}
