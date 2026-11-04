using Godot;
using System;

public partial class Explosion : Area3D
{
	private void _on_timer_timeout()
{

    QueueFree();
}
}
