using Godot;
using System;

public partial class Camera3dhorror : Camera3D
{
	[Export] public float MouseSensitivity = 0.002f;

    private CharacterBody3D Vinyl;
    private float pitch = 0.0f;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured; //I think cursor location

        Vinyl = GetParent<CharacterBody3D>();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel"))
        {
            Input.MouseMode = Input.MouseMode == Input.MouseModeEnum.Captured 
                ? Input.MouseModeEnum.Visible 
                : Input.MouseModeEnum.Captured;
        }

        if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured) //I stole this code
        {
            if (Vinyl != null)
            {
                Vinyl.RotateY(-mouseMotion.Relative.X * MouseSensitivity);
            }

            pitch -= mouseMotion.Relative.Y * MouseSensitivity;
            pitch = Mathf.Clamp(pitch, Mathf.DegToRad(-89f), Mathf.DegToRad(89f));

            Vector3 camRot = Rotation;
            camRot.X = pitch;
            Rotation = camRot;
        }
    }
}
