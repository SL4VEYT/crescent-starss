using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
public partial class CombatManager : Node3D
{
	[ExportGroup("Light Attack")]
    [Export] private Area3D light_attack_hitbox;
    [Export] private CollisionShape3D light_attack;

	[ExportGroup("Heavy Attacks")]
    [Export] private Area3D Uppercut_hitbox;
    [Export] private CollisionShape3D Uppercut;
	private int comboStep = 0; //0 being idle
    private double comboTimer = 0.0; //timer for reseting the combo
    private const double ComboWindow = 0.8;  //how much time to keep the combo going

    private HashSet<Node3D> _hitTargetsThisSwing = new HashSet<Node3D>(); //nofuckingclue
    private int _currentDamage = 1; //stored damage vaariable

    public override void _Ready()
    {
        light_attack_hitbox.BodyEntered += OnEnemyHit; //better than checking "if" constantly
        Uppercut_hitbox.BodyEntered += OnEnemyHit;
    }

	private void OnEnemyHit(Node3D body) //no clue yet
    {
        if (body == Owner || _hitTargetsThisSwing.Contains(body)) return;

        if (body.HasMethod("TakeDamage"))
        {
            _hitTargetsThisSwing.Add(body);
            body.Call("TakeDamage", _currentDamage);
        }
	}

	public void ResetCombo() //self explanatory
    {
        comboStep = 0;
        comboTimer = 0;
        light_attack.Disabled = true;
        Uppercut.Disabled = true;
    }
}
