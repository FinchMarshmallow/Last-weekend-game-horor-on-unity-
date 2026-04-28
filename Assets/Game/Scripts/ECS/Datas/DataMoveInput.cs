using System;
using UnityEngine;

[Serializable]
public class DataMoveInput : BaseData
{
	public Vector3 Direct;

	public override BaseData Copy()
	{
		return new DataMoveInput();
	}
}

