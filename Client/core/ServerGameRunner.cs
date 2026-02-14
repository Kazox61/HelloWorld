using Godot;
using HelloWorld.Core;
using Massive.Common;

namespace HelloWorld.Client.core;

public partial class ServerGameRunner : Node {
	public ServerGame ServerGame;

	public override void _Ready() {
		ServerGame = new ServerGame();
		ServerGame.Start();
	}

	public override void _PhysicsProcess(double delta) {
		ServerGame.Update(delta);
	}
}