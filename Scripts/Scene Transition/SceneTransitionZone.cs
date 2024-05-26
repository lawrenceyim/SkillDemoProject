using Godot;
using System;

public partial class SceneTransitionZone : Node2D
{
	[Export] private SceneManager.SceneName sceneName;

	private void _on_area_2d_body_entered(Node2D body) {
		if (IsPlayer((Node) body)) {
			CallDeferred("SwitchSceneOnBodyEntered");
		}
	}


	private void SwitchSceneOnBodyEntered() {
		SceneManager.GetInstance().LoadSceneByEnum(sceneName);
	}

	private bool IsPlayer(Node node) {
		foreach (Node child in node.GetChildren()) {
			if (child is Player) {
				return true;
			}
		}
		return false;
	}
}


