using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
public partial class CombatManager : Node3D
{
	[ExportGroup("Light Attack")]
    [Export] private Area3D light_attack_hitbox;
    [Export] private CollisionShape3D light_attack;

	[ExportGroup("Heavy Attacks")]
    [Export] private Area3D Uppercut_hitbox;
    [Export] private CollisionShape3D Uppercut;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
