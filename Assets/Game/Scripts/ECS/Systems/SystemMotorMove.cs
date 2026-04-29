using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SystemMotorMove : BaseSystem
{
	private Dictionary<Entity, Func<Vector3, float, Vector3>> _handlers = new();

	public List<Entity> _entities;
	public List<DataMotor> _motors;
	public List<DataMoveInput> _inputs;
	public List<DataMotorMove> _moves;

	private Filter<DataMotor, DataMoveInput, DataMotorMove> _filter;

	public override void Init()
	{
		_filter = new(out _entities, out _motors, out _inputs, out _moves, TagEntity.All);

		_filter.HandlerAddEntity += AddHandlerEntity;
		_filter.HandlerBeforeRemoveEntity += RemoveHandlerEntity;

		World.AddFilter(_filter);
	}

	private void AddHandlerEntity(Entity entity)
	{
		DataMotor motorData = _motors[_motors.Count - 1];
		DataMotorMove motorMove = _moves[_moves.Count - 1];
		DataMoveInput input = _inputs[_moves.Count - 1];

		Func<Vector3, float, Vector3> handler = (vel, time) =>
		{
			return UpdateVelosity(vel, motorData, motorMove, input);
		};

		_handlers[entity] = handler;
		_motors[_motors.Count - 1].HandlerUpdateVelocity += handler;
	}

	private void RemoveHandlerEntity(Entity entity)
	{
		if (entity.TryGetDataByID<DataMotor>(out var motor) &&
			_handlers.TryGetValue(entity, out var handler))
		{
			motor.HandlerUpdateVelocity -= handler;
			_handlers.Remove(entity);
		}
	}

	private Vector3 UpdateVelosity(Vector3 currentVelocity, DataMotor data, DataMotorMove move, DataMoveInput input)
	{
		bool isOnGround = data.Motor.GroundingStatus.IsStableOnGround;
		float deltaTime = Time.deltaTime;

		Vector3
			targetVelocity = Vector3.zero,
			worldDirect = Vector3.zero,
			worldInput = Vector3.zero;


		if (move.TypeMove != TypeMotorMove.None)
		{
			worldInput =
				data.Motor.CharacterForward * input.Direct.z +
				data.Motor.CharacterRight * input.Direct.x;
		}
		
		if (isOnGround)
		{
			worldDirect = data.Motor.GetDirectionTangentToSurface(worldInput, data.Motor.GroundingStatus.GroundNormal).normalized;
		}
		else
		{
			worldDirect = worldInput;
		}

		switch (move.TypeMove)
		{
			case TypeMotorMove.None:
				break;

			case TypeMotorMove.Default:
				targetVelocity = worldDirect * move.SpeedMove;
				break;

			case TypeMotorMove.Sprint:
				targetVelocity = worldDirect * move.SpeedMove * move.MultySpeedSprint;
				break;

			case TypeMotorMove.Squat:
				targetVelocity = worldDirect * move.SpeedMove * move.MultySpeedSquat;
				break;

			default:
				break;
		}

		currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, 1f - Mathf.Exp(-move.StoppingStable * deltaTime));

		if (!isOnGround)
		{
			currentVelocity += Vector3.up * move.Gravity * deltaTime;
		}

		return currentVelocity;
	}
}
