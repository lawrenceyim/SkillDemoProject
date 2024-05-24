using System;
using System.Collections.Generic;
using Godot;

public partial class InteractableItem : BaseItem, IInteractable
{
	[Signal] public delegate void PopupBoxEventHandler(string message);
	[Export] protected PopupManager popupManager;
	[Export] protected Area2D interactionArea;
	[Export] protected Sprite2D interactionSprite;
	[Export] protected PlayerInputHandler playerInputHandler;
	private bool canInteractWith = false;
	private DialogueTree dialogueTree;

	public override void _Ready() {
		interactionArea.BodyEntered += AddInteractionIndicator;
		interactionArea.BodyExited += RemoveInteractionIndicator;
		playerInputHandler.Interaction += InteractWith;

		dialogueTree = new DialogueTree(new DialogueNode("Your mailbox is empty."));
		dialogueTree.OnQuestionSent += QuestionReceived;
		dialogueTree.OnDialogueSent += DialogueReceived;

		interactionSprite.Visible = false;
	}

	private void DialogueReceived(object sender, List<string> e) {
		foreach(string message in e) {
			GD.Print(message);
			popupManager.ShowPopupPanel();
			popupManager.ChangePopupLabelText(message);
		}
	}

	private void QuestionReceived(object sender, (string, List<string>) e) {
		throw new NotImplementedException();
	}

	private void RemoveInteractionIndicator(Node2D body)
	{
		if (IsPlayer((Node) body)) {
			interactionSprite.Visible = false;
			canInteractWith = false;
			EmitSignal(SignalName.PopupBox, true, "");
			popupManager.HidePopupPanel();
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
			dialogueTree.Evaluate();
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
