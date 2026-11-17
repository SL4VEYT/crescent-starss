using Godot;
using System;

public partial class Anomally_get_to_horror : Area3D
{
	[Export(PropertyHint.File, "*.tscn")] 
    public string TargetScenePath; 

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }
	private void OnBodyEntered(Node3D body)
{
    if (body.IsInGroup("player"))
    {
        Error result = GetTree().ChangeSceneToFile("res://leveldesign/horrorsegment.tscn");
	}

}

}
