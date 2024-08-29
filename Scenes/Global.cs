using Godot;
using System;

/* 
Global Script function:

- Maintains various global various including Lives, Coins, Items
- Maintains Coins for current Level and Total Game
- Instantiates AudioPlayer for Level Music
- Methods for Adding/Removing lives and coins and updating HUD
- managers global timer 


*/

public class Global : Node
{
	public int maxLives = 3;
	public int currentLives = 0;
	public int healthLevel = 100;
	public int currentStage = 1;
	public int levelCoins = 0; //coins for current level
	public int totalCoins = 0; //coins acrued across entire game

	public int[] eggsCollected = new int [] {0,0,0};
	public Node currentLevel;
	public HUD hud;
	public Player player;
	
	public override void _Ready()
	{
		// init routine

		currentLives = maxLives;
	
		//start audio player
		var audioPlayer = new AudioStreamPlayer();
		AddChild(audioPlayer);
		audioPlayer.Stream = ResourceLoader.Load<AudioStream>("res://Assets/audio/theme.ogg");
		audioPlayer.VolumeDb= -6;
		audioPlayer.Play();

		//load stage

	}
	
	public void SubtractLife()
	{
		if (currentLives!=0)
			currentLives += -1;
		hud.RefreshHudLives();
	}
	
	public void ResetLives()
	{
		   currentLives = maxLives;
	}
		
	public void ResetEggCount()
	{
		  eggsCollected = new[] {0,0,0};

	}
	public void AddCoin()
	{
		levelCoins+=1;
		hud.RefreshHudCoins();
		
	}

	public void AddEgg(int egg)
	{
		eggsCollected[egg] =1;
		hud.RefreshHudEggs();
		
	}
	
	public void ResetLevelCoins()
	{
		levelCoins = 0;
		hud.RefreshHudCoins();
		
	}

	public void UpdateHealthBar(int amount)
	{
		healthLevel += amount;
		
		if(healthLevel<0)
			healthLevel=0;

		hud.RefreshHealthBar();
	}
	
	async public void ChangeGameState(string message, bool resetLives, bool resetLevel, string scenePath)
	{
		//used to start, stop, or end level
		//also display a message in the hud
		
		var timer = (Timer)GetTree().Root.FindNode("LevelTimer",true,false);

		timer.Paused = true;

		player.playerEnabled = false;
		player.StopAnimation();

		hud.ShowHudMessage(message, 2);
		await ToSignal(GetTree().CreateTimer(2.0f), "timeout");
		
		healthLevel = 100;

		if(resetLives)
		{
			ResetLives();
			ResetEggCount();
		}

		if(resetLevel && (scenePath!= null))
			{GetTree().ChangeScene(scenePath);
		
	}

	
	
	}
}
