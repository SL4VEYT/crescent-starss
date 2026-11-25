using Godot;
using System;

public partial class Menu : AnimatedSprite2D
{
	[Export] public float Amplitude = 8f;
    [Export] public float Speed = 1.5f;

    private float _startY;
    private float _time;

    public override void _Ready()
    { 
        _startY = Position.Y;
    }

    public override void _Process(double delta)          //I've stolen all of this, dont ask me, I dont get physics
    {
        _time += (float)delta * Speed;
        Vector2 pos = Position;
        pos.Y = _startY + Mathf.Sin(_time) * Amplitude;
        Position = pos;
    }
}
