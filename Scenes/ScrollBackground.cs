using Godot;
using System;

public class ScrollBackground : Sprite
{
	
	[Export]	
	public float velocity= -2;
	
	private float spriteWidth = 0;

	public float xExitOffset=8;

	public float xEnterOffset=15;
	
	
	public override void _Ready()
	{
		
		spriteWidth = Texture.GetSize().x * Scale.x;
		
	}

	 public override void _Process(float delta)
  	{
		Position = new Vector2(Position.x+velocity, Position.y);
		reposition();
  	}
	
	
	void reposition()
	{
	//	var cameraTransform = GetParent().GetParent().GetNode<Camera2D>("Player/Camera2D").Transform;

		if (Position.x < -spriteWidth)
		//if( (Position.x + spriteWidth) < cameraTransform.origin.x)
			Position = new Vector2(Position.x + (2 * spriteWidth +xEnterOffset), Position.y);
	}
	
}
