using Fixed64;
using HelloWorld.Core.Components;
using Massive;
using Massive.Common;
using Massive.Netcode;
using Massive.Physics.Components;
using Massive.QoL;

namespace HelloWorld.Core.Systems;

public class StartSystem : NetSystem, IFirstTick {
	public void FirstTick() {
		var floor = World.CreateEntity();
		floor.Set(new Transform {
			Position = new FVector3(FP.Zero, FP.MinusOne, FP.Zero)
		});
		floor.Set(new RigidBody {
			InverseMass = FP.Zero,
			Restitution = FP.Zero,
			Friction = FP.One
		});
		floor.Set(new BoxCollider {
			HalfExtents = new FVector3(100.ToFP(), FP.One, 100.ToFP())
		});

		for (var i = 0; i < 6; i++) {
			var obstacle = World.CreateEntity();
			obstacle.Set(new Transform { Position = new FVector3((-18 + i * 6).ToFP(), (i * 0.5 + 0.5).ToFP(), (-10).ToFP()) });
			obstacle.Set(new ViewAsset(4));
			obstacle.Set(new RigidBody {
				Velocity = FVector3.Zero,
				InverseMass = FP.Zero,
				Restitution = FP.Zero,
				Friction = FP.Half
			});
			obstacle.Set(new BoxCollider {
				HalfExtents = new FVector3(FP.Two, (1 + i).ToFP() * FP.Half, FP.Two)
			});
			obstacle.Set(new PrototypeMesh { Size = new FVector3(4.ToFP(), (1 + i).ToFP(), 4.ToFP())});
		}
		
		var enemy = World.CreateEntity(new Enemy());
		enemy.Set(new Transform { Position = new FVector3(FP.Zero, FP.One, FP.Zero) });
		enemy.Set(new ViewAsset(2));
		enemy.Set(new RigidBody {
			Velocity = FVector3.Zero,
			InverseMass = FP.Zero,
			Restitution = FP.Zero,
			Friction = FP.Half
		});
		enemy.Set(new BoxCollider {
			HalfExtents = new FVector3(FP.Half, FP.One, FP.Half)
		});
		enemy.Set(new Health { Value = 1, MaxValue = 1 });
	}
}