using Fixed64;
using HelloWorld.Core.Components;
using HelloWorld.Core.Input;
using Massive;
using Massive.Common;
using Massive.Netcode;
using Massive.Physics.Components;

namespace HelloWorld.Core.Systems;

public class MovementSystem : NetSystem, IUpdate {
	public void Update() {
		World.ForEach((Entity entity, ref Player player, ref RigidBody rigidBody, ref Transform transform) => {
			var playerInput = Inputs.GetInput<PlayerInput>(player.InputChannel).LastFresh();
			var moveDir = new FVector3(playerInput.DirectionX.ToFP(), FP.Zero, playerInput.DirectionY.ToFP());
			if (moveDir != FVector3.Zero) {
				moveDir = FVector3.Normalize(moveDir);
			}

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

			if (playerInput.Jump) {
				rigidBody.Velocity += new FVector3(
					FP.Zero,
					7.ToFP(),
					FP.Zero
				);
			}

			if (playerInput.Kill) {
				entity.Destroy();
			}
		});
	}
}