using System;
using System.Collections.Generic;

public class DialogueTree
{
	public event EventHandler<(string, List<string>)> OnQuestionSent;
	public event EventHandler<List<string>> OnDialogueSent;

	private BaseDialogueNode startNode;

	public DialogueTree(BaseDialogueNode startNode) {
		this.startNode = startNode;
		SetEventHandlers(startNode);
	}

	public void Evaluate() {
		startNode.Evaluate();
	}

	public void SetEventHandlers(BaseDialogueNode node) {
		if (startNode == null) {
			return;
		}

		if (node is ConditionNode) {
			(BaseDialogueNode, BaseDialogueNode) children = (node as ConditionNode).GetChildren();
			SetEventHandlers(children.Item1);
			SetEventHandlers(children.Item2);
		} else if (node is QuestionNode) {
			(node as QuestionNode).OnQuestionSent += QuestionReceived;
		} else if (node is DialogueNode) {
			(node as DialogueNode).OnDialogueSent += DialogueReceived;
		}
	}

	private void DialogueReceived(object sender, List<string> e) {
		OnDialogueSent?.Invoke(sender, e);
	}

	private void QuestionReceived(object sender, (string, List<string>) e) {
		OnQuestionSent?.Invoke(sender, e);
	}
}
