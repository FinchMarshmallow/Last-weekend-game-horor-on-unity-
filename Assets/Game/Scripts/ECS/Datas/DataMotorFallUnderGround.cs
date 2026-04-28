using UnityEngine;

public class DataMotorFallUnderGround : BaseData
{
	public LayerMask MaskImpassable;

	public override BaseData Copy()
	{
		DataMotorFallUnderGround copy = new();
		copy.MaskImpassable = MaskImpassable;
		return copy;
	}
}
