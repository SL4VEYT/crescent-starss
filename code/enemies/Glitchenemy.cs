using Godot;
using System;

public partial class Glitchenemy : CharacterBody3D
{
	[Export] private float speed = 6.0f;
    private Vector3 direction = new Vector3(1, 1, 0).Normalized(); //no clue
    [Export] private int Damage = 200;
	private void OnDamageAreaBodyEntered(Node3D body)
    {  
        if (body.IsInGroup("player"))
        {
            Node healthManager = body.GetNodeOrNull("HealthManager");
            healthManager.Call("TakeDamage", Damage);
        }
    }
	public override void _PhysicsProcess(double delta)
    {
        KinematicCollision3D collision = MoveAndCollide(direction * speed * (float)delta);

        if (collision != null)
        {
            
            direction = direction.Bounce(collision.GetNormal()).Normalized(); //????
            direction.Z = 0; //lock
			
        }
    }
}
