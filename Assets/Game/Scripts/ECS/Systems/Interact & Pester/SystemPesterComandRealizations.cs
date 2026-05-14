using System;
using System.Collections.Generic;
using UnityEngine;
using UpdatesIntarfaces;

[Serializable]
public class SystemPesterComandRealizations : BaseSystem, IUpdate
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
		for (int i = 0; i < _entities.Count; i++)
		{
			if (_pesters[i].Command == PesterCommand.None)
				continue;
		
			Debug.Log($"{i}, {_pesters[i].Command}");

			switch (_pesters[i].Command)
			{

				case PesterCommand.Inventory:

					RealizationsPesterAndInteraction.Pester_TrySvapItemHandAndInventory_AutoSet(_pesters[i], _inventorys[i], _hands[i]);
					_pesters[i].Command = PesterCommand.None;

					break;

				case PesterCommand.TakeInteract:

					RealizationsPesterAndInteraction.Pester_TryTakeibleItemIntoHand_AutoSet(_pesters[i], _hands[i]);
					_pesters[i].Command = PesterCommand.None;

					break;

				case PesterCommand.Drop:

					RealizationsPesterAndInteraction.Pester_TryDropItemInHand_AutoSet(_pesters[i], _hands[i]);
					_pesters[i].Command = PesterCommand.None;

					break;

				case PesterCommand.DropForce:

					RealizationsPesterAndInteraction.Pester_TryDropItemInHand_AutoSet(_pesters[i], _hands[i], true);
					_pesters[i].Command = PesterCommand.None;

					break;

				default:
					_pesters[i].Command = PesterCommand.None;
#if UNITY_EDITOR
					Debug.LogError($"SystemInventory: Update: Command: {_pesters[i].Command} : no realizations");
#endif
					break;
			}
		}
	}
}
