using Fixed64;
using Massive.Netcode;

namespace HelloWorld.Core.Input;

public struct PlayerInput : IFadeOutInput<PlayerInput> {
	public FP MoveDirectionX;
	public FP MoveDirectionY;
	public FP AimDirectionX;
	public FP AimDirectionY;
	public bool Jump;
	public bool Attack;

	public PlayerInput FadeOut(int ticksPassed, in FadeOutConfig config) {
		var fadeOutPercent = FP.Clamp((ticksPassed - config.StartDecayTick).ToFP() / config.DecayDurationTicks.ToFP(),
			FP.Zero, FP.One);
		var directionModifier = FP.One - fadeOutPercent;
		var buttonsIsFresh = ticksPassed < 30;

		return new PlayerInput {
			MoveDirectionX = MoveDirectionX * directionModifier,
			MoveDirectionY = MoveDirectionY * directionModifier,
			AimDirectionX = buttonsIsFresh ? AimDirectionX : FP.Zero,
			AimDirectionY = buttonsIsFresh ? AimDirectionY : FP.Zero,
			Jump = buttonsIsFresh && Jump,
			Attack = buttonsIsFresh && Attack,
		};
	}
}