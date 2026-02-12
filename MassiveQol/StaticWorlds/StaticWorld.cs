using System;

namespace Massive.QoL
{
	public class StaticWorld<TWorldType>
	{
		// ReSharper disable once StaticMemberInGenericType
		public static World Instance;

		static StaticWorld()
		{
			if (Attribute.GetCustomAttribute(typeof(TWorldType), typeof(StaticWorldTypeAttribute)) is StaticWorldTypeAttribute worldAttribute)
			{
				Instance = new World(new WorldConfig(worldAttribute.StoreEmptyTypesAsDataSets));
			}
			else
			{
				Instance = new World(new WorldConfig());
			}

			StaticWorlds.Register(typeof(TWorldType), Instance);
		}
	}
}
