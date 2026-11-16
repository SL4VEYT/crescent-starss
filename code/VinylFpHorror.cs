using Godot;
using System;

public partial class VinylFpHorror : CharacterBody3D
{
	[Export] public float Speed = 5.0f;
	[Export] public float SprintMultiplier = 2.0f;
	public const float JumpVelocity = 4.5f;

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

	float currentSpeed = Speed;
	if (Input.IsKeyPressed(Key.Shift))
{
	GD.Print("you are pressing shift");
    currentSpeed *= SprintMultiplier;
}
		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * currentSpeed;
			velocity.Z = direction.Z * currentSpeed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed); //might get changed if it causes issues
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
