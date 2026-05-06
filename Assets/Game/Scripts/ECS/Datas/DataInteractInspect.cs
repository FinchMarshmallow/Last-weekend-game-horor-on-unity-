using System;
using UnityEngine;

[Serializable]
public class DataInteractInspect : BaseData
{
	[SerializeField] private string nameActionProvider;
	[NonSerialized] public ActionProvider Action;
	public int
		IdActionStart,
		IdActionEnd;

	public override BaseData Copy()
	{
		DataInteractInspect copy = new();

		copy.nameActionProvider = nameActionProvider;
		copy.IdActionEnd = IdActionEnd;
		copy.IdActionStart = IdActionStart;

		return copy;
	}

	public override void Init(Entity entity)
	{
		entity.TryGetComponent(out ActionProviderManager manager);
		Action = manager.GetByName(nameActionProvider);
	}
}