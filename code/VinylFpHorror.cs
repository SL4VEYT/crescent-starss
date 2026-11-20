using Godot;
using System;
using System.Diagnostics;

public partial class VinylFpHorror : CharacterBody3D
{
	[Export] public float Speed = 5.0f;
	float currentSpeed;
	[Export] public float CrouchSpeed = 3.0f;
	[Export] public float SprintMultiplier = 2.0f;
	public const float JumpVelocity = 4.5f;
	[Export] public CollisionShape3D Hitbox_standing;

 	[ExportGroup("CameraHeight&shit")]
	[Export] public float StandHeight = 1.6f;
    [Export] public float CrouchHeight = 0.8f;
    [Export] public float TransitionSpeed = 8.0f;
	private bool crouching = false;
	public Camera3D cam;
    public override void _Ready()
    {
		currentSpeed = Speed;
		cam = GetNode<Camera3D>("Camera3D");
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

	
	if (Input.IsKeyPressed(Key.Shift))
{
	crouching = false;
	Hitbox_standing.Disabled = false;
    //currentSpeed *= SprintMultiplier;
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
		if (Input.IsActionJustPressed("crouch") && IsOnFloor())
		{
			if(crouching == false)
			{
				currentSpeed = CrouchSpeed;
				crouching = true;
				Hitbox_standing.Disabled = true;
			}

			else
			{
				currentSpeed = Speed;
				crouching = false;
				Hitbox_standing.Disabled = false;
			}
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
			velocity.X = Mathf.MoveToward(Velocity.X, 0, currentSpeed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, currentSpeed); //might get changed to currentspeed if it causes issues
		}

	float targetY = crouching ? CrouchHeight : StandHeight;
	Vector3 camPos = cam.Position;
	camPos.Y = Mathf.MoveToward(camPos.Y, targetY, TransitionSpeed * (float)delta); //stolen camera lines
	cam.Position = camPos;

		Velocity = velocity;
		MoveAndSlide();
	}
}
