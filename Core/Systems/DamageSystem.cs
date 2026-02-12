using HelloWorld.Core.Components;
using Massive;
using Massive.Netcode;

namespace HelloWorld.Core.Systems;

public class DamageSystem : NetSystem, IUpdate {
	public void Update() {
		var healths = World.DataSet<Health>();
		var damages = World.DataSet<Damage>();

		foreach (var entityId in damages) {
			var damageEntity = damages.Get(entityId);
			if (!healths.Has(damageEntity.TargetEntifier.Id)) {
				continue;
			}
			
			var health = healths.Get(damageEntity.TargetEntifier.Id);
			health.Value -= damageEntity.Value;
			
			health.Value = Math.Max(health.Value, 0);

			if (health.Value == 0) {
				World.Destroy(damageEntity.TargetEntifier);
			}
		}
		
		damages.Clear();
	}
}