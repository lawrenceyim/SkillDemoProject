using Godot;
using System;
using System.Collections.Generic;

public partial class MailBox : InteractableItem {
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		interactionArea.BodyEntered += AddInteractionIndicator;
		interactionArea.BodyExited += RemoveInteractionIndicator;
		playerInputHandler.Interaction += InteractWith;

		dialogueTree = new DialogueTree(new DialogueNode("Your mailbox is empty."));
		dialogueTree.OnQuestionSent += QuestionReceived;
		dialogueTree.OnDialogueSent += DialogueReceived;

		interactionSprite.Visible = false;
	}

	protected override void AddInteractionIndicator(Node2D body) {
		if (IsPlayer((Node)body)) {
			interactionSprite.Visible = true;
			canInteractWith = true;
		}
	}

	protected override void RemoveInteractionIndicator(Node2D body) {
		if (IsPlayer((Node)body)) {
			interactionSprite.Visible = false;
			canInteractWith = false;
		}
	}

	protected override void DialogueReceived(object sender, List<string> e) {
		popupManager.AddMessageToQueue(e);
	}

	protected override void QuestionReceived(object sender, (string, List<string>) e) {
		throw new NotImplementedException();
	}

	public override void InteractWith() {
		if (canInteractWith) {
			GD.Print("Interacted with mail box");
			if (!popupManager.IsDialogueOccurring()) {
				dialogueTree.Evaluate();
			}
			GD.Print(popupManager.HasMessages());
			popupManager.DisplayNextMessageOrClose();
			GD.Print(popupManager.HasMessages());
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