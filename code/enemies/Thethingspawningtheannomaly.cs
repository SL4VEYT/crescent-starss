using Godot;
using System;

public partial class Thethingspawningtheannomaly : Area3D

{
	[Export] public Area3D thething { get; set; }
	[Export] public Anomally_get_to_horror a;
	public override void _Ready()
	{
        BodyEntered += OnBodyEntered;
	}
	private void OnBodyEntered(Node3D body)
	{

	if(body.IsInGroup("player"))

		{
			a.aggroed = true;
			thething.Monitoring = true;
			thething.Visible = true;
		}

	}

	}
