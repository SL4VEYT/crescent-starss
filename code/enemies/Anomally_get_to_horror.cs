using Godot;
using System;

public partial class Anomally_get_to_horror : Area3D
{
	[Export(PropertyHint.File, "*.tscn")] 
    public string TargetScenePath; 
    [Export] public float Accel = 10f;
    float _speed;
    public bool aggroed = false;
    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }
    public override void _PhysicsProcess(double delta)
    {
        if(aggroed == true)

        {
        _speed += Accel * (float)delta;
        Position += Vector3.Left * _speed * (float)delta;
        }
    }
    private void Chase()
    {
        
    }

	private void OnBodyEntered(Node3D body)
    {

    if (body.IsInGroup("player"))
    {
        Error result = GetTree().ChangeSceneToFile("res://leveldesign/horrorsegment.tscn");
	}

    }

}
