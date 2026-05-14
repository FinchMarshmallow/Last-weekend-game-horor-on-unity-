using System;
using UnityEngine;

[Serializable]
public class DataPesterHand : BaseData
{
	public HandPoint Point;
	public bool IsFree;
	public float MaxForce;

	[NonSerialized] public InteractByffer Buffer;
	[NonSerialized] public DataInteractTkeible Item;
	[NonSerialized] public DataInteractWithItemInHand ItemInterac;

	[SerializeField] private string nameActionProvider;
	[NonSerialized] public ActionProvider Action;
	public int
		IdActionTake,
		IdActionDrop;

	public override BaseData Copy()
	{
		DataPesterHand copy = new();
		return copy;
	}

	public override void Init(Entity entity)
	{
		entity.TryGetComponent(out ActionProviderManager manager);
		Action = manager.GetByName(nameActionProvider);
	}
}