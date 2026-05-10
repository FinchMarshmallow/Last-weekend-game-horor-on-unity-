using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataPesterInventory : BaseData
{
	public int MaxSlots, CountFreeSlots, CurrentSelectSlot;

	[NonSerialized] public Transform Content;
	[NonSerialized] public DataInteractItem[] Items;

	[SerializeField] private string nameActionProvider;
	[NonSerialized] public ActionProvider Action;
	public int
		IdActionAddItem,
		IdActionRemoveItem;

	public override BaseData Copy()
	{
		DataPesterInventory copy = new();

		copy.MaxSlots = MaxSlots;
		copy.CountFreeSlots = MaxSlots; // new inventory free

		return copy;
	}

	public override void Init(Entity entity)
	{
		Items = new DataInteractItem[MaxSlots];
		Content = entity.transform;
		entity.TryGetComponent(out ActionProviderManager manager);
		Action = manager.GetByName(nameActionProvider);
	}
}