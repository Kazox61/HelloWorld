using Fixed64;
using Godot;
using HelloWorld.Client.core.ui;
using HelloWorld.Core.Input;

namespace HelloWorld.Client.core;

public partial class InputCollector : Node {
	[Export] private MobileUi _mobileUi;
	
	public Vector2 MoveDirection { get; private set; }
	public Vector2 AimDirection { get; private set; }
	public bool IsAttacking { get; private set; }
	public bool IsJumping { get; private set; }

	private float _accumulatedMouseX;
	private float _accumulatedMouseY;

	public override void _PhysicsProcess(double delta) {
		IsAttacking = false;
		
		MoveDirection = Input
			.GetVector("move_left", "move_right", "move_forward", "move_back")
			.Normalized();
		IsJumping = Input.IsActionJustPressed("jump");
	}

	public override void _Process(double delta) {
		if (Input.GetMouseMode() == Input.MouseModeEnum.Visible && Input.IsActionJustPressed("ui_accept")) {
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}
		
		if (Input.IsActionJustPressed("ui_cancel")) {
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
	}

	public override void _UnhandledInput(InputEvent @event) {
		if (@event is InputEventMouseMotion motion && Input.GetMouseMode() == Input.MouseModeEnum.Captured) {
			_accumulatedMouseX += -motion.Relative.X;
			_accumulatedMouseY += -motion.Relative.Y;
		}
	}
	
	public PlayerInput GatherInput() {
		var aimDirectionX = _accumulatedMouseX.ToFP();
		var aimDirectionY = _accumulatedMouseY.ToFP();

		if (_mobileUi.Visible) {
			var mobileAimInput = _mobileUi.GatherAimInput();
			aimDirectionX = mobileAimInput.X.ToFP();
			aimDirectionY = mobileAimInput.Y.ToFP();
		}
		
		var input = new PlayerInput {
			MoveDirectionX = MoveDirection.X.ToFP(),
			MoveDirectionY = MoveDirection.Y.ToFP(),
			AimDirectionX = aimDirectionX,
			AimDirectionY = aimDirectionY,
			Jump = IsJumping,
		};

		_accumulatedMouseX = 0;
		_accumulatedMouseY = 0;

		return input;
	}
}