using System;
using GXPEngine;

public class MainGame : Game {
    public static MainGame singleton;

	Scene scene;
    public int levelIndex = 0;
    int lastLevel = 30;

    public MainGame() : base(1920, 1080, false, true, 1600, 900)
    {
        singleton = this;
        //targetFps = 50;
        AddChild(scene = new MainMenu());
    }

    public void ChangeLevel()
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
        singleton.AddChild(new LevelSelect(10, 30));
    }

	// For every game object, Update is called every frame, by the engine:
	void Update()
    {
        if (Input.GetKeyDown(Key.ESCAPE))
            AddChild(scene = new MainMenu());
    }

	static void Main()
	{
		new MainGame().Start();
	}
}