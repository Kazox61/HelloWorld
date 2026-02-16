using Godot;

namespace HelloWorld.Client.core.ui;

public partial class MobileUi : CanvasLayer {
	[Export] public VirtualJoystick MoveJoystick;
	[Export] public Control AimInputCollector;
	
	private Vector2 _accumulatedDrag;
	
	public override void _EnterTree() {
		Visible = DisplayServer.IsTouchscreenAvailable();
		AimInputCollector.GuiInput += OnAimInput;
	}
	
	public override void _ExitTree() {
		AimInputCollector.GuiInput -= OnAimInput;
	}
	
	private void OnAimInput(InputEvent inputEvent) {
		if (inputEvent is not InputEventScreenDrag dragEvent) {
			return;
		}
		
		_accumulatedDrag -= dragEvent.Relative;
	}
	
	public Vector2 GatherAimInput() {
		var result = _accumulatedDrag;
		_accumulatedDrag = Vector2.Zero;
		return result;
	}
}