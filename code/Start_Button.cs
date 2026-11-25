using Godot;
using System;

public partial class Start_Button : Button
{
	[Export] public string Scene;

    public override void _Ready()
    {
        Pressed += () => GetTree().ChangeSceneToFile(Scene);
    }
}
