using Fixed64;
using HelloWorld.Core.Components;
using Massive;
using Massive.Netcode;

namespace HelloWorld.Core.Systems;

public class ProjectileLifetimeSystem : NetSystem, IUpdate {
	public void Update() {
		World.ForEach((Entity entity, ref Projectile projectile) => {
			projectile.Lifetime += FP.One / Session.Config.TickRate;

			if (projectile.Lifetime > FP.One) {
				entity.Destroy();
			}
		});
	}
}