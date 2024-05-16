using System;
using GXPEngine;

public class MainGame : Game {
    public static MainGame singleton;

	Scene scene;
    int levelIndex = 0;
    int lastLevel = 30;

    public MainGame() : base(1920, 1080, false, true, 1600, 900)
    {
        singleton = this;
        //targetFps = 50;
        AddChild(scene = new LevelSelect(10, 16));
    }

    void ChangeLevel()
    {
        if ( levelIndex != lastLevel)
        {
            levelIndex++;
			AddChild(scene = new Level(levelIndex));
        } else {
            Console.WriteLine("idk"); // Wait on team to know what end screen should be
        }
    }

    public static void OpenLevelSelect()
    {
        singleton.AddChild(new LevelSelect(10, 16));
    }

	// For every game object, Update is called every frame, by the engine:
	void Update() {
        if ( Input.GetKeyDown(Key.ENTER) || scene.SceneIsOver()) ChangeLevel();  // replace with other functionality later
	}

	static void Main()
	{
		new MainGame().Start();
	}
}