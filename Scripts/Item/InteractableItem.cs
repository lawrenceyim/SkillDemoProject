using System.Collections.Generic;
using Godot;

public abstract partial class InteractableItem : BaseItem, IInteractable
{
	[Export] protected Area2D interactionArea;
	[Export] protected Sprite2D interactionSprite;
	[Export] protected Player player;
	protected PopupManager popupManager;
	protected PlayerInputHandler playerInputHandler;
	protected bool canInteractWith = false;
	protected DialogueTree dialogueTree;

	public override void _Ready() {
		popupManager = (PopupManager) GetTree().Root.GetNode("PopupManager");
		playerInputHandler = player.GetPlayerInputHandler();
		// playerInputHandler.Interaction += InteractWith;
		interactionSprite.Visible = false;
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
