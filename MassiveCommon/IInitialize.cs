using Massive;

namespace Massive.Common;

public interface IInitialize : ISystemMethod<IInitialize> {
	void Initialize();

	void ISystemMethod<IInitialize>.Run() => Initialize();
}