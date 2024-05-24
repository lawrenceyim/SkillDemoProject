using Godot;
using System;

public partial class PlayerMovement : Node {
	[Export] private CharacterBody2D characterBody2D;
	[Export] private PlayerAnimation playerAnimation;
	private float movementSpeed = 50;
	private Vector2 movementVector = new Vector2(0, 0);

	public void MoveByVector2(Vector2 vector) {
		characterBody2D.Velocity = vector.Normalized() * movementSpeed;
		characterBody2D.MoveAndSlide();
		playerAnimation.PlayWalkingAnimationByVector2(vector);
	}

	public void ResetVelocity() {
		characterBody2D.Velocity = new Vector2(0, 0);
	}
}
