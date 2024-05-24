using Godot;
using System;

public partial class HurtBox : Area2D, IDamageable
{
	[Signal] public delegate void DamagedEventHandler(Damage damage);

	public void TakeDamage(Damage damage) {
		EmitSignal(SignalName.Damaged, new Damage());
	}
}
