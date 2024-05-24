using Godot;
public partial class Player : Node, IDamageable {
	[Export] private CharacterBody2D characterBody;
	[Export] private HurtBox[] hurtBoxes;
	[Export] private int health = 10;

	public override void _Ready() {
		foreach (HurtBox hurtBox in hurtBoxes) {
			hurtBox.Damaged += TakeDamage;
		}
	}

	public void TakeDamage(Damage damage) {
		health -= damage.GetDamageAmount();
		if (health <= 0) {
			// Add death
		}
	}
}
