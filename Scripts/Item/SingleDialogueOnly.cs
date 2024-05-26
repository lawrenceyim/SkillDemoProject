using Godot;
using System;
using System.Collections.Generic;

public partial class SingleDialogueOnly : InteractableItem {
	[Export(PropertyHint.MultilineText)] private string signText;
	
	public override void _Ready() {
		base._Ready();
		BaseDialogueNode startNode = new DialogueNode(signText);
		SetDialogueTree(new DialogueTree(startNode));
	}

	protected override void DialogueReceived(object sender, List<string> e) {
		popupManager.AddMessageToQueue(e);
	}

	public override void InteractWith() {
		if (!popupManager.IsDialogueOccurring() && !questionBox.IsQuestionOccurring()) {
			dialogueTree.Evaluate();
		}
		popupManager.DisplayNextMessageOrClose();
		Logger.Print(this, Logger.DebugLevel.DEBUG, "Interacted with SingleDialogueOnly.");
	}

	protected override void QuestionReceived(object sender, (string, List<string>) e) {
	}
}
