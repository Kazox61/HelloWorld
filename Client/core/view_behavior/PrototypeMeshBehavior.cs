using Fixed64;
using Godot;
using HelloWorld.addons.massive_godot_integration.view_synchronizer;
using HelloWorld.Core.Components;
using Massive;

namespace HelloWorld.Client.Core;

[GlobalClass]
public partial class PrototypeMeshBehavior : EntityBehaviour {
	[Export] private MeshInstance3D _targetMeshInstance;

	private DataSet<PrototypeMesh> _prototypeMeshes;
	private Entity _entity;
	
	public override void OnEntityAssigned(Entity entity) {
		_entity = entity;
		_prototypeMeshes = _entity.World.DataSet<PrototypeMesh>();
		Update();
	}
	public override void OnEntityRemoved() {
		_prototypeMeshes = null;
		_entity = Entity.Dead;
	}

	private void Update() {
		if (!_prototypeMeshes.Has(_entity.Id)) {
			return;
		}

		var prototypeMesh = _prototypeMeshes.Get(_entity.Id);
		
		if (_targetMeshInstance.Mesh is not BoxMesh mesh) {
			return;
		}
		
		mesh.Size = new Vector3(
			prototypeMesh.Size.X.ToFloat(),
			prototypeMesh.Size.Y.ToFloat(),
			prototypeMesh.Size.Z.ToFloat()
		);
	}
}