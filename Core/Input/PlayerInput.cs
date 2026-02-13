using Fixed64;
using Massive.Netcode;

namespace HelloWorld.Core.Input;

public struct PlayerInput : IInput {
	public FP DirectionX;
	public FP DirectionY;
	public FP AimX;
	public FP AimY;
	public bool Jump;
	public bool Attack;
}