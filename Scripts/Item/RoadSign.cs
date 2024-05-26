using Godot;
using System;
using System.Collections.Generic;

public partial class RoadSign : InteractableItem {
	[Export(PropertyHint.MultilineText)] private string signText;

	public override void _Ready() {
		base._Ready();

		BaseDialogueNode startNode = new DialogueNode(signText);
		SetDialogueTree(new DialogueTree(startNode));
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
			if (!popupManager.IsDialogueOccurring()) {
				dialogueTree.Evaluate();
			}
			popupManager.DisplayNextMessageOrClose();
			Logger.Print(this, Logger.DebugLevel.DEBUG, "Interacted with road sign.");
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
