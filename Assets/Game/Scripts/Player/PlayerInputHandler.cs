using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
	private void Awake()
	{
		PlayerInputsData.IsRotationHead = true;
	}

	private void Update()
	{
		PlayerInputsData.RotationHead = Input.mousePositionDelta * ConfigKeyCode.Current.SentityMause;

		Vector3 move = Vector3.zero;

		if (Input.GetKey(ConfigKeyCode.Current.Forward)) move.z++;
		if (Input.GetKey(ConfigKeyCode.Current.Back)) move.z--;
		if (Input.GetKey(ConfigKeyCode.Current.Right)) move.x++;
		if (Input.GetKey(ConfigKeyCode.Current.Left)) move.x--;

		PlayerInputsData.IsMoveInput = move.sqrMagnitude > 0;

		if (PlayerInputsData.IsMoveInput)
			move = move.normalized;

		PlayerInputsData.MoveInput = move;
	}
}