using Godot;
using System;
using System.Collections.Generic;

public partial class MailBox : InteractableItem {
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		base._Ready();

		BaseDialogueNode startNode = new DialogueNode("Your mailbox is empty.");
		(startNode as DialogueNode).AddMessage("Check again tomorrow");
		SetDialogueTree(new DialogueTree(startNode));
	}

	protected override void DialogueReceived(object sender, List<string> e) {
		popupManager.AddMessageToQueue(e);
	}

	protected override void QuestionReceived(object sender, (string, List<string>) e) {
	}

	public override void InteractWith() {
		if (!popupManager.IsDialogueOccurring()) {
			dialogueTree.Evaluate();
		}
		popupManager.DisplayNextMessageOrClose();
		Logger.Print(this, Logger.DebugLevel.DEBUG, "Interacted with mailbox.");
	}
}
