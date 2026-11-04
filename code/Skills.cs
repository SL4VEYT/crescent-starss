using Godot;
using System;

public partial class Skills : Node3D
{
	 [Export] public Marker3D fireballSpawnPoint;
	private PackedScene fireballScene = GD.Load<PackedScene>("res://prefabs/fireball.tscn");

	public override void _Process(double delta)
	{
		 if (Input.IsActionJustPressed("Ability1"))
  		  {
			
			ShootFireball();
	   	  }
	
	}
	private void ShootFireball()
	{
		GD.Print("Fuck You");
		if (fireballScene == null || fireballSpawnPoint == null) return;

		Node3D fireballInstance = (Node3D)fireballScene.Instantiate();

		// that's a problem for future me

		fireballInstance.GlobalPosition = fireballSpawnPoint.GlobalPosition;

		GetTree().CurrentScene.AddChild(fireballInstance);
	}
}
