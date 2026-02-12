namespace Massive.QoL
{
	public interface IPoolReturn<in TItem>
	{
		void Return(TItem item);
	}
}
