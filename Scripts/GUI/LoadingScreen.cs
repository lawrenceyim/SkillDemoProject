using Godot;
using System;

public partial class LoadingScreen : Sprite2D
{
	public event EventHandler LoadedBlackScreen;

	public enum LoadingMode {
		FadingIn = 0,
		FadingOut = 1,
		None = 2,
	}

	[Export] private float fadeOutTime = 1f;
	private Timer timer;
	private LoadingMode loadingMode = LoadingMode.None;
	Color color = new Color(0, 0, 0, 0);

	public override void _Ready() {
		SelfModulate = color;
		timer = new Timer();
		AddChild(timer);
		timer.Timeout += OnTimeOut;
	}

	private void OnTimeOut() {
		if (loadingMode == LoadingMode.FadingOut) {
			color.A = 0f;
			SelfModulate = color;
			loadingMode = LoadingMode.None;
		}
	}

	public override void _Process(double delta) {
		if (loadingMode == LoadingMode.None) {
			return;
		}
		if (loadingMode == LoadingMode.FadingOut) {
			FadeOut();
		}
	}

	public void FadeIn() {
		color.A = 1f;
		SelfModulate = color;
		loadingMode = LoadingMode.FadingOut;
		timer.Start(fadeOutTime);
		LoadedBlackScreen?.Invoke(this, EventArgs.Empty);
	}

	// Remove black loading screen
	public void FadeOut() {
		color.A = (float)(timer.TimeLeft / fadeOutTime);
		SelfModulate = color;
	}
}
