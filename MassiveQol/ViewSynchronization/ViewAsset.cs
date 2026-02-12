using System;
using System.Runtime.CompilerServices;

namespace Massive.QoL
{
	[Serializable, Component]
	public struct ViewAsset : IEquatable<ViewAsset>
	{
		public int IdPlusOne;

		/// <summary>
		/// Negative view ID is invalid.
		/// </summary>
		public int Id
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => IdPlusOne - 1;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => IdPlusOne = value + 1;
		}

		public ViewAsset(int id)
		{
			IdPlusOne = id + 1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(ViewAsset a, ViewAsset b)
		{
			return a.Equals(b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(ViewAsset a, ViewAsset b)
		{
			return !(a == b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(ViewAsset other)
		{
			return IdPlusOne == other.IdPlusOne;
		}

		public override bool Equals(object obj)
		{
			return obj is ViewAsset other && Equals(other);
		}

		public override int GetHashCode()
		{
			return IdPlusOne;
		}
	}
}
