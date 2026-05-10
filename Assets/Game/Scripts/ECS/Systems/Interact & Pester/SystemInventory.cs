using System;
using System.Collections.Generic;
using UnityEngine;
using UpdatesIntarfaces;

[Serializable]
public class SystemInventory : BaseSystem, IUpdate
{
	public List<Entity> _entities;
	public List<DataPester> _pesters;
	public List<DataPesterHand> _hands;
	public List<DataPesterInventory> _inventorys;
	private Filter<DataPester, DataPesterHand, DataPesterInventory> _filter;

	public override void Init()
	{
		_filter = new(out _entities, out _pesters, out _hands, out _inventorys, TagEntity.All);
		World.AddFilter(_filter);
	}

	public void Update()
	{
		/*for (int i = 0; i < _entities.Count; i++)
		{
			if (_pesters[i].Command != PesterCommand.PutIntoInventory)
				continue;


			if (_hands[i].IsFree)
			{
				if (_inventorys[i].CurrentSelectSlot < 0 &&
					_inventorys[i].CurrentSelectSlot >= _inventorys[i].MaxSlots)
				{
					continue;
				}

				int caunter = 0;
				bool error = false;
				InteractByffer interactByffer = new();
				DataInteract dataInteract = null;
				DataInteractTkeible dataTakeible = null;
				DataInteractItem dataItem = null;

				for (int j = 0; j < _pesters[i].Interacts.Count; j++)
				{
					if(_pesters[i].Interacts[j].State == InteractState.InInventory)
					{
						if (caunter >= _inventorys[i].CurrentSelectSlot)
						{
							interactByffer = _pesters[i].Interacts[j];
							dataInteract = interactByffer.Data;
							error =
								interactByffer.Entity.TryGetDataByType(out dataTakeible) &&
								interactByffer.Entity.TryGetDataByType(out dataItem);

							break;
						}
						caunter++;
					}
				}

				if (error)
					continue;

				dataInteract.State = InteractState.InHand;

				dataTakeible.WorldObject.SetParent(_hands[i].Point.Content);
				dataTakeible.WorldObject.localPosition = dataTakeible.OffsetLocalPos;
				dataTakeible.WorldObject.localRotation = Quaternion.identity;
				dataTakeible.WorldObject.Rotate(dataTakeible.OffsetLocalRotAxis);

				interactByffer.Entity.gameObject.SetActive(true);

				dataTakeible.Action.Invoke(dataTakeible.IdActionTake);

				_inventorys[i].CountFreeSlots++;
				_hands[i].IsFree = false;

				_pesters[i].Command = PesterCommand.None;
				continue;
			}
			else
			{
				if (_inventorys[i].CountFreeSlots == 0) // Svap Inventory item And Hand item
				{
					int caunter = 0;
					bool error = false;
					InteractByffer interactByffer_InInventoty = new();
					DataInteract dataInteract_InInventoty = null;
					DataInteractTkeible dataTakeible_InInventoty = null;
					DataInteractItem dataItem_InInventoty = null;

					for (int j = 0; j < _pesters[i].Interacts.Count; j++)
					{
						if (_pesters[i].Interacts[j].State == InteractState.InInventory)
						{
							if (caunter >= _inventorys[i].CurrentSelectSlot)
							{
								interactByffer_InInventoty = _pesters[i].Interacts[j];
								dataInteract_InInventoty = interactByffer_InInventoty.Data;
								error =
									!(dataInteract_InInventoty.TryGetInteractByType(out dataTakeible_InInventoty) &&
									dataInteract_InInventoty.TryGetInteractByType(out dataItem_InInventoty));

								break;
							}
							caunter++;
						}
					}

					if (error)
						continue;

					InteractByffer interactByffer_InHand = new();
					DataInteract dataInteract_InHand = null;
					DataInteractTkeible dataTakeible_InHand = null;
					DataInteractItem dataItem_InHand = null;


					for (int j = 0; j < _pesters[i].Interacts.Count; j++)
					{
						if (_pesters[i].Interacts[j].State == InteractState.InHand)
						{
							interactByffer_InHand = _pesters[i].Interacts[j];
							dataInteract_InHand = interactByffer_InHand.Data;
							error =
								!(dataInteract_InHand.TryGetInteractByType(out dataTakeible_InHand) && 
								dataInteract_InHand.TryGetInteractByType(out dataItem_InHand));
							break;
						}
					}

					if (error)
						continue;

					// item inventory move to hand
					dataInteract_InInventoty.State = InteractState.InHand;

					dataTakeible_InInventoty.WorldObject.SetParent(_hands[i].Point.Content);
					dataTakeible_InInventoty.WorldObject.localPosition = dataTakeible_InInventoty.OffsetLocalPos;
					dataTakeible_InInventoty.WorldObject.localRotation = Quaternion.identity;
					dataTakeible_InInventoty.WorldObject.Rotate(dataTakeible_InInventoty.OffsetLocalRotAxis);

					interactByffer_InInventoty.Entity.gameObject.SetActive(true);

					dataTakeible_InInventoty.Action.Invoke(dataTakeible_InInventoty.IdActionTake);
					dataItem_InInventoty.Action.Invoke(dataItem_InInventoty.IdActionLeaveInpentory);

					// item hand move to inventory
					dataInteract_InHand.State = InteractState.InInventory;

					dataTakeible_InHand .WorldObject.SetParent(_inventorys[i].Content);
					dataTakeible_InHand .WorldObject.localPosition = Vector3.zero;
					dataTakeible_InHand .WorldObject.localRotation = Quaternion.identity;

					interactByffer_InHand.Entity.gameObject.SetActive(false);

					dataTakeible_InHand.Action.Invoke(dataTakeible_InHand.IdActionDrop);
					dataItem_InInventoty.Action.Invoke(dataItem_InInventoty.IdActionIntoInpentory);


					_pesters[i].Command = PesterCommand.None;
					continue;
				}

				if (_inventorys[i].CountFreeSlots > 0)
				{

					InteractByffer interactByffer = new();
					DataInteract dataInteract = null;
					DataInteractTkeible dataTakeible = null;

					for (int j = 0; j < _pesters[i].Interacts.Count; j++)
					{
						if (_pesters[i].Interacts[j].State == InteractState.InHand)
						{
							interactByffer = _pesters[i].Interacts[j];
							dataInteract = interactByffer.Data;
							dataInteract.TryGetInteractByType(out dataTakeible);
							break;
						}
					}

					if (dataInteract == null ||
						interactByffer.Entity == null)
						continue;

					interactByffer.Entity.gameObject.SetActive(false);
					dataInteract.State = InteractState.InInventory;

					_inventorys[i].CountFreeSlots--;
					_hands[i].IsFree = true;

					_pesters[i].Command = PesterCommand.None;
					continue;
				}
			}
		}*/

		for (int i = 0; i < _entities.Count; i++)
		{
			DataPester pester = _pesters[i];

			if (pester.Command == PesterCommand.None)
				continue;

			DataPesterHand hand = _hands[i];
			DataPesterInventory inventory = _inventorys[i];

			switch (pester.Command)
			{
				case PesterCommand.None:
					break;

				case PesterCommand.Inventory:

					int idPester = _pesters[i].GetIdInteractByffer(_entities[i]);

					DataInteract dataInteract = _pesters[i].Interacts[idPester].Data;
					DataInteractItem dataItem = dataInteract.GetInteractByType<DataInteractItem>();

					if (RealizationsPesterAndInteraction.Pester_TryAddItemIntoInventory(_inventorys[i], dataItem))
					{

					}

					break;
			}
		}
	}
}
