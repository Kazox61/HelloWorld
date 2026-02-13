using Godot;
using Massive;

namespace HelloWorld.Client.core;

public partial class StartScene : Node {
	[Export] public PackedScene ServerScene;
	[Export] public PackedScene ClientScene;

	public override void _Ready() {
		if (OS.GetCmdlineArgs().Contains("--server")) {
			var server = ServerScene.Instantiate();
			AddChild(server);
			GetWindow().Title = "Massive Server";
			return;
		}

		var client = ClientScene.Instantiate();
		AddChild(client);
		GetWindow().Title = "Massive Client";
	}
}