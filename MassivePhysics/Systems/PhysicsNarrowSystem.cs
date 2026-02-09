using Fixed64;
using Massive.Common;
using Massive.Netcode;
using Massive.Physics.Components;

namespace Massive.Physics.Systems;

public class PhysicsNarrowPhaseSystem : NetSystem, IUpdate {
	public void Update() {
		World.Include<TriggerEvent>().ForEach(triggerEventEntity => {
			triggerEventEntity.Destroy();
		});
		
		World.ForEach((Entity pairEntity, ref BroadPhasePair pair) => {
			ref var colA = ref pair.A.Get<BoxCollider>();
			ref var colB = ref pair.B.Get<BoxCollider>();

			// update collider centres
			colA.Center = pair.A.Get<Transform>().Position;
			colB.Center = pair.B.Get<Transform>().Position;

			var gjk = GJK.Calculate(colA, colB);

			if (!gjk.CollisionHappened) {
				pairEntity.Destroy();
				return;
			}

			var collision = EPA.Calculate(gjk.Simplex, colA, colB);

			var isTrigger = colA.IsTrigger || colB.IsTrigger;

			if (isTrigger) {
				World.Create(new TriggerEvent {
					EntifierA = pair.A.Entifier,
					EntifierB = pair.B.Entifier
				});
			}

			World.Create(new Contact {
				EntifierA = pair.A.Entifier,
				EntifierB = pair.B.Entifier,
				Collision = collision,
				IsTrigger = isTrigger
			});

			pairEntity.Destroy();
		});
	}
}