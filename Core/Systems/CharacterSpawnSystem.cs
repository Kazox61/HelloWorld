using Fixed64;
using HelloWorld.Core.Components;
using Massive;
using Massive.Common;
using Massive.Netcode;
using Massive.Physics.Components;
using Massive.QoL;

namespace HelloWorld.Core.Systems;

public class CharacterSpawnSystem : NetSystem, IUpdate, IInject<MassiveRandom> {
	private MassiveRandom _massiveRandom;
	
	public void Update() {
		foreach (var (channel, _) in Inputs.GetAllEvents<PlayerConnectedEvent>()) {
			CreatePlayer(channel);
		}
		
		World.ForEach((Entity entity, ref PlayerKill playerKill) => {
			playerKill.RespawnCooldown -= FP.One / Session.Config.TickRate;
			if (playerKill.RespawnCooldown > FP.Zero) {
				return;
			}
			
			CreatePlayer(playerKill.InputChannel);
			entity.Destroy();
		});
	}

	public void Inject(MassiveRandom massiveRandom) {
		_massiveRandom = massiveRandom;
	}

	private void CreatePlayer(int channel) {
		var player = World.CreateEntity(new Player { InputChannel = channel });
		player.Set(new Transform {
			Position = new FVector3(
				_massiveRandom.NextInt(-5, 5).ToFP(),
				2.ToFP(),
				_massiveRandom.NextInt(-5, 5).ToFP()
			)
		});
		player.Set(new ViewAsset(1));
		player.Set(new RigidBody {
			Velocity = FVector3.Zero,
			InverseMass = FP.One,
			Restitution = FP.Zero,
			Friction = FP.Half,
			UseGravity = true
		});
		player.Set(new BoxCollider {
			HalfExtents = new FVector3(FP.Half, 0.8f.ToFP(), FP.Half)
		});
		player.Set(new Health { Value = 10, MaxValue = 10 });
	}
}