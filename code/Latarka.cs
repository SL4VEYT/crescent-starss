using Godot;
using System;

public partial class Latarka : SpotLight3D
{
	public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo && keyEvent.Keycode == Key.F) //not sure why this is so long, but eh
        {
            Visible = !Visible;
        }
    }
}
