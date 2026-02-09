using System.Runtime.Serialization;
using Massive;

[DataContract]
public struct ViewAsset : ICopyable<ViewAsset> {
	[DataMember]
	public string PackedScenePath;

	public void CopyTo(ref ViewAsset other) {
		other.PackedScenePath = PackedScenePath?.ToString();
	}
}