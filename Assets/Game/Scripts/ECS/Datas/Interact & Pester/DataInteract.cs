using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable, Flags]
public enum InteractType
{
	None = 0,
	CanInteractAsEnvironment = 1,
	CanTakeInHand = 1 << 1,
	CanMoveIntoInventory = 1 << 2,
	CanInteractAsHandItem = 1 << 3,
	CanInspect = 1 << 4,
}

public interface IInteract
{
	public InteractType GetTypeInteract();
}

public struct PesterByffer
{
	public DataPester Data;
	public ProviderPester Provider;
	public Entity Entity;

	public PesterByffer(Entity entity, DataPester data, ProviderPester provider)
	{
		this.Entity = entity;
		this.Data = data;
		this.Provider = provider;
	}
}

[Serializable]
public class DataInteract : BaseData
{
	public ProviderInteract Provider { get; private set; }
	public InteractType HowCanInteract;
	public InteractState State;
	public PesterByffer CurrentPester;

	public List<IInteract> Interacts;

	[NonReorderable] public Entity Entity;

	public override BaseData Copy()
	{
		DataInteract copy = new();
		return copy;
	}

	public override void Init(Entity entity)
	{
		Entity = entity;
		Provider = entity.GetComponent<ProviderInteract>();

		if (entity.TryGetAllDataByType(out Interacts))
		{
			for (int i = 0; i < Interacts.Count; i++)
			{
				HowCanInteract ^= Interacts[i].GetTypeInteract();
			}
		}
	}

	public T GetInteractByType<T>() where T : class, IInteract, new()
	{
		{
			T t = new T();
			if ((HowCanInteract & t.GetTypeInteract()) == 0)
				return null;
		}

		for (int i = 0; i < Interacts.Count; i++)
		{
			if (Interacts[i] is T t)
				return t;
		}

		return null;
	}

	public bool TryGetInteractByType<T>(out T t) where T : class, IInteract, new()
	{
		t = new();

		if ((HowCanInteract & t.GetTypeInteract()) == 0)
			return false;

		for (int i = 0; i < Interacts.Count; i++)
		{
			if (Interacts[i] is T tByffer)
			{
				t = tByffer;
				return true;
			}
		}

		return false;
	}
}
