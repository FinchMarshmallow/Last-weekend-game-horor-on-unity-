using KinematicCharacterController;
using UnityEngine;

public class CharecterController : MonoBehaviour, ICharacterController
{
	private KinematicCharacterMotor _motor;

	[SerializeField] private float speedMove, stable, gravity, multySpeedDrop, multySpeedSquat, multySpeedSprint;

	public bool IsSquat;

	private void Start()
	{
		TryGetComponent(out _motor);
		_motor.CharacterController = this;
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

		if (!_motor.GroundingStatus.IsStableOnGround)
		{
			currentVelocity += (Vector3.up * gravity * deltaTime / multySpeedDrop);
			moveMultiplier = multySpeedDrop;
		}

		Vector3 moveInput = PlayerInputsData.MoveInput;


		Vector3 worldInput = 
			_motor.CharacterForward * moveInput.z +
			_motor.CharacterRight * moveInput.x;

		Vector3 targetDirection;
		if (_motor.GroundingStatus.IsStableOnGround)
		{
			targetDirection = _motor.GetDirectionTangentToSurface(worldInput,  _motor.GroundingStatus.GroundNormal).normalized;
		}
		else
		{
			targetDirection = worldInput.normalized;
		}

		if (IsSquat) moveMultiplier *= multySpeedSquat;
		else if(PlayerInputsData.IsSprint) moveMultiplier *= multySpeedSprint;

		Vector3 targetVelocity = targetDirection * speedMove * moveMultiplier;

		currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity,1f - Mathf.Exp(-stable * deltaTime));
	}
}