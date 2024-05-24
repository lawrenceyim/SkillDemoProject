using Godot;
using System;

// Add new damage types by adding them to this enum
public enum DamageType {
	None = 0,
}

public partial class Damage : Resource {
	private DamageType type;
	private int damageAmount;

	public Damage() { }
	public Damage(DamageType type, int damageAmount) {
		this.type = type;
		this.damageAmount = damageAmount;
	}

	public void SetDamageAmount(int damageAmount) {
		this.damageAmount = damageAmount;
	}

	public int GetDamageAmount() {
		return damageAmount;
	}

	public void SetDamageType(DamageType type) {
		this.type = type;
	}

	public DamageType GetDamageType() {
		return type;
	}
}
