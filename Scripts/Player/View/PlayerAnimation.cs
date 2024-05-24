using Godot;
using System;

public partial class PlayerAnimation : Node {
	public enum AnimationName {
		WALKING_UP = 0,
		WALKING_RIGHT = 1,
		WALKING_DOWN = 2,
		WALKING_LEFT = 3,
		IDLE_UP = 4,
		IDLE_RIGHT = 5,
		IDLE_DOWN = 6,
		IDLE_LEFT = 7,
	}

	[Export] AnimatedSprite2D sprite;
	AnimationName idleAnimation = AnimationName.IDLE_DOWN;

	public void PlayWalkingAnimationByVector2(Vector2 vector) {
		if (vector.IsZeroApprox()) {
			sprite.Play(idleAnimation.ToString());
		} else if (vector.X > 0) {
			sprite.Play(AnimationName.WALKING_RIGHT.ToString());
			idleAnimation = AnimationName.IDLE_RIGHT;
		} else if (vector.X < 0) {
			sprite.Play(AnimationName.WALKING_LEFT.ToString());
			idleAnimation = AnimationName.IDLE_LEFT;
		} else if (vector.Y < 0) {
			sprite.Play(AnimationName.WALKING_UP.ToString());
			idleAnimation = AnimationName.IDLE_UP;
		} else if (vector.Y > 0) {
			sprite.Play(AnimationName.WALKING_DOWN.ToString());
			idleAnimation = AnimationName.IDLE_DOWN;
		}
	}
}
