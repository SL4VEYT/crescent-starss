using Godot;
using System;
using System.Collections.Generic;
public partial class Fireball : Area3D
{
  [Export] public Sprite3D sprite;
  [Export] public float Speed = 15.0f;
  [Export] public int currentDamage;
   private Vector3 direction = Vector3.Right;
   private PackedScene explosionScene = GD.Load<PackedScene>("res://prefabs/explosion.tscn"); //reference to the explosion
   private HashSet<Node3D> _hitTargetsThisSwing = new HashSet<Node3D>(); //no fucking idea, but it worked in the other script so now its here
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

                
                direction = (target - GlobalPosition).Normalized(); //I think something in here causes an error, but if I fix it, the fireball stops working

                
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
        if (body.IsInGroup("destroyable_obj"))
    {
        body.QueueFree();
    }
        if (body.IsInGroup("enemy"))
    {
            if (body.HasMethod("TakeDamage")) //let's hope that works
        {
            _hitTargetsThisSwing.Add(body); //prevents multihit
            body.Call("TakeDamage", currentDamage);
        }
    } 
        BOOM();
        QueueFree();
    }

    private void BOOM() //function for spawning the explosion
    {
        if (explosionScene != null)
        {
            Node3D explosionInstance = explosionScene.Instantiate<Node3D>();
            GetTree().CurrentScene.AddChild(explosionInstance);
            explosionInstance.GlobalPosition = GlobalPosition;
        }
    }
    }
