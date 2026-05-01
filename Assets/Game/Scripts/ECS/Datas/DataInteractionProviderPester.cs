using UnityEngine;

public class DataInteractionProviderPester : BaseData
{
	[SerializeField] private ProviderPesterItem provider;

	public IDataPesterProvider Pester => provider;

	public override BaseData Copy()
	{
		DataInteractionProviderPester copy = new();

		copy.provider = provider;

		return copy;
	}
}