using Fixed64;
using HelloWorld.Core.Components;
using HelloWorld.Core.Input;
using Massive;
using Massive.Common;
using Massive.Netcode;
using Massive.Physics.Components;
using Massive.QoL;

namespace HelloWorld.Core.Systems;

public class PlayerAttackSystem : NetSystem, IUpdate {
	public void Update() {
		World.ForEach((Entity entity, ref Player player, ref Transform transform) => {
			var playerInput = Inputs.GetInput<PlayerInput>(player.InputChannel).LastFresh();
			
			if (playerInput.Attack) {
				var projectile = World.CreateEntity(new Projectile { OwnerEntifier = entity.Entifier });
				projectile.Set(new Transform {
					Position = transform.Position + new FVector3(0.ToFP(), 0.5f.ToFP(), 0.ToFP()),
				});
				
				var yaw = transform.Rotation.Y;
				var pitch = transform.Rotation.X;
				var cosPitch = FP.Cos(pitch);
				var direction = FVector3.Normalize(new FVector3(
					FP.Sin(yaw) * cosPitch,
					-FP.Sin(pitch),
					FP.Cos(yaw) * cosPitch
				));
				
				projectile.Set(new RigidBody {
					Velocity = direction * 20.ToFP(),
					InverseMass = 1.ToFP()
				});
				
				projectile.Set(new BoxCollider {
					HalfExtents = new FVector3(0.25f.ToFP(), 0.25f.ToFP(), 0.25f.ToFP()),
					IsTrigger = true
				});
				
				projectile.Set(new ViewAsset(3));
			}
		});
	}
}