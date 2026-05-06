using System;
using UnityEngine;

[Serializable]
public class DataInteractInventoriable : BaseData, IInteract
{
	[SerializeField] private string nameActionProvider;
	[NonSerialized] public ActionProvider Action;
	public int
		IdActionLeaveInpentory,
		IdActionIntoInpentory;

	public Sprite SlotImg;
	public Transform WorldObject;

	public override BaseData Copy()
	{
		DataInteractInventoriable copy = new();

		copy.nameActionProvider = nameActionProvider;
		copy.IdActionLeaveInpentory = IdActionLeaveInpentory;
		copy.IdActionIntoInpentory = IdActionIntoInpentory;

		return copy;
	}

	public override void Init(Entity entity)
	{
		entity.TryGetComponent(out ActionProviderManager manager);
		Action = manager.GetByName(nameActionProvider);
		WorldObject = entity.transform;
	}

	public InteractType GetTypeInteract() => InteractType.CanMoveIntoInventory;
}