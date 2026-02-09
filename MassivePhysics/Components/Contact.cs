using Fixed64;

namespace Massive.Physics.Components;

public struct Contact {
	public Entifier EntifierA;
	public Entifier EntifierB;
	public Collision Collision;
	
	public bool IsTrigger;
}