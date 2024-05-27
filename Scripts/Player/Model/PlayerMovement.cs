using Godot;
public partial class PlayerMovement : Node {
	private CharacterBody2D characterBody2D;
	private PlayerAnimation playerAnimation;
	private float movementSpeed = 100;
	private Vector2 movementVector = new Vector2(0, 0);

	public override void _Ready() {
		Player player = (Player) GetParent();
		characterBody2D = player.GetCharacterBody2D();
		playerAnimation = player.GetPlayerAnimation();
	}

	public void MoveByVector2(Vector2 vector) {
		characterBody2D.Velocity = vector.Normalized() * movementSpeed;
		characterBody2D.MoveAndSlide();
		playerAnimation.PlayWalkingAnimationByVector2(vector);
	}

	public void ResetVelocity() {
		characterBody2D.Velocity = new Vector2(0, 0);
	}
}
