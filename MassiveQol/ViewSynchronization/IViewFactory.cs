namespace Massive.QoL
{
	public interface IViewFactory<TView>
	{
		TView CreateView(ViewAsset viewAsset);

		void DestroyView(TView view);
	}
}
