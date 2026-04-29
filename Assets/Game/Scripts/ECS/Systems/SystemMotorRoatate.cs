using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SystemMotorRoatate : BaseSystem
{
	private Dictionary<Entity, Func<Quaternion, float, Quaternion>> _handlers = new();

	public List<Entity> _entities;
	public List<DataMotor> _motors;
	public List<DataMotorRotate> _rotates;

	public Filter<DataMotorRotate, DataMotor> _filter;

	public override void Init()
	{
		_filter = new(out _entities, out _rotates, out _motors, TagEntity.All);

		_filter.HandlerAddEntity += AddHandlerEntity;
		_filter.HandlerBeforeRemoveEntity += RemoveHandlerEntity;

		World.AddFilter(_filter);
	}

	private void AddHandlerEntity(Entity entity)
	{
		DataMotor motorData = _motors[_motors.Count - 1];
		DataMotorRotate roatateData = _rotates[_rotates.Count - 1];

		Func<Quaternion, float, Quaternion> handler = (rot, t) =>
		{
			return HandlerUpdateRotation(rot, roatateData);
		};

		_handlers[entity] = handler;
		_motors[_motors.Count - 1].HandlerUpdateRotation += handler;
	}

	private void RemoveHandlerEntity(Entity entity)
	{
		if (entity.TryGetDataByID<DataMotor>(out var motor) &&
			_handlers.TryGetValue(entity, out var handler))
		{
			motor.HandlerUpdateRotation -= handler;
			_handlers.Remove(entity);
		}
	}

	private Quaternion HandlerUpdateRotation(Quaternion currentRotation, DataMotorRotate roatateData)
	{
		return roatateData.TargetRotate;
	}
}