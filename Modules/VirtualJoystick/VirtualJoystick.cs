using Godot;
using System;


public class VirtualJoystick : Control
{
	
[Export]
public bool constrainYAxis = true;

JoystickHandle jPos;
	
	public override void _Ready()
	{
	
	if(OS.GetName() != "Android") //|| OS.GetName() == "iOS")
			RectPosition = new Vector2(0,10000);
			
	
	
	
	jPos = GetNode<JoystickHandle>("JoyPanel/JoystickBackground/JoystickHandle");
	jPos.setConstrainYAxis(constrainYAxis);
	}

	public Vector2 GetJoystickPos()
	{
	
		return jPos.getJoyPos();	
	
	}

	public bool JumpButtonPressed()
	{
		var jButton1 = GetNode<TouchScreenButton>("ButtonPanel/JumpButton");

	
			if(jButton1.IsPressed())
				return true;
		
		return false;

	}
	

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
