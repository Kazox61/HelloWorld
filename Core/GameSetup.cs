using HelloWorld.Core.Systems;
using Massive;
using Massive.Common;
using Massive.Physics.Systems;

namespace HelloWorld.Core;

public class GameSetup : IGameSetup {
	public void SetupGame(MassiveSystems systems, MassiveWorld world, uint seed) {
		systems
			.New<PhysicsSystem>()
			.New(() => new MassiveRandom(seed))
			.New<StartSystem>()
			.New<CharacterSpawnSystem>()
			.New<CharacterDespawnSystem>()
			.New<MovementSystem>()
			.New<PlayerAttackSystem>()
			.New<ProjectileTriggerSystem>()
			.New<DamageSystem>();
	}
}