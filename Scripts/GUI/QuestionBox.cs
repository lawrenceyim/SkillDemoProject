using Godot;
using System;
using System.Collections.Generic;

public partial class QuestionBox : CanvasLayer {
	public event EventHandler<int> ButtonSelectedEvent;
	public event EventHandler<bool> QuestionEventOccurring;

	[Export] private Panel backgroundPanel;
	[Export] private Label questionLabel;
	[Export] private PackedScene buttonPrefab;
	[Export] private VBoxContainer vBoxContainer;
	private List<QuestionResponseButton> buttons = new List<QuestionResponseButton>();

	public override void _Ready() {
		HideQuestionBox();
	}

	public void AddQuestionAndResponses(string message, List<string> responses) {
		QuestionEventOccurring?.Invoke(this, true);
		questionLabel.Text = message;
		foreach (string response in responses) {
			AddButton(response);
		}
	}

	public void AddButton(string message) {
		Button button = (Button)buttonPrefab.Instantiate();
		vBoxContainer.AddChild(button);
		button.Text = message;
		(button as QuestionResponseButton).GetButtonIndex += ButtonPressed;
		(button as QuestionResponseButton).SetButtonIndex(buttons.Count);
		buttons.Add(button as QuestionResponseButton);
	}

	public void ButtonPressed(object sender, int index) {
		QuestionEventOccurring?.Invoke(this, false);
		ButtonSelectedEvent?.Invoke(this, index);
		ClearButtons();HideQuestionBox();
	}

	public void DisplayQuestionBox() {
		Visible = true;
	}

	public void HideQuestionBox() {
		Visible = false;
	}

	public void ClearButtons() {
		foreach (QuestionResponseButton button in buttons) {
			button.QueueFree();
		}
		buttons.Clear();
	}
	
	public bool IsQuestionOccurring() {
		return buttons.Count > 0;
	}
}
