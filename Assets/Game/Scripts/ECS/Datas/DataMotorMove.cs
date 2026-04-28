using UnityEngine;

public enum TypeMotorMove
{
	None = 0,
	Default = 1,
	Sprint = 2,
	Squat = 3,
}

public class DataMotorMove : BaseData
{
	public float 
		SpeedMove,
		StoppingStable,
		Gravity,
		MultySpeedDrop,
		MultySpeedSquat,
		MultySpeedSprint;

	public TypeMotorMove TypeMove;

	public override BaseData Copy()
	{
		DataMotorMove copy = new();

		copy.SpeedMove = SpeedMove;
		copy.StoppingStable = StoppingStable;
		copy.Gravity = Gravity;
		copy.MultySpeedDrop = MultySpeedDrop;
		copy.MultySpeedSquat = MultySpeedSquat;
		copy.MultySpeedSprint = MultySpeedSprint;
		copy.TypeMove = TypeMove;

		return copy;
	}
}
