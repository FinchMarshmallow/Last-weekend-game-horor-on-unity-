using System;
using UnityEngine;

[Serializable]
public class DataPesterInventory : BaseData
{
	// Select Slot -1 it free slot 
	public int MaxSlots, CurrentFreeSlots, CurrentSelectSlot;

	[NonSerialized] public Transform Content;

	public override BaseData Copy()
	{
		DataPesterInventory copy = new();

		copy.MaxSlots = MaxSlots;
		copy.CurrentFreeSlots = CurrentFreeSlots;

		return copy;
	}

	public override void Init(Entity entity)
	{
		Content = entity.transform;
	}
}