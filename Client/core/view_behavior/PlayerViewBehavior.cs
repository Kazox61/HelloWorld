using Fixed64;
using Godot;
using HelloWorld.addons.massive_godot_integration.view_synchronizer;
using HelloWorld.Client.core;
using HelloWorld.Core.Components;
using Massive;
using Massive.Common;
using Massive.Physics.Components;

namespace HelloWorld.Client.Core;

public partial class PlayerViewBehavior : EntityBehaviour {
	[Export] private GodotPlushSkin _plushSkin;
	[Export] private Camera3D _camera;
	[Export] private Label _debugLabel;
	
	private Entity _entity;
	private DataSet<RigidBody> _rigidBodies;
	private DataSet<Player> _players;
	private DataSet<Health> _healths;
	private DataSet<Transform> _transforms;
	
	public override void OnEntityAssigned(Entity entity) {
		_entity = entity;
		_rigidBodies = _entity.World.DataSet<RigidBody>();
		_players = _entity.World.DataSet<Player>();
		_healths = _entity.World.DataSet<Health>();
		_transforms = _entity.World.DataSet<Transform>();
	}
	public override void OnEntityRemoved() {
		_rigidBodies = null;
		_players = null;
		_entity = Entity.Dead;
	}

	public override void _Process(double delta) {
		if (_rigidBodies.Has(_entity.Id)) {
			var rigidBody = _rigidBodies.Get(_entity.Id);

			if (!rigidBody.IsGrounded) {
				_plushSkin.SetState(rigidBody.Velocity.Y > FP.Zero ? "jump" : "fall");
			}
			else {
				var isRunning = !FP.ApproximatelyEqual(rigidBody.Velocity.X, FP.Zero) ||
				                !FP.ApproximatelyEqual(rigidBody.Velocity.Z, FP.Zero);
				_plushSkin.SetState(isRunning ? "run" : "idle");
			}
		}

		if (_players.Has(_entity.Id)) {
			var player = _players.Get(_entity.Id);
			_camera.Current = player.InputChannel == ClientGameRunner.LocalPlayerChannel;
		}
		
		_debugLabel.Text = "";

		if (_transforms.Has(_entity.Id)) {
			var transform = _transforms.Get(_entity.Id);
			_debugLabel.Text += $"({transform.Position.X.ToFloat():0.00}, {transform.Position.Y.ToFloat():0.00}, {transform.Position.Z.ToFloat():0.00})";
		}
		
		if (_healths.Has(_entity.Id)) {
			var health = _healths.Get(_entity.Id);
			_debugLabel.Text += $"\n{health.Value}:{health.MaxValue}";
		}
	}
}