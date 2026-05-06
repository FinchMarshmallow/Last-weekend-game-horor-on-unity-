using System;
using UnityEngine;

[Serializable]
public class DataInteractEnvironment : BaseData, IInteract
{
	[SerializeField] private string nameActionProvider;
	[NonSerialized] public ActionProvider Action;
	public int
		MaxState,
		CurrentState,
		OffsetState  = 1;

	public ProviderPester LastPester;

	public override BaseData Copy()
	{
		DataInteractEnvironment copy = new();

		copy.nameActionProvider = nameActionProvider;
		copy.MaxState = MaxState;
		copy.CurrentState = CurrentState;
		copy.OffsetState = OffsetState;

		return copy;
	}

	public override void Init(Entity entity)
	{
		entity.TryGetComponent(out ActionProviderManager manager);
		Action = manager.GetByName(nameActionProvider);
	}

	public InteractType GetTypeInteract() => InteractType.CanInteractAsEnvironment;
}
