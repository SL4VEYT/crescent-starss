using Godot;
using System;

public partial class Enemy : RigidBody3D
{
	[Export] private int health = 10;
    private PackedScene death = GD.Load<PackedScene>("res://particles/cardboard/Leparticle.tscn");
   
    public void TakeDamage(int amount) //called by CombatMaanaager
    {
        health -= amount;
        GD.Print($"Enemy hit for {amount} damage!");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (death != null)
        {
            Node3D particleInstance = death.Instantiate<Node3D>();
            GetTree().CurrentScene.AddChild(particleInstance);
           particleInstance.GlobalPosition = GlobalPosition;
        }
        Freeze = false;
        ApplyCentralImpulse(new Vector3(4.0f, 6.0f, 0.0f));
        QueueFree();

    }
}
