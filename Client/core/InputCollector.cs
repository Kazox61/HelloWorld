using Godot;
using HelloWorld.Client.core.ui;

namespace HelloWorld.Client.core;

public partial class InputCollector : Node {
	[Export] private MobileUi _mobileUi;
	
	public Vector2 MoveDirection { get; private set; }
	public Vector2 AimDirection { get; private set; }
	public bool IsAttacking { get; private set; }
	public bool IsJumping { get; private set; }

	public override void _EnterTree() {
		_mobileUi.AimJoystick.Flicked += OnAimJoystickFlicked;
	}
	
	public override void _ExitTree() {
		_mobileUi.AimJoystick.Flicked -= OnAimJoystickFlicked;
	}

	public override void _PhysicsProcess(double delta) {
		IsAttacking = false;
		
		MoveDirection = Input
			.GetVector("move_left", "move_right", "move_forward", "move_back")
			.Normalized();
		IsJumping = Input.IsActionJustPressed("jump");
	}
	
	private void OnAimJoystickFlicked(Vector2 direction) {
		AimDirection = direction.Normalized();
		IsAttacking = true;
	}
}