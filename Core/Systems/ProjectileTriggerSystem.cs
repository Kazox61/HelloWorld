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
			
			if (entityA.Has<Projectile>() && entityB.Has<Player>()) {
				var projectile = entityA.Get<Projectile>();
				if (projectile.OwnerEntifier != triggerEvent.EntifierB) {
					entityB.Destroy();
					entityA.Destroy();
				}
			}
			else if (entityB.Has<Projectile>() && entityA.Has<Player>()) {
				var projectile = entityB.Get<Projectile>();
				if (projectile.OwnerEntifier != triggerEvent.EntifierA) {
					entityA.Destroy();
					entityB.Destroy();
				}
			}
		});
	}
}