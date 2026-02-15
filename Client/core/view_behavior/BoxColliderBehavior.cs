using Fixed64;
using Godot;
using HelloWorld.addons.massive_godot_integration.view_synchronizer;
using Massive;
using Massive.Common;
using Massive.Physics.Components;

namespace HelloWorld.Client.Core;

[GlobalClass]
public partial class BoxColliderBehavior : EntityBehaviour {
	private DataSet<BoxCollider> _boxColliders;
	private DataSet<Transform> _transforms;
	private Entity _entity;
	
	public override void OnEntityAssigned(Entity entity) {
		_entity = entity;
		_boxColliders = _entity.World.DataSet<BoxCollider>();
		_transforms = _entity.World.DataSet<Transform>();
		Update();
	}
	public override void OnEntityRemoved() {
		_boxColliders = null;
		_transforms = null;
		_entity = Entity.Dead;
	}

	public override void _Process(double delta) {
		Update();
	}

	private void Update() {
		return;
		if (!_boxColliders.Has(_entity.Id)) {
			return;
		}
		
		if (!_transforms.Has(_entity.Id)) {
			return;
		}
		
		var boxCollider = _boxColliders.Get(_entity.Id);
		var transform = _transforms.Get(_entity.Id);
		var aabb = new Aabb(
			new Vector3(
				transform.Position.X.ToFloat() - boxCollider.HalfExtents.X.ToFloat(), 
				transform.Position.Y.ToFloat() - boxCollider.HalfExtents.Y.ToFloat(), 
				transform.Position.Z.ToFloat() - boxCollider.HalfExtents.Z.ToFloat()
			),
			new Vector3(boxCollider.HalfExtents.X.ToFloat() * 2, boxCollider.HalfExtents.Y.ToFloat() * 2, boxCollider.HalfExtents.Z.ToFloat() * 2)
		);
		DebugDraw3D.DrawAabb(aabb, Colors.Green);
	}
}