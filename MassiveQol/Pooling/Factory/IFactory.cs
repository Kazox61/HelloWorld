namespace Massive.QoL
{
	public interface IFactory<out T>
	{
		T Create();
	}
}
