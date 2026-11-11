using Godot;
using System;

public partial class Enemy : RigidBody3D
{
	[Export] private int health = 10;

   
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
        Freeze = false;
        ApplyCentralImpulse(new Vector3(4.0f, 6.0f, 0.0f));
        QueueFree();
    }
}
