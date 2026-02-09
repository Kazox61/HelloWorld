using Fixed64;
using HelloWorld.Core.Components;
using Massive;
using Massive.Common;
using Massive.Netcode;
using Massive.Physics.Components;

namespace HelloWorld.Core.Systems;

public class CharacterSpawnSystem : NetSystem, IUpdate, IInject<MassiveRandom> {
	private MassiveRandom _massiveRandom;
	
	public void Update() {
		foreach (var (channel, _) in Inputs.GetAllEvents<PlayerConnectedEvent>()) {
			var player1 = World.CreateEntity(new Player { InputChannel = channel });
			player1.Set(new Transform {
				Position = new FVector3(
					_massiveRandom.NextInt(-5, 5).ToFP(),
					2.ToFP(),
					_massiveRandom.NextInt(-5, 5).ToFP()
				)
			});
			player1.Set(new ViewAsset { PackedScenePath = "uid://c5t8fo6tmhs08" });
			player1.Set(new RigidBody {
				Velocity = FVector3.Zero,
				InverseMass = FP.One,
				Restitution = FP.Zero,
				Friction = 0.5f.ToFP(),
				UseGravity = true
			});
			player1.Set(new BoxCollider {
				HalfExtents = new FVector3(0.5f.ToFP(), 0.8f.ToFP(), 0.5f.ToFP())
			});
		}
	}

	public void Inject(MassiveRandom massiveRandom) {
		_massiveRandom = massiveRandom;
	}
}