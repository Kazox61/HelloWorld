using HelloWorld.Core.Components;
using Massive;
using Massive.Netcode;
using Massive.Physics.Components;

namespace HelloWorld.Core.Systems;

public class ProjectileTriggerSystem : NetSystem, IUpdate {
	public void Update() {
		World.ForEach((ref TriggerEvent triggerEvent) => {
			var entityA = triggerEvent.EntifierA.In(World);
			var entityB = triggerEvent.EntifierB.In(World);
			
			if (entityA.Has<Projectile>() && entityB.Has<Health>()) {
				var projectile = entityA.Get<Projectile>();
				if (projectile.OwnerEntifier != triggerEvent.EntifierB) {
					World.Create(new Damage {
						Value = 1,
						TargetEntifier = entityB.Entifier,
						SourceEntifier = projectile.OwnerEntifier
					});
					entityA.Destroy();
				}
			}
			else if (entityB.Has<Projectile>() && entityA.Has<Health>()) {
				var projectile = entityB.Get<Projectile>();
				if (projectile.OwnerEntifier != triggerEvent.EntifierA) {
					World.Create(new Damage {
						Value = 1,
						TargetEntifier = entityA.Entifier,
						SourceEntifier = projectile.OwnerEntifier
					});
					entityB.Destroy();
				}
			}
		});
	}
}