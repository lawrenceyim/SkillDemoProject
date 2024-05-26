using System.Collections.Generic;
using Godot;

public abstract partial class InteractableItem : BaseItem, IInteractable
{
	[Export] protected Player player;
	protected PopupManager popupManager;
	protected QuestionBox questionBox;
	protected bool canInteractWith = false;
	protected DialogueTree dialogueTree;

	public override void _Ready() {
		questionBox = (QuestionBox) GetTree().Root.GetNode("QuestionBox");
		popupManager = (PopupManager) GetTree().Root.GetNode("PopupManager");
		questionBox.ButtonSelectedEvent += QuestionResponseReceived;
	}

	public void SetDialogueTree(DialogueTree dialogueTree) {
		this.dialogueTree = dialogueTree;
		dialogueTree.OnQuestionSent += QuestionReceived;
		dialogueTree.OnDialogueSent += DialogueReceived;
	}

	public abstract void InteractWith();
	protected abstract void QuestionResponseReceived(object sender, int index);
	protected abstract void DialogueReceived(object sender, List<string> e);
	protected abstract void QuestionReceived(object sender, (string, List<string>) e);
}
