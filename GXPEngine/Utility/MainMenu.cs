using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class MainMenu : Scene
    {
        public MainMenu() : base(true)
        {
        }
        private Sprite SelectBackground;

        void Update()
        {
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
                    Sprite s = (Sprite)AddChild(new Sprite($"assets/UI/MainMenu{i + 1}.png", true, false));
                    s.centerOrigin();
                    s.Position = new Vec2(960, 600);
                    s.width = 1336;
                    s.height = 800;
                    UILayer.AddChild(s);
                    SelectBackground = s;
                    if (Input.GetMouseButtonUp(0))
                        MainGame.OpenLevelSelect();
                }
                if (!changed)
                {
                    Sprite s = (Sprite)AddChild(new Sprite("assets/UI/MainMenuBase.png", true, false));
                    s.centerOrigin();
                    s.Position = new Vec2(960, 600);
                    s.width = 1336;
                    s.height = 800;
                    UILayer.AddChild(s);
                    SelectBackground = s;
                }
            }
        }
    }
}
