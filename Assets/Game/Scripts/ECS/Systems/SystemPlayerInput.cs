using System;
using System.Collections.Generic;
using UnityEngine;
using UpdatesIntarfaces;

[Serializable]
public class SystemPlayerInput : BaseSystem, IUpdate, ILateUpdate
{
	private Dictionary<Entity, bool> _squatStates = new();

	public List<Entity> _entities;
	public List<DataMoveInput> _inputs;
	public List<DataMotorMove> _motorMoves;
	public List<DataMotorRotate> _motorRotate;
	public List<DataPester> _pesters;

	private Filter<DataMoveInput, DataMotorMove, DataMotorRotate, DataPester> _filter;

	private float _timePressDropKey = 0f, _forceDrop;

	public override void Init()
	{
		_filter = new(out _entities, out _inputs, out _motorMoves, out _motorRotate, out _pesters, TagEntity.Player);
		World.AddFilter(_filter);
	}

	public void Update()
	{
		if (PlayerInputsData.IsPause)
			return;

		Move();
		InputInteract();
	}

	public void LateUpdate()
	{
		if (PlayerInputsData.IsPause || !PlayerInputsData.IsRotationHead)
			return;

		HandRotate();
	}

	private void Move()
	{
		PlayerInputsData.RotationHead = Input.mousePositionDelta * ConfigKeyCode.Current.SentityMause;

		for (int i = 0; i < _entities.Count; i++)
		{
			Entity entity = _entities[i];
			DataMoveInput input = _inputs[i];
			DataMotorMove motorMove = _motorMoves[i];

			Vector3 move = Vector3.zero;
			if (Input.GetKey(ConfigKeyCode.Current.Forward)) move.z++;
			if (Input.GetKey(ConfigKeyCode.Current.Back)) move.z--;
			if (Input.GetKey(ConfigKeyCode.Current.Right)) move.x++;
			if (Input.GetKey(ConfigKeyCode.Current.Left)) move.x--;

			bool hasMoveInput = move.sqrMagnitude > 0f;
			if (hasMoveInput)
				move.Normalize();

			input.Direct = move;

			bool stelsDown = Input.GetKeyDown(ConfigKeyCode.Current.Stels);
			bool stelsUp = Input.GetKeyUp(ConfigKeyCode.Current.Stels);

			if (!_squatStates.ContainsKey(entity))
				_squatStates[entity] = false;

			if (stelsDown) _squatStates[entity] = true;
			else if (stelsUp) _squatStates[entity] = false;

			bool squatActive = _squatStates[entity];

			bool sprintHeld = Input.GetKey(ConfigKeyCode.Current.Sprint);

			if (!hasMoveInput)
				motorMove.TypeMove = TypeMotorMove.None;
			else if (squatActive)
				motorMove.TypeMove = TypeMotorMove.Squat;
			else if (sprintHeld)
				motorMove.TypeMove = TypeMotorMove.Sprint;
			else
				motorMove.TypeMove = TypeMotorMove.Default;
		}
	}

	private void HandRotate()
	{
		for (int i = 0; i < _motorRotate.Count; i++)
		{
			float xMouseMove = Input.mousePositionDelta.x * ConfigKeyCode.Current.SentityMause.x * Time.deltaTime * 10f;
			_motorRotate[i].TargetRotate *= Quaternion.AngleAxis(xMouseMove, Vector3.up);
		}
	}

	private void InputInteract()
	{
		_forceDrop = 0f;
		PesterCommand command = PesterCommand.None;

		if (Input.GetKey(ConfigKeyCode.Current.InteractOrTake)) command = PesterCommand.TakeInteract;
		if (Input.GetKey(ConfigKeyCode.Current.Inspect)) command = PesterCommand.InspectSwitchState;

		if (Input.GetKey(ConfigKeyCode.Current.Drop)) _timePressDropKey += Time.deltaTime;
		else if (Input.GetKeyUp(ConfigKeyCode.Current.Drop))
		{
			if(_timePressDropKey < ConfigKeyCode.Current.StartPressTimeDropForce)
			{
				command = PesterCommand.Drop;
			}
			else
			{
				command = PesterCommand.DropForce;
				_forceDrop = Mathf.Clamp01(_timePressDropKey / ConfigKeyCode.Current.MaxPressTimeDropForce);
			}
		}

		for (int i = 0; i < _entities.Count; i++)
		{
			_pesters[i].Command = command;
			_pesters[i].DropForce = _forceDrop;
		}
	}
}
