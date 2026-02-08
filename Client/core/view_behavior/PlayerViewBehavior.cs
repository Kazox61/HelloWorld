using Fixed64;
using Godot;
using HelloWorld.Client.core;
using HelloWorld.Core.Components;
using Massive;
using Massive.Physics.Components;
using massivegodotintegration.addons.massive_godot_integration.synchronizer;

namespace HelloWorld.Client.Core;

public partial class PlayerViewBehavior : ViewBehavior {
	[Export] private GodotPlushSkin _plushSkin;
	[Export] private Camera3D _camera;
	
	private DataSet<RigidBody> _rigidBodies;
	private DataSet<Player> _players;
	private Entity _entity;
	
	public override void OnEntityAssigned(World world, Entity entity) {
		_entity = entity;
		_rigidBodies = world.DataSet<RigidBody>();
		_players = world.DataSet<Player>();
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
	}
}