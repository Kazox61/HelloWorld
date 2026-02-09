namespace Massive.Common;

public interface IGameSetup {
	void SetupGame(MassiveSystems systems, MassiveWorld world, uint seed);
}