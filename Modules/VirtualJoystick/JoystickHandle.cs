using Godot;
using System;

public class JoystickHandle : TouchScreenButton
{

Vector2 handleRadius;
float joystickBoundry;
float eventDistanceFromCenter = 0;
int joyReturnToCenterSpeed = 20;
int dragging = -1;
int threshold = 10;

bool constrainYAxis = false;

Vector2 getHandlePosition ()
	{
	return Position + handleRadius;
	}
	
public override void _Ready()
	{
	
	handleRadius = new Vector2(Normal.GetSize().x/2,Normal.GetSize().y/2);

	joystickBoundry = GetParent<Sprite>().GetRect().Size.x/2;
	
	// center button
	Position  -= handleRadius;

	}

public override void _Process(float delta)
	{
		if(dragging==-1)
		{
			var positionDifference = new Vector2(0,0) - handleRadius - Position;
			Position += positionDifference * joyReturnToCenterSpeed * delta;
		}

	}
public override void _Input(InputEvent @event)
	{
		

		if(@event is InputEventScreenDrag eventScreenDrag )
			{
				eventDistanceFromCenter = (eventScreenDrag.Position - (GetParent<Sprite>().Position) + GetParent().GetParent<Panel>().RectPosition).Length();	
				if(eventDistanceFromCenter <= joystickBoundry * GlobalScale.x || eventScreenDrag.Index == dragging)
				{
					GlobalPosition = eventScreenDrag.Position - handleRadius * GlobalScale.x;
					if(constrainYAxis)
						Position = new Vector2(Position.x, -handleRadius.y);

					if(getHandlePosition().Length() > joystickBoundry)
					{
					Position = getHandlePosition().Normalized() * joystickBoundry - handleRadius;
					}
					dragging = eventScreenDrag.Index;
				}		
				
			}
		if (@event is InputEventScreenTouch eventScreenTouch && @event.IsPressed())
			{
				eventDistanceFromCenter = (eventScreenTouch.Position - (GetParent<Sprite>().Position + GetParent().GetParent<Panel>().RectPosition)).Length();	
				
				if(eventDistanceFromCenter <= joystickBoundry * GlobalScale.x  || eventScreenTouch.Index == dragging)
				{
					GlobalPosition = eventScreenTouch.Position - handleRadius * GlobalScale.x;
					
					if(constrainYAxis)
						Position = new Vector2(Position.x, -handleRadius.y);

					if(getHandlePosition().Length() > joystickBoundry)
					{
					Position = getHandlePosition().Normalized() * joystickBoundry - handleRadius;
					}
					dragging = eventScreenTouch.Index;
				}	
			}
		
		//clear drag

		if (@event is InputEventScreenTouch eventScreenTouch1 && !@event.IsPressed() &&  eventScreenTouch1.Index == dragging)
		{
			dragging = -1;
		}


		}

public Vector2 getJoyPos()
	{
		if(getHandlePosition().Length() > threshold)
			return getHandlePosition().Normalized();
		return new Vector2(0,0);
	}

public void setConstrainYAxis(bool set)
	{
		constrainYAxis = set;

	}
}
