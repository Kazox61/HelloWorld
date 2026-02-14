using Godot;

namespace HelloWorld.Client.core.ui;

public partial class MobileUi : CanvasLayer {
	public override void _EnterTree() {
		Visible = DisplayServer.IsTouchscreenAvailable();
	}
}