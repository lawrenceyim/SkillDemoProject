using Godot;
using System;

public partial class QuestionResponseButton : Button
{
	public event EventHandler<int> GetButtonIndex;
	private int index;

	public override void _Pressed() {
		GetButtonIndex?.Invoke(this, index);
	}

	public void SetButtonIndex(int index) {
		this.index = index;
	}
}
