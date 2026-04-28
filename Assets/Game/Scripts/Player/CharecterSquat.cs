using KinematicCharacterController;
using System.Text.RegularExpressions;
using UnityEngine;

public class CharecterSquat : MonoBehaviour
{
	[SerializeField] private Transform eyesPoint, meshRoot;
	[SerializeField] private float heightSquat;
	[SerializeField] private Vector3 eyesOffest, rootSquatOffest, scaleRootSquat;

	private Vector3 _startEyesPos, _scaleRootDefault, _startRootPos;
	private KinematicCharacterMotor _motor;
	private float _heightDefault;
	private CharecterController _controller;

	private int _command, _state = 1;

	private void Awake()
	{
		TryGetComponent(out _motor);
		TryGetComponent(out _controller);
		_startEyesPos = eyesPoint.position;
		_heightDefault = _motor.Capsule.height;
		_scaleRootDefault = meshRoot.localScale;
		_startRootPos = meshRoot.localPosition;
	}

	private void StartSquat()
	{
		_motor.Capsule.height = heightSquat;
		_motor.Capsule.center = new Vector3(0f, heightSquat / 2f, 0f);
		eyesPoint.localPosition += eyesOffest;
		meshRoot.localPosition += rootSquatOffest;
		meshRoot.localScale = scaleRootSquat;
		_controller.IsSquat = true;
		_state = -1;
	}

	private void StopSquat()
	{
		_motor.Capsule.height = _heightDefault;
		_motor.Capsule.center = new Vector3(0f, _heightDefault / 2f, 0f);
		eyesPoint.localPosition = _startEyesPos;
		meshRoot.localPosition = _startRootPos;
		meshRoot.localScale = _scaleRootDefault;
		_controller.IsSquat = false;
		_state = 1;
	}

	private void Update()
	{
		if (Input.GetKeyDown(ConfigKeyCode.Current.Stels)) _command = -1;
		else if (Input.GetKeyUp(ConfigKeyCode.Current.Stels)) _command = 1;

		if (PlayerInputsData.IsPause)
			return;

		if (_command == -1 && _state == 1)
			StartSquat();

		if(_command ==  1 && _state == -1)
			StopSquat();
	}
}
