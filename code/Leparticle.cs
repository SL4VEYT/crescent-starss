using Godot;
using System;

public partial class Leparticle : Node3D
{
	public override void _Ready()
    {
        var rng = new RandomNumberGenerator();

        foreach (Node child in GetChildren())
        {
            if (child is RigidBody3D piece)
            {
                Vector3 burstDir = new Vector3(
                    rng.RandfRange(-10f, 10f),
                    rng.RandfRange(2f, 13f),
                    0f
                );
                piece.ApplyCentralImpulse(burstDir);
                piece.ApplyTorqueImpulse(new Vector3(0, 0, rng.RandfRange(-10f, 10f)));
            }
        }

        
        GetTree().CreateTimer(4.0f).Timeout += QueueFree; //clears
    }
}
