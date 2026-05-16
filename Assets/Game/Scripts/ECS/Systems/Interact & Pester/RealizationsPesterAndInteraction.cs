using UnityEngine;

public static class RealizationsPesterAndInteraction
{
	/* Pesters */
	public static bool Pester_TrySvapItemHandAndInventory_AutoSet(DataPester pester, DataPesterInventory inventory, DataPesterHand hand)
	{
		if (inventory == null ||
			hand == null)
			return false;

		DataInteractItem interactItem = null;

		 if (!hand.IsFree &&
			!hand.Buffer.Entity.TryGetDataByType(out  interactItem))
			return false;

		if (inventory.CountFreeSlots > 0 &&
			Pester_TryAddItemIntoInventory(inventory, interactItem))
		{
			Interact_SetStateInInventory(pester, hand.Buffer.Data, interactItem, inventory);
			hand.IsFree = true;
			return true;
		}

		interactItem = inventory.Items[inventory.CurrentSelectSlot];

		if (!interactItem.Entity.TryGetDataByType(out DataInteractTkeible tkeibleItem))
			return false;

		if (!tkeibleItem.Entity.TryGetDataByType(out interactItem))
			return false;

		hand.IsFree = true;
		inventory.Items[inventory.CurrentSelectSlot] = null;
		inventory.CountFreeSlots++;

		if (Pester_TryAddItemIntoInventory(inventory, interactItem) &&
			Pester_TryTakeibleItemIntoHand(pester, hand))
		{
			Interact_SetStateInHand(pester, hand.Buffer.Data, tkeibleItem, hand);
			Interact_SetStateInInventory(pester, hand.Buffer.Data, interactItem, inventory);
			return true;
		}

		// remove svap
		hand.IsFree = false;
		inventory.Items[inventory.CurrentSelectSlot] = interactItem;
		inventory.CountFreeSlots--;

		return false;
	}

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

	public static bool Pester_TryTakeibleItemIntoHand(DataPester pester, DataPesterHand hand)
	{
		if (hand == null ||
			!hand.IsFree ||
			!hand.Point.Hit.transform.TryGetComponent(out Entity itemItentity) ||
			!itemItentity.TryGetDataByType(out DataInteractTkeible item))
			return false;

		int i = pester.GetIdInteractByffer(itemItentity);

		if (i == -1 ||
			i >= pester.Interacts.Count)
			return false;

		hand.Buffer = pester.Interacts[i];

		itemItentity.TryGetDataByType(out DataInteractWithItemInHand itemInteract);

		hand.IsFree = false;
		hand.Item = item;
		hand.ItemInterac = itemInteract;


		hand.Action.Invoke(hand.IdActionTake);
		item.Action.Invoke(item.IdActionTake);

		return true;
	}

	public static bool Pester_TryTakeibleItemIntoHand_AutoSet(DataPester pester, DataPesterHand hand)
	{
		if (hand == null ||
			!hand.Point.IsContact ||
			!hand.Point.Hit.transform.TryGetComponent(out Entity entityItemItentity) ||
			!entityItemItentity.TryGetDataByType(out DataInteract interact))
			return false;


		if (entityItemItentity.TryGetDataByType(out DataInteractTkeible itemTkeible))
		{
			entityItemItentity.TryGetDataByType(out DataInteractWithItemInHand itemInteract);
		
			InteractByffer byffer = new();

			byffer.Entity = entityItemItentity;
			byffer.Provider = interact.Provider;
			byffer.Data = interact;
			byffer.State = InteractState.InHand;

			pester.Interacts.Add(byffer);

			hand.Buffer = byffer;
			hand.IsFree = false;
			hand.Item = itemTkeible;
			hand.ItemInterac = itemInteract;

			hand.Action.Invoke(hand.IdActionTake);
			itemTkeible.Action.Invoke(itemTkeible.IdActionTake);

			Interact_SetStateInHand(pester, interact, itemTkeible, hand);

			Debug.Log($"hand: {hand}, IsContact: {hand.Point.IsContact}, itemItentity: {entityItemItentity}, interact: {interact}, item: {itemTkeible}");
			return true;
		}
		else if (entityItemItentity.TryGetDataByType(out DataInteractEnvironment environment))
		{
			environment.Action.Invoke(environment.CurrentState);
			environment.CurrentState++;

			if (environment.CurrentState > environment.MaxState)
				environment.CurrentState = 0;

			Debug.Log($"hand: {hand}, IsContact: {hand.Point.IsContact}, itemItentity: {entityItemItentity}, interact: {interact}, environment: {environment}");
			return true;
		}
		
		return false;
	}

	public static bool Pester_TryDropItemInHand_AutoSet(DataPester pester, DataPesterHand hand, bool isForce)
	{
		if (hand == null ||
			hand.IsFree ||
			hand.Buffer.Entity == null ||
			!hand.Buffer.Entity.TryGetDataByType(out DataInteract interact) ||
			!hand.Buffer.Entity.TryGetDataByType(out DataInteractTkeible item))
			return false;

		Entity itemItentity = hand.Buffer.Entity;

		int i = pester.GetIdInteractByffer(itemItentity);

		if (i == -1 ||
			i >= pester.Interacts.Count)
			return false;

		hand.Buffer = pester.Interacts[i];

		hand.IsFree = true;
		hand.Item = null;
		hand.ItemInterac = null;

		hand.Action.Invoke(hand.IdActionDrop);
		item.Action.Invoke(item.IdActionDrop);

		if (isForce)
		{
			Interact_SetStateDropForce(pester, interact, item, hand, pester.DropForce);
		}
		else
		{
			Interact_SetStateDropToPoint(pester, interact, item, hand);
		}

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

		item.WorldObject.transform.localPosition = Vector3.zero;


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

		pester.Interacts.RemoveAt(interactId);

		item.WorldObject.gameObject.SetActive(true);
		item.WorldObject.SetParent(null);

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