using Fixed64;
using Godot;
using HelloWorld.addons.massive_godot_integration.view_synchronizer;
using Massive;
using Massive.Common;

namespace HelloWorld.Client.Core;

[GlobalClass]
public partial class TransformViewBehavior : EntityBehaviour {
	[Export] private Node3D _targetNodePosition;
	[Export] private Node3D _targetNodeRotation;

	private DataSet<Transform> _transforms;
	private Entity _entity;
	
	public override void OnEntityAssigned(Entity entity) {
		_entity = entity;
		_transforms = _entity.World.DataSet<Transform>();
		Update();
	}

	public override void OnEntityRemoved() {
		_transforms = null;
		_entity = Entity.Dead;
	}

	public override void _Process(double delta) {
		Update();
	}

	private void Update() {
		if (!_transforms.Has(_entity.Id)) {
			return;
		}
		
		var transform = _transforms.Get(_entity.Id);
		_targetNodePosition.Position = new Vector3(
			transform.Position.X.ToFloat(),
			transform.Position.Y.ToFloat(),
			transform.Position.Z.ToFloat()
		);
		_targetNodeRotation.Rotation = new Vector3(
			transform.Rotation.X.ToFloat(),
			transform.Rotation.Y.ToFloat(),
			transform.Rotation.Z.ToFloat()
		);
	}
}