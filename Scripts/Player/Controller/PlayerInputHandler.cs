using Godot;
using System;

public partial class PlayerInputHandler : Node, IInputHandler {
	[Signal] public delegate void InteractionEventHandler();
	[Signal] public delegate void MovementVectorEventHandler(Vector2 vector);
	private PlayerMovement playerMovement;
	private PopupManager popupManager;
	private bool movementPaused = false;
	
	public override void _Ready() {
		Player player = (Player) GetParent();
		playerMovement = player.GetPlayerMovement();
		popupManager = (PopupManager) GetTree().Root.GetNode("PopupManager");
		popupManager.Dialogue += SetMovementPause;
	}

	public override void _Process(double delta) {
		HandleInput();
	}

	public void HandleInput() {
		if (Input.IsActionJustPressed("Interact")) {
			EmitSignal(SignalName.Interaction);
		}

		if (movementPaused) {
			return;
		}

		Vector2 movementVector = new Vector2(0, 0);
		if (Input.IsActionPressed("MoveLeft")) {
			movementVector.X -= 1;
		}
		if (Input.IsActionPressed("MoveRight")) {
			movementVector.X += 1;
		}
		if (Input.IsActionPressed("MoveUp")) {
			movementVector.Y -= 1;
		}
		if (Input.IsActionPressed("MoveDown")) {
			movementVector.Y += 1;
		}
		playerMovement.MoveByVector2(movementVector);
		if (!movementVector.IsZeroApprox()) {
			EmitSignal(SignalName.MovementVector, movementVector);
		}
	}

	public void SetMovementPause(bool paused) { 
		movementPaused = paused;
	}
}
