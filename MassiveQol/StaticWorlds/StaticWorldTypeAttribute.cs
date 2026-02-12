using System;

namespace Massive.QoL
{
	[AttributeUsage(AttributeTargets.Struct)]
	public class StaticWorldTypeAttribute : Attribute
	{
		public readonly bool StoreEmptyTypesAsDataSets;

		public StaticWorldTypeAttribute(int pageSize = Constants.PageSize, bool storeEmptyTypesAsDataSets = false)
		{
			StoreEmptyTypesAsDataSets = storeEmptyTypesAsDataSets;
		}
	}
}
