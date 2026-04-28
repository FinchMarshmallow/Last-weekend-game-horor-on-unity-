using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	[SerializeField] private Transform headPoint;

	[SerializeField] private Vector3 _planarDirect = Vector3.forward;

	private float _targetVertical = 0f;

	public static Quaternion CharecterRot { get; private set; }

	private void LateUpdate()
	{
		if (!PlayerInputsData.IsRotationHead || PlayerInputsData.IsPause)
			return;

		Vector3 currentUp = Vector3.up;
		_planarDirect = Quaternion.AngleAxis(PlayerInputsData.RotationHead.x, currentUp) * _planarDirect;
		_planarDirect.Normalize();

		_targetVertical -= PlayerInputsData.RotationHead.y;
		_targetVertical = Mathf.Clamp(_targetVertical, -89.9f, 89.9f);

		Quaternion bodyRotation = Quaternion.LookRotation(_planarDirect, currentUp);

		Quaternion headRotation = bodyRotation * Quaternion.Euler(_targetVertical, 0f, 0f);

		headPoint.rotation = headRotation;
		CharecterRot = bodyRotation;
	}
}