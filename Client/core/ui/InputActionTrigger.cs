using Godot;


namespace HelloWorld.Client.core.ui;

[GlobalClass]
public partial class InputActionTrigger : Node {
	[Export] public StringName ActionName;
	
	private Control _control;
	
	public override void _EnterTree() {
		_control = GetParent<Control>();
		_control.GuiInput += OnGuiInput;
	}

	public override void _ExitTree() {
		_control.GuiInput -= OnGuiInput;
		_control = null;
	}
	
	private void OnGuiInput(InputEvent inputEvent) {
		if (inputEvent is not InputEventScreenTouch touchEvent) {
			return;
		}
		
		if (touchEvent.Pressed) {
			Input.ActionPress(ActionName);
		} 
		else {
			Input.ActionRelease(ActionName);
		}
	}
}