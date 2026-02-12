namespace Massive.QoL
{
	public interface IPool<TItem> : IPoolReturn<TItem>
	{
		TItem Get();
	}
}
