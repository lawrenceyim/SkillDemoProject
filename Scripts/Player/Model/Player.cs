using Godot;

public partial class Player : CharacterBody2D, IDamageable {
	[Export] private CharacterBody2D characterBody2D;
	[Export] private PlayerAnimation playerAnimation;
	[Export] private PlayerInputHandler playerInputHandler;
	[Export] private PlayerMovement playerMovement;
	[Export] private AnimatedSprite2D animatedSprite2D;
	[Export] private HurtBox[] hurtBoxes;
	[Export] private RayCast2D rayCast2D;
	[Export] private Line2D debugLine;
	[Export] private int health = 10; // Probably should make a separate Health class
	private Vector2 lastMovementVector = new Vector2(0, 0);

	public override void _Ready() {
		debugLine.Visible = false;
		foreach (HurtBox hurtBox in hurtBoxes) {
			hurtBox.Damaged += TakeDamage;
		}
		playerInputHandler.Interaction += RaycastForInteractable;
		playerInputHandler.MovementVector += SetMovementVector;
	}

	public void TakeDamage(Damage damage) {
		health -= damage.GetDamageAmount();
		if (health <= 0) {
			// Add death
		}
	}

	public void SetMovementVector(Vector2 vector) {
		lastMovementVector = vector;
	}

	public void RaycastForInteractable() {
		float degrees = lastMovementVector.Angle();
		rayCast2D.Rotation = degrees;
		debugLine.Rotation = degrees;
		rayCast2D.ForceRaycastUpdate();
		GodotObject target = rayCast2D.GetCollider();
		if (target is CollisionObject2D collisionObject2D) {
			if (collisionObject2D.GetParent() is IInteractable parent) {
				parent.InteractWith();
				playerMovement.MoveByVector2(new Vector2(0, 0));
			}
		} else if (target is TileMap tileMap) {
			Vector2 collisionPoint = rayCast2D.GetCollisionPoint() - rayCast2D.GetCollisionNormal();
			Vector2I tileCoords = tileMap.LocalToMap(collisionPoint);
			TileData tileData = tileMap.GetCellTileData(1, tileCoords);
			GD.Print(collisionPoint + " " + tileCoords + " " + tileData);
			if ((bool) tileData?.GetCustomDataByLayerId(0)) {
				GD.Print("IS WATER");
			}
		}

	}


	#region Getters for node dependencies
	public CharacterBody2D GetCharacterBody2D() {
		return characterBody2D;
	}

	public PlayerAnimation GetPlayerAnimation() {
		return playerAnimation;
	}

	public AnimatedSprite2D GetAnimatedSprite2D() {
		return animatedSprite2D;
	}

	public PlayerInputHandler GetPlayerInputHandler() {
		return playerInputHandler;
	}

	public PlayerMovement GetPlayerMovement() {
		return playerMovement;
	}
	#endregion
}
