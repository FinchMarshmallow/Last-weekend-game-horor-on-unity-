using KinematicCharacterController;
using UnityEngine;

public class CharecterController : MonoBehaviour, ICharacterController
{
	[SerializeField] private KinematicCharacterMotor motor;

	[SerializeField] private float speedMove, stable, gravity, multySpeedDrop;

	private void Start()
	{
		motor.CharacterController = this;
	}

	public void AfterCharacterUpdate(float deltaTime)
	{

	}

	public void BeforeCharacterUpdate(float deltaTime)
	{
	}

	public bool IsColliderValidForCollisions(Collider coll)
	{
		return true;
	}

	public void OnDiscreteCollisionDetected(Collider hitCollider)
	{
	}

	public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
	{
	}

	public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
	{
	}

	public void PostGroundingUpdate(float deltaTime)
	{
	}

	public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
	{
	}

	public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
	{
		currentRotation = PlayerCamera.CharecterRot;
	}
	public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
	{
		float moveMultiplier = 1f;

		if (!motor.GroundingStatus.IsStableOnGround)
		{
			currentVelocity += (Vector3.up * gravity * deltaTime);
			moveMultiplier = multySpeedDrop;
		}

		Vector3 moveInput = PlayerInputsData.MoveInput;


		Vector3 worldInput = 
			motor.CharacterForward * moveInput.z +
			motor.CharacterRight * moveInput.x;

		Vector3 targetDirection;
		if (motor.GroundingStatus.IsStableOnGround)
		{
			targetDirection = motor.GetDirectionTangentToSurface(worldInput,  motor.GroundingStatus.GroundNormal).normalized;
		}
		else
		{
			targetDirection = worldInput.normalized;
		}

		Vector3 targetVelocity = targetDirection * speedMove * moveMultiplier;

		currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity,1f - Mathf.Exp(-stable * deltaTime));
	}
}