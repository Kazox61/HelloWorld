using Massive;

namespace Massive.Common;

public interface IUpdate : ISystemMethod<IUpdate> {
	void Update();

	void ISystemMethod<IUpdate>.Run() => Update();
}