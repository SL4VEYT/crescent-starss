using Godot;
using System;

public partial class HealthManager : Node3D
{
	[Export] public int MaxHP;
	private int CurrentHP;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
