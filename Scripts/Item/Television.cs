using Godot;
using System;
using System.Collections.Generic;

public partial class Television : InteractableItem {
	public override void _Ready() {
		base._Ready();
		BaseDialogueNode startNode = new QuestionNode.Builder()
			.SetQuestion("How old are you?")
			.AddResponse("< 18", () => GD.Print("You're underaged"))
			.AddResponse(">= 18", () => GD.Print("You're old enough to vote to watch TV"))
			.Build();
		SetDialogueTree(new DialogueTree(startNode));
	}

	public override void InteractWith() {
		if (questionBox == null) {
			GD.Print("questionbox is null");
		}
		if (questionBox.IsQuestionOccurring() || popupManager.IsDialogueOccurring()) {
			return;
		}
		dialogueTree.Evaluate();
	}

	protected override void DialogueReceived(object sender, List<string> e) {
		throw new NotImplementedException();
	}

	protected override void QuestionReceived(object sender, (string, List<string>) e) {
		questionBox.DisplayQuestionBox();
		questionBox.AddQuestionAndResponses(this, e.Item1, e.Item2);
	}

	protected override void QuestionResponseReceived(object sender, int index) {
		if (sender != this) {
			GD.Print("Sender does not match receiver");
			return;
		}
		GD.Print(index);
	}
}
