using Fixed64;
using Massive.Common;
using Massive.Netcode;
using Massive.Physics.Components;

namespace Massive.Physics.Systems;

public class PhysicsBroadPhaseSystem : NetSystem, IUpdate {
	public void Update() {
		var entities = new List<int>();
		var query = World.Include<Transform, BoxCollider>();
		foreach (var entityId in query) {
			entities.Add(entityId);
		}

		for (var i = 0; i < entities.Count; i++) {
			for (var j = i + 1; j < entities.Count; j++) {
				var a = World.GetEntity(entities[i]);
				var b = World.GetEntity(entities[j]);

				ref var ta = ref a.Get<Transform>();
				ref var tb = ref b.Get<Transform>();

				ref var ca = ref a.Get<BoxCollider>();
				ref var cb = ref b.Get<BoxCollider>();

				var delta = tb.Position - ta.Position;
				var radius = ca.BoundingRadius + cb.BoundingRadius;

				if (FVector3.Dot(delta, delta) <= radius * radius) {
					World.Create(new BroadPhasePair { A = a, B = b });
				}
			}
		}
	}
}