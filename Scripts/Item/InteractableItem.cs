using System;
using Godot;

public partial class InteractableItem : BaseItem, IInteractable
{
	[Signal] public delegate void PopupBoxEventHandler(bool display, string message);

	[Export] protected Area2D interactionArea;
	[Export] protected Sprite2D interactionSprite;
	[Export] protected PlayerInputHandler playerInputHandler;

	private bool canInteractWith = false;

	public override void _Ready() {
		interactionArea.BodyEntered += AddInteractionIndicator;
		interactionArea.BodyExited += RemoveInteractionIndicator;
		//playerInputHandler.Interaction += InteractWith;

		interactionSprite.Visible = false;
	}

	private void RemoveInteractionIndicator(Node2D body)
	{
		if (IsPlayer((Node) body)) {
			interactionSprite.Visible = false;
			canInteractWith = false;
			EmitSignal(SignalName.PopupBox, true, "");
		}
	}

	private void AddInteractionIndicator(Node2D body)
	{
		if (IsPlayer((Node) body)) {
			interactionSprite.Visible = true;
			canInteractWith = true;
			EmitSignal(SignalName.PopupBox, true, "The mailbox is empty");
		}
	}

	public void InteractWith()
	{
		if (canInteractWith) {
			GD.Print("Interacted with mail box");
		}
	}

	private bool IsPlayer(Node node) {
		foreach (Node child in node.GetChildren()) {
			if (child is Player) {
				return true;
			}
		}
		return false;
	}
}
