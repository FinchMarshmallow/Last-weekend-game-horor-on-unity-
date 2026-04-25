using UnityEngine;

public struct KeysCodes
{
	public KeyCode
		Forward,
		Back,
		Left,
		Right,
		Interaction ,
		OpenInventory,
		Stels,
		Sprint,
		Jump;

	public Vector2 SentityMause;
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
		Default.Interaction = KeyCode.E;
		Default.OpenInventory = KeyCode.F;
		Default.Stels = KeyCode.LeftShift;
		Default.Sprint = KeyCode.LeftAlt;
		Default.Jump = KeyCode.Space;

		Default.SentityMause = Vector2.one;

		Current = Default;
	}
}
