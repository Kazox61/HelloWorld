using Fixed64;
using HelloWorld.Core.Components;
using HelloWorld.Core.Input;
using Massive;
using Massive.Common;
using Massive.Netcode;
using Massive.Physics.Components;

namespace HelloWorld.Core.Systems;

public class MovementSystem : NetSystem, IUpdate {
	private static FP _sensitivity = 0.1f.ToFP();
	
	public void Update() {
		World.ForEach((ref Player player, ref RigidBody rigidBody, ref Transform transform) => {
			var playerInput = Inputs.GetInput<PlayerInput>(player.InputChannel).FadeOut(new FadeOutConfig(30, 60));
			
			player.Yaw += playerInput.AimDirectionX * (FP.One / Session.Config.TickRate) * _sensitivity;
			player.Pitch += playerInput.AimDirectionY * (FP.One / Session.Config.TickRate) * _sensitivity;
			
			player.Pitch = FP.Clamp(player.Pitch, -FP.HalfPi, FP.HalfPi);
			
			var forward = new FVector3(
				FP.Sin(player.Yaw),
				FP.Zero,
				FP.Cos(player.Yaw)
			);

			var right = new FVector3(
				forward.Z,
				FP.Zero,
				-forward.X
			);
			
			var moveDirection = forward * playerInput.MoveDirectionY + right * playerInput.MoveDirectionX;
			var moveDir = FVector3.NormalizeSafe(moveDirection);
			var moveSpeed = 6.ToFP();
			rigidBody.Velocity = new FVector3(
				moveDir.X * moveSpeed,
				rigidBody.Velocity.Y,
				moveDir.Z * moveSpeed
			);

			if (moveDir != FVector3.Zero) {
				transform.Rotation = new FVector3(
					FP.Zero,
					FP.Atan2(moveDir.X, moveDir.Z),
					FP.Zero
				);
			}

			if (playerInput.Jump && rigidBody.IsGrounded) {
				rigidBody.Velocity += new FVector3(
					FP.Zero,
					15.ToFP(),
					FP.Zero
				);
			}
		});
	}
}