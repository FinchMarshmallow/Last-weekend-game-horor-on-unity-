using System;
using System.Collections.Generic;
using UnityEngine;
using UpdatesIntarfaces;

[Serializable]
public class SystemPlayerInput : BaseSystem, IUpdate
{
	private Dictionary<Entity, bool> _squatStates = new();

	public List<Entity> _entities;
	public List<DataMoveInput> _inputs;
	public List<DataMotorMove> _motorMoves;

	private Filter<DataMoveInput, DataMotorMove> _filter;

	public override void Init()
	{
		_filter = new(out _entities, out _inputs, out _motorMoves, TagEntity.Player);
		World.AddFilter(_filter);
	}

	public void Update()
	{
		if (PlayerInputsData.IsPause)
			return;

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
}