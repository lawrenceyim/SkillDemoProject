using Godot;
using System;

public partial class SceneTransitionZone : Node2D
{
	[Export] private SceneManager.SceneName sceneName;
	private LoadingScreen loadingScreen;

	public override void _Ready() {
		loadingScreen = (LoadingScreen)GetTree().Root.GetNode("LoadingScreen");
	}

	private void _on_area_2d_body_entered(Node2D body) {
		if (body is Player) {
			loadingScreen.FadeIn();
			CallDeferred("SwitchSceneOnBodyEntered");
		}
	}

	private void SwitchSceneOnBodyEntered() {
		SceneManager.GetInstance().LoadSceneByEnum(sceneName);
	}
}


