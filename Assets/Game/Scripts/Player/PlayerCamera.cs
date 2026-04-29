using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	[SerializeField] private Transform headPoint;

	private float _targetVertical = 0f;

	public static Quaternion CharecterRot { get; private set; }

	private void Awake()
	{
		PlayerInputsData.IsRotationHead = true;
	}

	private void LateUpdate()
	{
		if (!PlayerInputsData.IsRotationHead || PlayerInputsData.IsPause)
			return;

		_targetVertical -= PlayerInputsData.RotationHead.y;
		_targetVertical = Mathf.Clamp(_targetVertical, -89.9f, 89.9f);

		Quaternion headRotation = Quaternion.Euler(_targetVertical, 0f, 0f);

		headPoint.localRotation = headRotation;
	}
}