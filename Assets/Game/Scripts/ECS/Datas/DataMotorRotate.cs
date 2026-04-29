using System;
using UnityEngine;

[Serializable]
public class DataMotorRotate : BaseData
{
	public Quaternion TargetRotate = Quaternion.identity;

	public override BaseData Copy()
	{
		DataMotorRotate copy = new ();
		copy.TargetRotate = TargetRotate;
		return new DataMotorRotate();
	}
}