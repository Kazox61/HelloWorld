using Fixed64;
using Massive.Common;
using Massive.Netcode;
using Massive.Physics.Components;

namespace Massive.Physics.Systems;

public class PhysicsGravitySystem : NetSystem, IUpdate {
	private static readonly FVector3 Gravity = new(FP.Zero, (-20).ToFP(), FP.Zero); 

	public void Update() {
		World.ForEach((ref RigidBody body) => {
			body.IsGrounded = false;
			
			if (body.InverseMass == FP.Zero || !body.UseGravity) {
				return;
			}

			body.Velocity += Gravity * (FP.One / Session.Config.TickRate.ToFP());
		});
	}
}