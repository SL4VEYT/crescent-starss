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
    private int _currentDamage = 5; //stored damage vaariable

    public override void _Ready()
    {
        light_attack_hitbox.BodyEntered += OnEnemyHit; //better than checking "if" constantly
        Uppercut_hitbox.BodyEntered += OnEnemyHit;
    }

public override void _Process(double delta)
    {
        if (comboStep > 0)
        {
            comboTimer -= delta; //delta = time
            if (comboTimer <= 0)
            {
                ResetCombo(); //self explanatory
            }
        }
    }

    public async void ExecuteAttack()
    {
        comboTimer = ComboWindow;
        comboStep++;
        if (comboStep > 3) comboStep = 1;

        switch (comboStep)
        {
            case 1:
                _currentDamage = 5; //change to damage state
                await PerformSwingAsync(light_attack, 150);
                break;

            case 2:
                _currentDamage = 5; //change to damage state
                
                light_attack.Disabled = true;
                await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

                await PerformSwingAsync(light_attack , 150);
                break;

            case 3:
                _currentDamage = 15; //change to damage state

                light_attack.Disabled = true;

                await PerformSwingAsync(Uppercut, 250);
                ResetCombo();
                break;
        }
    }

    private async Task PerformSwingAsync(CollisionShape3D shapeToActivate, int activeDurationMs)
    {
        _hitTargetsThisSwing.Clear();
        shapeToActivate.Disabled = false;

        await Task.Delay(activeDurationMs);

        shapeToActivate.Disabled = true;
    }
	private void OnEnemyHit(Node3D body) //no clue yet
    {
        if (body == Owner || _hitTargetsThisSwing.Contains(body)) return; //makes it so that Vinyl does not hit himself

        if (body.HasMethod("TakeDamage"))
        {
            _hitTargetsThisSwing.Add(body); //prevents multihit
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
