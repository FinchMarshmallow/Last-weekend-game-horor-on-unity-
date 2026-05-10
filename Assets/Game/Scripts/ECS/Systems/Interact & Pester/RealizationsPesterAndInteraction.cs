using UnityEngine;

public static class RealizationsPesterAndInteraction
{
	/* Pesters */
	public static bool Pester_TryAddItemIntoInventory(DataPesterInventory inventory, DataInteractItem item)
	{
		if (inventory == null ||
			item == null ||
			inventory.CountFreeSlots <= 0)
			return false;

		if (inventory.Items[inventory.CurrentSelectSlot] == null)
		{
			inventory.Items[inventory.CurrentSelectSlot] = item;
			inventory.CountFreeSlots--;

			inventory.Action.Invoke(inventory.IdActionAddItem);
			item.Action.Invoke(item.IdActionIntoInpentory);

			return true;
		}
		else
		{
			for (int i = 0; i < inventory.Items.Length; i++)
			{
				if (inventory.Items[i] == null)
				{
					inventory.Items[i] = item;
					inventory.CountFreeSlots--;

					inventory.Action.Invoke(inventory.IdActionAddItem);
					item.Action.Invoke(item.IdActionIntoInpentory);

					return true;
				}
			}
		}

		return false;
	}

	public static bool Pester_TryRemoveItemFromInventory(DataPesterInventory inventory, DataInteractItem item)
	{
		if (inventory == null ||
			item == null || 
			inventory.CountFreeSlots >= inventory.MaxSlots)
			return false;

		for(int i = 0; i < inventory.Items.Length; i++)
		{
			if(inventory.Items[i] == item)
			{
				inventory.Items[i] = null;
				inventory.CountFreeSlots++;

				inventory.Action.Invoke(inventory.IdActionRemoveItem);
				item.Action.Invoke(item.IdActionLeaveInpentory);

				return true;
			}
		}

		return false;
	}

	public static bool Pester_TryTakeibleItemIntoHand(DataPesterHand hand, DataInteractTkeible item, DataInteractWithItemInHand itemInteract = null)
	{
		if (hand == null || 
			item == null ||
			!hand.IsFree)
			return false;

		hand.IsFree = false;
		hand.Item = item;
		hand.ItemInterac = itemInteract;

		hand.Action.Invoke(hand.IdActionTake);
		item.Action.Invoke(item.IdActionTake);

		return true;
	}

	public static bool Pester_TryDropItemInHand(DataPesterHand hand, DataInteractTkeible item)
	{
		if (hand == null ||
			item == null || 
			hand.Item != item)
			return false;

		hand.IsFree = true;
		hand.Item = null;
		hand.ItemInterac = null;

		hand.Action.Invoke(hand.IdActionDrop);
		item.Action.Invoke(item.IdActionDrop);

		return true;
	}

	/* Interacts */
	public static void Interact_SetStateInInventory(DataPester pester, DataInteract interact, DataInteractItem item, DataPesterInventory inventory)
	{
		if (inventory == null ||
			pester == null ||
			interact == null ||
			item == null)
			return;

		int interactId = pester.GetIdInteractByffer(interact);

		if (interactId == -1 || interactId >= pester.Interacts.Count)
			return;

		item.WorldObject.gameObject.SetActive(false);
		item.WorldObject.SetParent(inventory.Content);
		item.WorldObject.localPosition = Vector3.zero;
		item.WorldObject.localRotation = Quaternion.identity;

		InteractByffer byffer = pester.Interacts[interactId];
		byffer.State = InteractState.InInventory;
		pester.Interacts[interactId] = byffer;
	}

	public static void Interact_SetStateInHand(DataPester pester, DataInteract interact, DataInteractTkeible item, DataPesterHand hand)
	{
		if (hand == null ||
			pester == null ||
			interact == null ||
			item == null)
			return;

		int interactId = pester.GetIdInteractByffer(interact);

		if (interactId == -1 || interactId >= pester.Interacts.Count)
			return;

		interact.Entity.Rb.isKinematic = true;
		item.WorldObject.gameObject.SetActive(true);
		item.WorldObject.SetParent(hand.Point.Content);
		item.WorldObject.localPosition = item.OffsetLocalPos;
		item.WorldObject.localRotation = Quaternion.AngleAxis(item.OffsetRotAngle, item.OffsetLocalRotAxis);

		InteractByffer byffer = pester.Interacts[interactId];
		byffer.State = InteractState.InHand;
		pester.Interacts[interactId] = byffer;
	}

	public static void Interact_SetStateDropToPoint(DataPester pester, DataInteract interact, DataInteractTkeible item, DataPesterHand hand)
	{
		if (hand == null ||
			pester == null ||
			interact == null ||
			item == null)
			return;

		int interactId = pester.GetIdInteractByffer(interact);

		if (interactId == -1 || interactId >= pester.Interacts.Count)
			return;

		item.WorldObject.gameObject.SetActive(true);
		item.WorldObject.SetParent(null);

		if (hand.Point.IsContact)
		{
			item.WorldObject.position = hand.Point.Hit.point;
			item.WorldObject.up = hand.Point.Hit.normal;

		}
		else
		{
			item.WorldObject.up = Vector3.up;
			item.WorldObject.position = hand.Point.CastPointNoContact;
		}

		InteractByffer byffer = pester.Interacts[interactId];
		byffer.State = InteractState.None;
		pester.Interacts[interactId] = byffer;
		interact.Entity.Rb.isKinematic = false;
	}

	public static void Interact_SetStateDropForce(DataPester pester, DataInteract interact, DataInteractTkeible item, DataPesterHand hand, float inputForce)
	{
		if (hand == null ||
			pester == null ||
			interact == null ||
			item == null)
			return;

		int interactId = pester.GetIdInteractByffer(interact);

		if (interactId == -1 || interactId >= pester.Interacts.Count)
			return;

		item.WorldObject.gameObject.SetActive(true);
		item.WorldObject.SetParent(null);

		interact.Entity.Rb.isKinematic = false;
		interact.Entity.Rb.AddForce(hand.Point.Content.forward * hand.MaxForce * inputForce, ForceMode.Impulse);

		InteractByffer byffer = pester.Interacts[interactId];
		byffer.State = InteractState.None;
		pester.Interacts[interactId] = byffer;
	}
}