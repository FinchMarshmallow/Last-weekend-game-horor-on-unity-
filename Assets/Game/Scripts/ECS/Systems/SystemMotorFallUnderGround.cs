using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SystemMotorFallUnderGround : BaseSystem
{
	private Dictionary<Entity, Func<Collider, bool>> _handlers = new();

	public List<Entity> _entities;
	public List<DataMotor> _motors;
	public List<DataMotorFallUnderGround> _fallUnderGrounds;

	private Filter<DataMotor, DataMotorFallUnderGround> _filter;

	public override void Init()
	{
		_filter = new(out _entities, out _motors, out _fallUnderGrounds, TagEntity.All);

		_filter.HandlerAddEntity += AddHandlerEntity;
		_filter.HandlerBeforeRemoveEntity += RemoveHandlerEntity;

		World.AddFilter(_filter);
	}

	private void AddHandlerEntity(Entity entity)
	{
		DataMotorFallUnderGround fallData = _fallUnderGrounds[_fallUnderGrounds.Count - 1];

		Func<Collider, bool> handler = (collider) =>
		{
			int layerBit = (int)Math.Pow(2, collider.gameObject.layer);
			//Debug.Log($"layerBit: {layerBit}, gameObject: {(collider.gameObject.layer)}, fallData: {fallData.MaskImpassable.value}, &: {collider.gameObject.layer & fallData.MaskImpassable} bit & {layerBit & fallData.MaskImpassable}");
			return (layerBit & fallData.MaskImpassable) != 0;
		};

		_handlers[entity] = handler;
		_motors[_motors.Count - 1].HandlerColliderCollisions += handler;
	}

	private void RemoveHandlerEntity(Entity entity)
	{
		if (entity.TryGetDataByType<DataMotor>(out var motor) &&
			_handlers.TryGetValue(entity, out var handler))
		{
			motor.HandlerColliderCollisions -= handler;
			_handlers.Remove(entity);
		}
	}
}
