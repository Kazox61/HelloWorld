using HelloWorld.Core.Components;
using Massive;
using Massive.Common;
using Massive.Netcode;

namespace HelloWorld.Core.Systems;

public class CharacterDespawnSystem : NetSystem, IUpdate {
	public void Update() {
		foreach (var (channel, _) in Inputs.GetAllEvents<PlayerDisconnectedEvent>()) {
			foreach (var player in World.Include<Player>().Entities) {
				if (player.Get<Player>().InputChannel == channel) {
					player.Destroy();
				}
			}
		}
	}
}