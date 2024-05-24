using Godot;
using System;

public partial class PopupManager : Node
{
	[Export] Panel popupPanel;
	//[Export] Label nameLabel;
	[Export] Label messageLabel;
	
	public override void _Ready()
	{
		popupPanel.Visible = false;
	}

	public void ShowPopupPanel() {
		popupPanel.Visible = true;
	}

	public void ChangePopupLabelText(string message) {
		messageLabel.Text = message;
	}

	public void HidePopupPanel() {
		popupPanel.Visible = false;
	}
}
