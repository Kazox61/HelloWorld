using Massive.Netcode;

namespace HelloWorld.Core.Input;

public struct PlayerInput : IInput {
	public float DirectionX;
	public float DirectionY;
	public float AimX;
	public float AimY;
	public bool Jump;
	public bool Attack;
}