using HelloWorld.Core.Systems;
using Massive;
using Massive.Common;
using Massive.Physics.Systems;

namespace HelloWorld.Core;

public class GameSetup : IGameSetup {
	public void SetupGame(MassiveSystems systems, MassiveWorld world, uint seed, int localInputChannel) {
		systems
			.New(() => new MassiveRandom(seed))
			// .New<PhysicsGravitySystem>()
			 .New<PhysicsIntegrationSystem>()
			// .New<PhysicsBroadPhaseSystem>()
			// .New<PhysicsNarrowPhaseSystem>()
			// .New<PhysicsSolveSystem>()
			.New<StartSystem>()
			.New<CharacterSpawnSystem>()
			.New<MovementSystem>()
			.New<CameraFollowSystem>()
			.New<PlayerAttackSystem>();
	}
}