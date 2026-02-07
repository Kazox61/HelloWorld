using Fixed64;
using Massive.Common;
using Massive.Netcode;
using Massive.Physics.Components;

namespace Massive.Physics.Systems;

public class PhysicsIntegrationSystem : NetSystem, IUpdate {
	public void Update() {
		World.ForEach((ref Transform transform, ref RigidBody body) => {
			if (body.InverseMass == FP.Zero) {
				return;
			}

			transform.Position += body.Velocity * (FP.One / Session.Config.TickRate.ToFP());
		});
	}
}