namespace Massive.QoL
{
	public class ViewSynchronizer<TView> where TView : IEntityView
	{
		private readonly IViewFactory<TView> _viewFactory;

		public DataSet<ViewInstance<TView>> ViewInstances { get; } = new DataSet<ViewInstance<TView>>();

		public ViewSynchronizer(IViewFactory<TView> viewFactory)
		{
			_viewFactory = viewFactory;
		}

		public void SynchronizeAll(World world)
		{
			var viewAssets = world.DataSet<ViewAsset>();

			foreach (var id in ViewInstances)
			{
				var viewInstance = ViewInstances.Get(id);
				if (!viewAssets.Has(id) || !viewAssets.Get(id).Equals(viewInstance.Asset))
				{
					DestroyViewInstance(id, viewInstance);
				}
			}

			foreach (var id in viewAssets)
			{
				var viewAsset = viewAssets.Get(id);
				if (!ViewInstances.Has(id))
				{
					CreateViewInstance(viewAsset, world.GetEntity(id));
				}
			}
		}

		public void SynchronizeSingle(Entity entity)
		{
			var viewAsset = entity.Get<ViewAsset>();

			if (ViewInstances.Has(entity.Id))
			{
				var viewInstance = ViewInstances.Get(entity.Id);

				// If instance is alright, nothing to be done.
				if (viewInstance.Asset.Equals(viewAsset))
				{
					return;
				}
				else
				{
					DestroyViewInstance(entity.Id, viewInstance);
				}
			}

			CreateViewInstance(viewAsset, entity);
		}

		private void DestroyView(int entityId)
		{
			if (ViewInstances.Has(entityId))
			{
				DestroyViewInstance(entityId, ViewInstances.Get(entityId));
			}
		}

		private void CreateViewInstance(ViewAsset viewAsset, Entity entity)
		{
			var view = _viewFactory.CreateView(viewAsset);

			view.AssignEntity(entity);

			ViewInstances.Set(entity.Id, new ViewInstance<TView>() { Instance = view, Asset = viewAsset });
		}

		private void DestroyViewInstance(int entityId, ViewInstance<TView> viewInstance)
		{
			viewInstance.Instance.RemoveEntity();

			_viewFactory.DestroyView(viewInstance.Instance);

			ViewInstances.Remove(entityId);
		}
	}
}
