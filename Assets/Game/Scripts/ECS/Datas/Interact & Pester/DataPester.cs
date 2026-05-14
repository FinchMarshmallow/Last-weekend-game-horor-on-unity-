using System;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

[Serializable]
public enum PesterCommand
{
	None,
	
	TakeInteract,
	
	Drop,
	DropForce,
	
	Inventory,

	InspectSwitchState,
	Inspecting,
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
	public ProviderPester Provider;
	public PesterCommand Command;
	public float DropForce = 0f;

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
		entity.TryGetComponent(out ActionProviderManager manager);
		Action = manager.GetByName(nameActionProvider);
	}


	 /* get by entity */
	public int GetIdInteractByffer(Entity entity)
	{
		for (int i = 0; i < Interacts.Count; i++)
			if (Interacts[i].Entity == entity) return i;

		return -1;
	}

	/* get by provider */
	public int GetIdInteractByffer(ProviderInteract provider)
	{
		for (int i = 0; i < Interacts.Count; i++)
			if (Interacts[i].Provider == provider) return i;

		return -1;
	}

	/* get by data */
	public int GetIdInteractByffer(DataInteract data)
	{
		for (int i = 0; i < Interacts.Count; i++)
			if (Interacts[i].Data == data) return i;

		return -1;
	}
}
