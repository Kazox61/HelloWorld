using Fixed64;
using HelloWorld.Core.Components;
using Massive;
using Massive.Netcode;

namespace HelloWorld.Core.Systems;

public class DamageSystem : NetSystem, IUpdate {
	public void Update() {
		var healths = World.DataSet<Health>();
		var damages = World.DataSet<Damage>();

		foreach (var entityId in damages) {
			ref var damageEntity = ref damages.Get(entityId);
			if (!healths.Has(damageEntity.TargetEntifier.Id)) {
				continue;
			}
			
			ref var health = ref healths.Get(damageEntity.TargetEntifier.Id);
			health.Value -= damageEntity.Value;
			
			health.Value = Math.Max(health.Value, 0);

			if (health.Value == 0) {
				var targetEntity = damageEntity.TargetEntifier.In(World);
				if (targetEntity.Has<Player>()) {
					var player = targetEntity.Get<Player>();
					World.CreateEntity(new PlayerKill { InputChannel = player.InputChannel, RespawnCooldown = FP.One });
				}
				World.Destroy(damageEntity.TargetEntifier);
			}
		}
		
		damages.Clear();
	}
}