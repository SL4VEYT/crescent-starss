using Godot;
using System;

public partial class Monkey : Node3D
{
	 [ExportGroup("ass")]
	 [Export] public float X;
	 [Export] public float Y;
	public override void _Process(double delta)
	{
	RotateObjectLocal(Vector3.Right, Mathf.DegToRad(X) * (float)delta); 
    RotateObjectLocal(Vector3.Up, Mathf.DegToRad(Y) * (float)delta);
	}
}
