using Godot;
using System;

public partial class Monkey : Node3D
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	RotateObjectLocal(Vector3.Right, Mathf.DegToRad(15.0f) * (float)delta); 
    RotateObjectLocal(Vector3.Up, Mathf.DegToRad(3.0f) * (float)delta);
	}
}
