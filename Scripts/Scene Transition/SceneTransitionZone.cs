using Godot;
using System;

public partial class SceneTransitionZone : Node2D
{
	[Export] private SceneManager.SceneName sceneName;

	private void _on_area_2d_body_entered(Node2D body) {
		if (body is Player) {
			CallDeferred("SwitchSceneOnBodyEntered");
		}
	}


	private void SwitchSceneOnBodyEntered() {
		SceneManager.GetInstance().LoadSceneByEnum(sceneName);
	}
}


