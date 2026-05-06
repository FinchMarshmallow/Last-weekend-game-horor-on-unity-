using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum PesterCommand
{
	None,
	TakeInteract,
	Drop,
	ThrowWithForce,
	InspectSwitchState,
	Inspecting,
	PutIntoInventory,
}

[Serializable]
public enum InteractState
{
	None,
	InHand,
	InInventory,
}

[Serializable]
public struct InteractByffer
{
	public InteractState State;
	public Entity Entity;
	public ProviderInteract Provider;
	public DataInteract Data;
}

[Serializable]
public class DataPester : BaseData
{
	[SerializeField] private string nameActionProvider;
	[NonSerialized] public ActionProvider Action;
	public int
		IdActionNone,
		IdActionTakeInteract,
		IdActionDrop,
		IdActionThrowWithForce,
		IdActionIdActionInspectStart,
		IdActionIdActionInspecting,
		IdActionIdActionInspectEnd;

	public List<InteractByffer> Interacts;
	[NonSerialized] public ProviderPester Provider;
	public PesterCommand Command;
	public float DropForce = 0f;
	//public InteractType CurrentIntaracts;

	public override BaseData Copy()
	{
		DataPester copy = new();

		copy.nameActionProvider = nameActionProvider;
		copy.IdActionNone = IdActionNone;
		copy.IdActionTakeInteract = IdActionTakeInteract;
		copy.IdActionDrop = IdActionDrop; 
		copy.IdActionThrowWithForce = IdActionThrowWithForce;
		copy.IdActionIdActionInspectStart = IdActionIdActionInspectStart;
		copy.IdActionIdActionInspecting = IdActionIdActionInspecting;
		copy.IdActionIdActionInspectEnd = IdActionIdActionInspectEnd;

		return copy;
	}

	public override void Init(Entity entity)
	{
		entity.TryGetComponent(out Provider);
		entity.TryGetComponent(out ActionProviderManager manager);
		Action = manager.GetByName(nameActionProvider);
	}

/*	public void UpdateInteractMask()
	{
		CurrentIntaracts = 0;

		for (int i = 0; i < Interacts.Count; i++)
		{
			CurrentIntaracts ^= Interacts[i].Data.HowCanInteract;
		}
	}*/
}
