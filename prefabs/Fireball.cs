using Godot;
using System;

public partial class Fireball : Area3D
{
  [Export] public Sprite3D sprite;
  [Export] public float Speed = 15.0f;
   private Vector3 direction = Vector3.Right;
   private PackedScene explosionScene = GD.Load<PackedScene>("res://prefabs/explosion.tscn");
		public override void _Ready()
	{
        BodyEntered += OnBodyEntered;

		Camera3D camera = GetViewport().GetCamera3D();
        if (camera != null)
        {
            Vector2 mousePos = GetViewport().GetMousePosition();
            
            
            Plane plane = new Plane(Vector3.Back, 0); 
            Vector3? intersection = plane.IntersectsRay(camera.ProjectRayOrigin(mousePos), camera.ProjectRayNormal(mousePos));

            if (intersection.HasValue)
            {
                Vector3 target = intersection.Value;
                target.Z = GlobalPosition.Z; 

                
                direction = (target - GlobalPosition).Normalized();

                
                float angle = Mathf.Atan2(direction.Y, direction.X);
                Rotation = new Vector3(0, 0, angle);
            }
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition += direction * Speed * (float)delta;
	}

    private void OnBodyEntered(Node3D body)
    {
        if (body.IsInGroup("enemy"))
    {
        body.QueueFree();
    }
        BOOM();
        QueueFree();
    }

    private void BOOM()
    {
        if (explosionScene != null)
        {
          
            Node3D explosionInstance = explosionScene.Instantiate<Node3D>();
            GetTree().CurrentScene.AddChild(explosionInstance);
            explosionInstance.GlobalPosition = GlobalPosition;
        }
    }
    }
