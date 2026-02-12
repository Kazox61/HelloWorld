using Godot;

namespace HelloWorld.Client.core;

[GlobalClass]
public partial class WorldPositionUpdater : Node {
	[Export] private Node3D _anchor;
	
	public override void _Process(double delta) {
		GetParent<Control>().GlobalPosition = GetViewport().GetCamera3D().UnprojectPosition(_anchor.GlobalPosition);
	}
}