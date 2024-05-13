using System;
using GXPEngine;

public class MainGame : Game {
	public MainGame() : base(1920, 1080, false, true, 1600, 900)
	{
		targetFps = 50;
		AddChild(new Scene());
    }

	// For every game object, Update is called every frame, by the engine:
	void Update() {
		// Empty
	}

	static void Main()
	{
		new MainGame().Start();
	}
}