using Godot;
using System;

public partial class Vinyl : CharacterBody3D
{
    [Export] public Sprite3D sprite;
    [Export] public AnimationPlayer animPlayer;

    [ExportGroup("Movement Settings")]
	[Export] public float Speed = 5.0f;
	[Export] public float JumpVelocity = 4.5f;

    [ExportGroup("Jump Thingies")]
    [Export] public float CoyoteTime = 0.15f;    
    [Export] public float JumpBufferTime = 0.15f;
  

    private float coyoteTimer = 0.0f;
    private float jumpBufferTimer = 0.0f;
   
	    public override void _PhysicsProcess(double delta)  //here it starts
	{
		Vector3 velocity = Velocity;
        velocity.Z = 0f;
        float fDelta = (float)delta; //?????

        if (IsOnFloor())
        {
            coyoteTimer = CoyoteTime; //reset to set variable
        }
        else
        {
            coyoteTimer -= fDelta;
            velocity += GetGravity() * fDelta;
        }

        if (Input.IsActionJustPressed("ui_accept")) //if jump pressed
        {
            jumpBufferTimer = JumpBufferTime; 
        }
        else
        {
            jumpBufferTimer -= fDelta; 
        }


        if (jumpBufferTimer > 0.0f && coyoteTimer > 0.0f)
        {
            velocity.Y = JumpVelocity;
            coyoteTimer = 0.0f; //anti-doublejump
            jumpBufferTimer = 0.0f;
        }

        float direction = Input.GetAxis("ui_left", "ui_right");

        if (direction != 0f)
        {
            velocity.X = direction * Speed;
            if (animPlayer != null)
            {
                animPlayer.Play("run_old"); 
            }
        }
		
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0f, Speed); //no clue
            if (animPlayer != null)
            {
                animPlayer.Play("RESET"); 
            }
        }

        

    Velocity = velocity;
        if (direction < 0f)
    {
            sprite.FlipH = true; // Face left
    }
        else if (direction > 0f)
    {
            sprite.FlipH = false; // Face right
    }

MoveAndSlide();
}

    
}
