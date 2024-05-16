using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.LevelAssets.Other
{
    public class GameUI : UILayer
    {
        public static EasyDraw Objective = new EasyDraw(600, 200, false);
        public static EasyDraw Progress = new EasyDraw(600, 200, false);

        public GameUI()
        {
            Progress.Position = new Vec2(955, 680);
            Objective.Position = new Vec2(1000, 860);
            Progress.TextAlign(CenterMode.Center, CenterMode.Center);
            Progress.TextFont(Utils.LoadFont("Assets/WishMF.ttf", 56));
            Objective.TextFont(Utils.LoadFont("Assets/WishMF.ttf", 48));
            AddChild(Objective);
            AddChild(Progress);
        }

        public void Update()
        {
            Objective.ClearTransparent();
            Progress.ClearTransparent();
            Objective.Fill(255);
            Objective.Text(ObjectiveManager.GetObjectiveText(), 300, 100);
            Progress.Text(ObjectiveManager.GetProgress(), 300, 100);
        }
    }
}
