using System.Collections.Generic;
using Godot;

public abstract partial class InteractableItem : BaseItem, IInteractable
{
	[Export] protected Player player;
	protected Area2D interactionArea;
	protected PopupManager popupManager;
	protected bool canInteractWith = false;
	protected DialogueTree dialogueTree;

	public override void _Ready() {
		popupManager = (PopupManager) GetTree().Root.GetNode("PopupManager");
		interactionArea = (Area2D) GetNode("Interaction Area");
	}

	public void SetDialogueTree(DialogueTree dialogueTree) {
		this.dialogueTree = dialogueTree;
		dialogueTree.OnQuestionSent += QuestionReceived;
		dialogueTree.OnDialogueSent += DialogueReceived;
	}

	public abstract void InteractWith();
	protected abstract void DialogueReceived(object sender, List<string> e);
	protected abstract void QuestionReceived(object sender, (string, List<string>) e);
}
