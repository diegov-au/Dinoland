using Godot;
using System;

public class ParallaxCloud : ParallaxBackground
{

	Vector2 direction = new Vector2(1,0);
	
	float rotationSpeed = 0.4f;

	int speed = 0;
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	 ScrollOffset += direction * speed *delta;
	 direction = direction.Rotated(rotationSpeed * delta);
  }
}
