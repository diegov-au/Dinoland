using Godot;
using System;

/* 
Level Script function:

- Resets coins to zero 
- Show Level Start Message
- Enables Player Movement

*/

public class Level1 : Node2D
{
	[Export]
	public int levelTimerTime = 121;

	Global g; // used for glboal variable

	public async override void _Ready()
	{

	//initalize global
	g = GetNode<Global>("/root/Global");
	
	g.currentLevel = this;
	g.ResetLevelCoins();
	
	//set timer
	GetNode<Timer>("LevelTimer").WaitTime = levelTimerTime;
	GetNode<Timer>("LevelTimer").Start();
	GetNode<Timer>("LevelTimer").Paused = true;
	
	g.hud.ShowHudMessage("[center][wave]Collect All The Coins![/wave][/center]", 3);
	await ToSignal(GetTree().CreateTimer(2.0f), "timeout");
	
	var Player = GetNode<Player>("Player");
	Player.playerEnabled = true;

	//start timer
	GetNode<Timer>("LevelTimer").Paused = false;

	}
	
	

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		var timeRemaining = GetNode<Timer>("LevelTimer").TimeLeft;
		g.hud.UpdateTimer((int)timeRemaining);	
	}
	
	private void _on_LevelTimer_timeout()
	{
		g.SubtractLife();
		if(g.currentLives!=0)
			g.ChangeGameState("[center]Out Of Time![/center]", false, true, "res://Scenes/Level1.tscn");
		else
			g.ChangeGameState("[center]No Lives Left![/center]", true, true, "res://Scenes/Level1.tscn");
	}
	
	public void DeathZone()
	{
		g.healthLevel = 0;
		g.SubtractLife();
		
	
		if(g.currentLives!=0)
			g.ChangeGameState("[center]You Fell![/center]", false, true, "res://Scenes/Level1.tscn");
	else
			g.ChangeGameState("[center]No Lives Left![/center]", true, true, "res://Scenes/Level1.tscn");
	}

	
	public void LevelComplete()
	{
		g.ChangeGameState("[center]Level Complete![/center]", true, true, "res://Scenes/Level1.tscn");
	}
	
	

}






