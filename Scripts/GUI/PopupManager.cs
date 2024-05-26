using Godot;
using System;
using System.Collections.Generic;

public partial class PopupManager : Node
{
	[Signal] public delegate void DialogueEventHandler(bool occurring);
	private Panel popupPanel;
	//[Export] Label nameLabel;
	private Label messageLabel;
	private Queue<string> messageQueue = new Queue<string>();
	private bool isDialogueOccurring = false;
	private bool hasMessages => messageQueue.Count > 0; 

	public override void _Ready()
	{
		popupPanel = (Panel) GetNode("Panel");
		messageLabel = (Label) GetNode("Panel/Label");
		popupPanel.Visible = false;
	}

	public void ShowPopupPanel() {
		popupPanel.Visible = true;
	}

	public void AddMessageToQueue(List<string> messageList) {
		isDialogueOccurring = true;
		EmitSignal(SignalName.Dialogue, true);
		foreach (string message in messageList) {
			messageQueue.Enqueue(message);
		}
	}

	public void DisplayNextMessageOrClose() {
		if (messageQueue.Count > 0) {
			messageLabel.Text = messageQueue.Dequeue();
			ShowPopupPanel();
		} else {
			isDialogueOccurring = false;
			HidePopupPanel();
			EmitSignal(SignalName.Dialogue, false);
		}
	}

	public void ChangePopupLabelText(string message) {
		messageLabel.Text = message;
	}

	public void HidePopupPanel() {
		popupPanel.Visible = false;
	}

	public bool HasMessages() {
		return hasMessages;
	}

	public bool IsDialogueOccurring() {
		return isDialogueOccurring;
	}
}
