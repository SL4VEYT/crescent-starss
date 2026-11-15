using Godot;
using System;

public partial class HealthManager : Node3D
{
	[Export] public int MaxHP = 200;
	private int CurrentHP;
	public override void _Ready()
	{
		CurrentHP = MaxHP; //yeah
	}

	private void TakeDamage(int amount)
	{
		CurrentHP -= amount; //"amount" is defined in the enemy script
        CurrentHP = Mathf.Clamp(CurrentHP, 0, MaxHP);

			if (CurrentHP <= 0)
        {
            Die(); //yeah
        }
	}
	

private void Die()
    {
        GD.Print("Vinyl died! You suck");

		//code goes here
    }
}
