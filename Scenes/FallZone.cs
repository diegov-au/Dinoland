using Godot;
using System;

public class FallZone : Area2D
{
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
	private void _on_FallZone_body_entered(object body)
	{
	
	var g = (Global)GetNode("/root/Global");
	var level = (Level1)g.currentLevel;

	level.DeathZone();
	
	}
	
}
