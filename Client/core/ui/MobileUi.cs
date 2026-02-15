using Godot;

namespace HelloWorld.Client.core.ui;

public partial class MobileUi : CanvasLayer {
	[Export] public VirtualJoystick MoveJoystick;
	[Export] public VirtualJoystick AimJoystick;
	
	public override void _EnterTree() {
		Visible = DisplayServer.IsTouchscreenAvailable();
	}
}