using Fixed64;
using Godot;
using HelloWorld.Client.core;
using Massive;
using Massive.Physics.Components;
using massivegodotintegration.addons.massive_godot_integration.synchronizer;

namespace HelloWorld.Client.Core;

public partial class PlayerViewBehavior : ViewBehavior {
	[Export] private GodotPlushSkin _plushSkin;
	
	private DataSet<RigidBody> _rigidBodies;
	private Entity _entity;
	
	public override void OnEntityAssigned(World world, Entity entity) {
		_entity = entity;
		_rigidBodies = world.DataSet<RigidBody>();
	}
	public override void OnEntityRemoved() {
		_rigidBodies = null;
		_entity = Entity.Dead;
	}

	public override void _Process(double delta) {
		if (!_rigidBodies.Has(_entity.Id)) {
			return;
		}
		
		var rigidBody = _rigidBodies.Get(_entity.Id);
		
		if (rigidBody.Velocity.Y > FP.Half) {
			_plushSkin.SetState("jump");
		}
		else if (rigidBody.Velocity.Y < -FP.Half) {
			_plushSkin.SetState("fall");
		}
		else if (!FP.ApproximatelyEqual(rigidBody.Velocity.X, FP.Zero) || !FP.ApproximatelyEqual(rigidBody.Velocity.Z, FP.Zero)) {
			_plushSkin.SetState("run");
		}
		else {
			_plushSkin.SetState("idle");
		}
	}
}