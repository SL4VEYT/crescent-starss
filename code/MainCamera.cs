using Godot;
using System;

public partial class MainCamera : Camera3D
{
	[Export] private float offsetDistance = 2.0f; 
    [Export] private float lerpSpeed = 5.0f;

    private float _targetHOffset = 0.0f;

    public override void _PhysicsProcess(double delta)
    {
        float inputX = Input.GetAxis("ui_left", "ui_right");

        if (inputX > 0)
        {
            _targetHOffset = offsetDistance;
        }
        else if (inputX < 0)
        {
            _targetHOffset = -offsetDistance;
        }

        HOffset = Mathf.Lerp(HOffset, _targetHOffset, (float)delta * lerpSpeed);
    }
}
