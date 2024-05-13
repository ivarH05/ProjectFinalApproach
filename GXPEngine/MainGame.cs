using System;
using GXPEngine;

public class MainGame : Game {
	public MainGame() : base(1920, 1080, false, true, 1600, 900)
	{
		targetFps = 50;
		AddChild(new Scene());
    }

        int levelIndex = 1;
        int lastLevel = 3;

		void ChangeLevel()
        {
            if ( levelIndex != lastLevel)
            {
                levelIndex++;
				AddChild(new Scene(true, levelIndex));
            } else {
                Console.WriteLine("idk"); // Wait on team to know what end screen should be
            }
        }

	// For every game object, Update is called every frame, by the engine:
	void Update() {
        if ( Input.GetKeyDown(Key.ENTER) ) ChangeLevel();  // replace with other functionality later
	}

	static void Main()
	{
		new MainGame().Start();
	}
}