using KinematicCharacterController;
using System;
using UnityEngine;

public class DataMotor : BaseData, ICharacterController
{
	[NonSerialized] public KinematicCharacterMotor _motor;

	public event Func<Quaternion, float, Quaternion> HandlerUpdateRotation;
	public event Func<Vector3, float, Vector3> HandlerUpdateVelocity;
	public event Func<Collider, bool> HandlerColliderCollisions;

	public override BaseData Copy()
	{
		return new DataMotor();
	}

	public override void Init(Entity entity)
	{
		entity.TryGetComponent(out _motor);
		_motor.CharacterController = this;
	}

	// This is called when the motor wants to know what its rotation should be right now
	public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
	{
		Quaternion? buffer = HandlerUpdateRotation?.Invoke(currentRotation, deltaTime);

		if (buffer != null) currentRotation = buffer.Value;
	}

	// This is called when the motor wants to know what its velocity should be right now
	public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
	{
		Vector3? buffer = HandlerUpdateVelocity?.Invoke(currentVelocity, deltaTime);

		if (buffer != null) currentVelocity = buffer.Value;
	}

	// This is called before the motor does anything
	public void BeforeCharacterUpdate(float deltaTime) { }

	// This is called after the motor has finished its ground probing, but before PhysicsMover/Velocity/etc.... handling
	public void PostGroundingUpdate(float deltaTime) { }

	// This is called after the motor has finished everything in its update
	public void AfterCharacterUpdate(float deltaTime) { }

	// This is called after when the motor wants to know if the collider can be collided with (or if we just go through it)
	public bool IsColliderValidForCollisions(Collider coll)
	{
		bool? buffer = HandlerColliderCollisions?.Invoke(coll);
		
		if (buffer == null) return false;

		return buffer.Value;
	}

	// This is called when the motor's ground probing detects a ground hit
	public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport) { }

	// This is called when the motor's movement logic detects a hit
	public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport) { }

	// This is called after every move hit, to give you an opportunity to modify the HitStabilityReport to your liking
	public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport) { }
	
	// This is called when the character detects discrete collisions (collisions that don't result from the motor's capsuleCasts when moving)
	public void OnDiscreteCollisionDetected(Collider hitCollider) { }
}
