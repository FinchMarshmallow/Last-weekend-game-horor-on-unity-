using System;
using System.Collections.Generic;
using UnityEngine;

public class SystemMotorMove : BaseSystem
{
	public List<Entity> _entities;
	public List<DataMotor> _motors;
	public List<DataMotorMove> _moves;

	private Filter<DataMotor, DataMotorMove> _filter;
	public event Func<Vector3, float, Vector3> HandlerUpdateVelocity;

	public override void Init()
	{
		_filter = new(out _entities, out _motors, out _moves, TagEntity.All);

		_filter.HandlerAddEntity += AddHandlerEntity;
		_filter.HandlerBeforeRemoveEntity += RemoveHandlerEntity;

		World.AddFilter(_filter);
	}

	private void AddHandlerEntity(Entity entity)
	{
		_motors[_motors.Count - 1].HandlerUpdateVelocity += UpdateVelosity;
	}

	private void RemoveHandlerEntity(Entity entity)
	{
		if (entity.TryGetDataByID<DataMotor>(out var motor))
			motor.HandlerUpdateVelocity -= UpdateVelosity;
	}

	private Vector3 UpdateVelosity(Vector3 currentVel, float time)
	{
		return Vector3.zero; // временно
	}
}
