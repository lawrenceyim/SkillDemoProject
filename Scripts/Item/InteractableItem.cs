using System;
using System.Collections.Generic;
using Godot;

public abstract partial class InteractableItem : BaseItem, IInteractable
{
	[Export] protected PopupManager popupManager;
	[Export] protected Area2D interactionArea;
	[Export] protected Sprite2D interactionSprite;
	[Export] protected PlayerInputHandler playerInputHandler;
	protected bool canInteractWith = false;
	protected DialogueTree dialogueTree;

	public abstract void InteractWith();
	protected abstract void AddInteractionIndicator(Node2D body);
	protected abstract void RemoveInteractionIndicator(Node2D body);
	protected abstract void DialogueReceived(object sender, List<string> e);
	protected abstract void QuestionReceived(object sender, (string, List<string>) e);
}
