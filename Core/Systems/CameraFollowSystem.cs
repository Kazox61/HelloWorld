using HelloWorld.Core.Components;
using Massive;
using Massive.Common;
using Massive.Netcode;

namespace HelloWorld.Core.Systems;

public class CameraFollowSystem : NetSystem, IUpdate {
	public void Update() {
		World.ForEach((Entity _, ref Camera _, ref CameraTarget cameraTarget, ref Transform transform) => {
			var targetEntity = cameraTarget.TargetEntifier.In(World);
			if (!targetEntity.IsAlive) {
				return;
			}
			
			var playerTransform = targetEntity.Get<Transform>();
			var desiredPosition = playerTransform.Position + cameraTarget.Offset;
			transform.Position = desiredPosition;
		});
	}
}