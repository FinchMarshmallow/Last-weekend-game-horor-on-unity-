using System;
using UnityEngine;

public class HandPoint : MonoBehaviour
{
	[SerializeField] private Transform head, hand;
	[SerializeField] private float radiysCast, distanceCust;
	[SerializeField] private LayerMask layer;


	private Quaternion _oldQuaternion;
	private RaycastHit _hit;

	public Action<bool> ActionContact;
	public DataInteractEnvironment DataEnvironment;
	public Entity Entity;
	public Vector3 Pos => hand.position;
	public Transform Content => hand;
	public RaycastHit Hit => _hit;
	public bool IsContact;
	public Vector3 CastPointNoContact => head.position + (head.forward * distanceCust);

#if UNITY_EDITOR
	[SerializeField] private Color gizmo;
#endif

	private void Update()
	{
		if (_oldQuaternion == head.rotation)
			return;

		_oldQuaternion = head.rotation;

		Ray ray = new(head.position, head.forward);


		if (Physics.SphereCast(ray, radiysCast, out _hit, distanceCust, layer, QueryTriggerInteraction.Ignore))
		{
			ActionContact?.Invoke(true);
			IsContact = true;
			hand.position = _hit.point;

			if (_hit.transform.TryGetComponent(out Entity) &&
				Entity.TryGetDataByType(out DataEnvironment))
			{
				DataEnvironment.Action.Invoke(DataEnvironment.IdActionSellect);
			}
		}
		else if (IsContact)
		{
			ActionContact?.Invoke(false);
			IsContact = false;

			if (Entity != null &&
				DataEnvironment != null)
			{
				DataEnvironment.Action.Invoke(DataEnvironment.IdActionDeSellect);
				Entity = null;
				DataEnvironment = null;
			}
		}

		if (!IsContact)
		{
			hand.position = (head.position + (head.forward * distanceCust));
		}
	}


#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if (head == null)
			return;

		Gizmos.color = gizmo;
		Vector3
			originCast = head.position,
			pointCast;

		if (!IsContact)
		{
			pointCast = originCast + head.forward * distanceCust;
		}
		else
		{
			pointCast = _hit.point;
		}

		Gizmos.DrawSphere(pointCast, radiysCast);
		Gizmos.DrawLine(pointCast, head.position);
	}
#endif
}
