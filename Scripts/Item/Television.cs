using Godot;
using System;
using System.Collections.Generic;

public partial class Television : InteractableItem {
	private bool isPlayerOld = false;
	private bool interactedWithAlready = false;

	public override void _Ready() {
		base._Ready();
		BaseDialogueNode startNode = new ConditionNode.Builder()
			.SetCondition(() => interactedWithAlready)
			.SetTrueNode(new ConditionNode.Builder()
				.SetCondition(() => isPlayerOld)
				.SetTrueNode(new DialogueNode("You're old enough to use the PC"))
				.SetFalseNode(new DialogueNode("You're too young to use the PC"))
				.Build())
			.SetFalseNode(new QuestionNode.Builder()
				.SetQuestion("How old are you?")
				.AddResponse("< 18", () => isPlayerOld = false)
				.AddResponse(">= 18", () => isPlayerOld = true)
				.Build())
			.Build();
		SetDialogueTree(new DialogueTree(startNode));
	}

	public override void InteractWith() {
		if (popupManager.IsDialogueOccurring()) {
			popupManager.DisplayNextMessageOrClose();
			return;
		}
		dialogueTree.Evaluate();
		interactedWithAlready = true;
	}

	protected override void DialogueReceived(object sender, List<string> e) {
		popupManager.AddMessageToQueue(e);
		popupManager.DisplayNextMessageOrClose();
	}

	protected override void QuestionReceived(object sender, (string, List<string>) e) {
		questionBox.DisplayQuestionBox();
		questionBox.AddQuestionAndResponses(sender, e.Item1, e.Item2);
	}
}
