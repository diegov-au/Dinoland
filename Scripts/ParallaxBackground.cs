/// Attach this script to a Sprite then set the parallax effect strength: 0 = do not move up with player and 1 is move with player
/// 
/// Sprite must equal screen size to avoid texture popups
///
///
///

using Godot;
using System;

public class ParallaxBackground : Sprite
{
	Camera2D camera = new Camera2D();
	float cameraZoom = 1;
	Sprite sprite2 = new Sprite();
	
	[Export]
	public float parallaxEffect = 0;

	[Export]
	public bool Mirror = true;

	float startPos, startPos2, spriteWidth;
	
	Viewport root;
	
	public override void _Ready()
	{
	root = GetTree().Root;
	cameraZoom = 1/root.GetViewport().CanvasTransform.x.x; //gets the view port canvas zoom ratio

	startPos = Position.x;
	spriteWidth = Texture.GetSize().x * Scale.x;

		if(Mirror)
		{
	//create duplicate texture
		sprite2.Name = Name+"2";
		sprite2.Texture = Texture;
		sprite2.Centered = false;
		sprite2.Scale = Scale;
		sprite2.ZIndex = ZIndex;
		sprite2.Position = new Vector2(startPos + Texture.GetSize().x*Scale.x, Position.y);
		GetParent().CallDeferred("add_child",sprite2);

		startPos2 = sprite2.Position.x;
		}

	}


  public override void _Process(float delta)
  {
	

	 // var camera = GetTree().Root.GetNode<Camera2D>("Level1/Player/Camera2D");

	  //var cameraPos = camera.GetCameraPosition();

	  //cameraPos -= camera.GetViewportRect().Size; //.Size/2 *cameraZoom;

	var screenPos = (root.CanvasTransform.origin *-1) * cameraZoom; // we will use the screen viewport
	var cameraPos = screenPos;

	float dist = cameraPos.x  * parallaxEffect;
	float temp = cameraPos.x  * (1-parallaxEffect);

	
	Position = new Vector2(startPos + dist, Position.y);
	
	if(temp > startPos + spriteWidth)
		startPos += spriteWidth*2;
	else if ((temp < startPos-spriteWidth)  && temp > 0)
		{
			startPos -= spriteWidth*2;
		}

	if(Mirror)
	{
	sprite2.Position = new Vector2(startPos2 + dist, sprite2.Position.y);

	if(temp > startPos2 + spriteWidth)
		startPos2 += spriteWidth*2;
	else if ((temp < startPos2-spriteWidth)  && temp > 0)
		{
			startPos2 -= spriteWidth*2;
		}
	}
	
  }
}
