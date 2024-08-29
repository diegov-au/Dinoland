using Godot;
using System;

public class MovingClouds : Sprite
{

	[Export]
	private int speed = -50;

	
	
	Camera2D camera = new Camera2D();

	float cameraZoom = 1;

	public float xExitOffset=0;

	public float xEnterOffset=0;

	float spriteWidth;

	Viewport root;
	
	public override void _Ready()
	{
	root = GetTree().Root;

	cameraZoom = 1/root.GetViewport().CanvasTransform.x.x; //gets the view port canvas zoom ratio

	spriteWidth = Texture.GetSize().x;

	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	
//	var scene = (Node2D)GetTree().Root.FindNode("Level*",true,false);

	
	var screenPos = (root.CanvasTransform.origin *-1) * cameraZoom; // x,y start position of view port ; *1 to get positive value

	var screenRect = (root.GetVisibleRect().Size) * cameraZoom;   // GetViewportRect().Size.x) * cameraZoom;

 	var spriteMoveOffset = 1 * speed * delta;
 	
	Position = new Vector2(Position.x + spriteMoveOffset, Position.y); 

	if((Position.x + spriteWidth) < screenPos.x  + xExitOffset)
	{
		Position = new Vector2(screenPos.x + screenRect.x + xEnterOffset , Position.y);
	}
	
  }
}
