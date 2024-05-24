using Godot;

// Add this class as an Autoload or create a scene with a Node, add this script to the Node,
// and add the scene as an Autoload if you want to add [Export] variables to this class
public partial class SceneManager : Node {
	private static SceneManager instance;

	public static SceneManager GetInstance() {
		return instance;
	}

	public override void _Ready() {
		instance = this;
	}

	public void LoadSceneByName(string name) {
		GetTree().ChangeSceneToFile(name);
	}

	public void LoadSceneByPackedScene(PackedScene packedScene) {
		GetTree().ChangeSceneToPacked(packedScene);
	}
}
