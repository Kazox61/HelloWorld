using System;
using System.Threading.Tasks;
using Godot;
using Massive;

namespace HelloWorld.Client.core;

public partial class StartScene : Node {
	[Export] public PackedScene ServerScene;
	[Export] public PackedScene ClientScene;

	public override async void _Ready() {
		if (OS.GetCmdlineArgs().Contains("--server")) {
			var server = ServerScene.Instantiate();
			AddChild(server);
			GetWindow().Title = "Massive Server";
			return;
		}

		await Task.Delay(TimeSpan.FromSeconds(2));
		var client = ClientScene.Instantiate();
		AddChild(client);
		GetWindow().Title = "Massive Client";
	}
}