using UnityEngine;

public struct KeysCodes
{
	public KeyCode
		Forward,
		Back,
		Left,
		Right,
		Stels,
		Sprint,
		Jump,
		Pause,

		InteractOrTake,
		Drop,
		Inspect,
		PutIntoInventory;

	public Vector2 SentityMause;

	public float StartPressTimeDropForce, MaxPressTimeDropForce;
}

public static class ConfigKeyCode
{
	public static KeysCodes Current, Default;

	static ConfigKeyCode()
	{
		Default.Forward = KeyCode.W;
		Default.Back = KeyCode.S;
		Default.Left = KeyCode.A;
		Default.Right = KeyCode.D;
		Default.Stels = KeyCode.LeftShift;
		Default.Sprint = KeyCode.LeftAlt;
		Default.Jump = KeyCode.Space;
		Default.Pause = KeyCode.Escape;

		Default.InteractOrTake = KeyCode.E;
		Default.Drop = KeyCode.Q;
		Default.Inspect = KeyCode.Tab;

		Default.SentityMause = Vector2.one * 0.5f;

		Current = Default;
	}
}
