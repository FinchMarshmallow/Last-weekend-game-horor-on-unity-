using System;
using UnityEngine;

[Serializable]
public class DataPesterHand : BaseData
{
	public HandPoint Point;
	public bool IsFree;
	public float MaxForce;

	public override BaseData Copy()
	{
		DataPesterHand copy = new();

		return copy;
	}

	public override void Init(Entity entity)
	{
		entity.TryGetComponent(out Point);
	}
}