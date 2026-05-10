using System;
using System.Collections.Generic;
using UnityEngine;
using UpdatesIntarfaces;

[Serializable]
public class SystemHandTakeAndDrop : BaseSystem, IUpdate
{
	public List<Entity> _entities;
	public List<DataPester> _pesters;
	public List<DataPesterHand> _hands;
	private Filter<DataPester, DataPesterHand> _filter;

	public override void Init()
	{
		_filter = new(out _entities, out _pesters, out _hands, TagEntity.Player);
		World.AddFilter(_filter);
	}

	public void Update()
	{

		for (int i = 0; i < _entities.Count; i++)
		{
			ProviderPester providerPester = _pesters[i].Provider;

			if (providerPester.CurrentSelectObject.Entity != null &&
				_pesters[i].Command == PesterCommand.TakeInteract)
			{
				DataInteract dataInteract = providerPester.CurrentSelectObject.Data;

				// Take item into Hand
				if ((dataInteract.HowCanInteract & InteractType.CanTakeInHand) != 0 &&
					dataInteract.TryGetInteractByType(out DataInteractTkeible dataTakeible) &&
					_hands[i].IsFree)
				{
					_hands[i].IsFree = false;
					InteractByffer interactByffer = providerPester.CurrentSelectObject;
					interactByffer.State = InteractState.InHand;

					_pesters[i].Interacts.Add(interactByffer);

					dataTakeible.WorldObject.SetParent(_hands[i].Point.Content);
					dataTakeible.WorldObject.localPosition = dataTakeible.OffsetLocalPos;
					dataTakeible.WorldObject.localRotation = Quaternion.identity;
					dataTakeible.WorldObject.Rotate(dataTakeible.OffsetLocalRotAxis);
					dataTakeible.Action.Invoke(dataTakeible.IdActionTake);

					_pesters[i].Command = PesterCommand.None;
					continue;
				}

				// Iteract with Environment
				if ((dataInteract.HowCanInteract & InteractType.CanInteractAsEnvironment) != 0 &&
					dataInteract.TryGetInteractByType(out DataInteractEnvironment dataTEnvironment))
				{
					dataTEnvironment.CurrentState += dataTEnvironment.OffsetState;

					if (dataTEnvironment.CurrentState > dataTEnvironment.MaxState)
						dataTEnvironment.CurrentState = 0;

					dataTEnvironment.LastPester = providerPester;
					dataTEnvironment.Action.Invoke(dataTEnvironment.CurrentState);

					_pesters[i].Command = PesterCommand.None;
					continue;
				}
			}
			else if (!_hands[i].IsFree && (
				_pesters[i].Command == PesterCommand.Drop ||
				_pesters[i].Command == PesterCommand.DropForce))
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

				// Drop item in hand
				if (_pesters[i].Command == PesterCommand.Drop)
				{
					_hands[i].IsFree = false;

					Vector3 
						up = Vector3.up,
						pos = _hands[i].Point.Pos;

					if (_hands[i].Point.IsContact)
					{
						RaycastHit hit = _hands[i].Point.Hit;
						up = hit.normal;
						pos = hit.point;
					}

					dataInteract.State = InteractState.None;

					dataTakeible.Action.Invoke(dataTakeible.IdActionDrop);
					dataTakeible.WorldObject.SetParent(null);
					dataTakeible.WorldObject.position = pos;
					dataTakeible.WorldObject.up = up;

					_pesters[i].Command = PesterCommand.None;
					continue;
				}

				// Drop throw with force item in hand
				if (_pesters[i].Command == PesterCommand.DropForce)
				{
					_hands[i].IsFree = false;
					dataInteract.State = InteractState.None;

					dataTakeible.Action.Invoke(dataTakeible.IdActionDrop);
					dataTakeible.WorldObject.SetParent(null);
					interactByffer.Entity.Rb.AddRelativeForce(Vector3.forward * _hands[i].MaxForce * _pesters[i].DropForce, ForceMode.Impulse);

					_pesters[i].Command = PesterCommand.None;
					continue;
				}
			}

		}

	}
}