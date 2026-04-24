using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
	private void Awake()
	{
		PlayerInputsData.IsRotationHead = true;
	}

	private void Update()
	{
		PlayerInputsData.RotationHead = Input.mousePositionDelta * ConfigKeyCode.SentityMause;

		Vector3 move = Vector3.zero;

		if (Input.GetKey(ConfigKeyCode.Forward)) move.z++;
		if (Input.GetKey(ConfigKeyCode.Back)) move.z--;
		if (Input.GetKey(ConfigKeyCode.Right)) move.x++;
		if (Input.GetKey(ConfigKeyCode.Left)) move.x--;

		PlayerInputsData.IsMoveInput = move.sqrMagnitude > 0;

		if (PlayerInputsData.IsMoveInput)
			move = move.normalized;

		PlayerInputsData.MoveInput = move;
	}
}