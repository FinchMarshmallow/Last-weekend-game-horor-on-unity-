using System;
using UnityEngine;

[Serializable]
public class DataInteractTkeible : BaseData, IInteract
{
	[SerializeField] private string nameActionProvider;
	[NonSerialized] public ActionProvider Action;
	public int
		IdActionDrop,
		IdActionTake;
	public Sprite SlotHandImg;
	public Transform WorldObject;
	public Vector3 OffsetLocalPos, OffsetLocalRotAxis;
	public float OffsetRotAngle;

	public override BaseData Copy()
	{
		DataInteractTkeible copy = new();

		copy.nameActionProvider = nameActionProvider;
		copy.IdActionDrop = IdActionDrop;
		copy.IdActionTake = IdActionTake;
		copy.SlotHandImg = SlotHandImg;
		copy.WorldObject = WorldObject;
		copy.OffsetLocalPos = OffsetLocalPos;
		copy.OffsetLocalRotAxis = OffsetLocalRotAxis;
		copy.OffsetRotAngle = OffsetRotAngle;

		return copy;
	}

	public override void Init(Entity entity)
	{
		entity.TryGetComponent(out ActionProviderManager manager);
		Action = manager.GetByName(nameActionProvider);
	}

	public InteractType GetTypeInteract() => InteractType.CanTakeInHand;
}