using UnityEngine;

public class HandPoint : MonoBehaviour
{
	[SerializeField] private Transform head, hand;
	[SerializeField] private float radiysCast, distanceCust;
	[SerializeField] private LayerMask layer;

	private Vector3 _startHandPos;
	private Quaternion _oldQuaternion;
	private RaycastHit _hit;

	public Vector3 Pos => hand.position;
	public Transform Content => hand;
	public RaycastHit Hit => _hit;
	public bool IsContact { get; private set; }
	public Vector3 CastPointNoContact => head.position + (head.forward * distanceCust);

#if UNITY_EDITOR
	[SerializeField] private Color gizmo;
#endif

	private void Awake()
	{
		_startHandPos = hand.localPosition;
	}

	private void Update()
	{
		if (_oldQuaternion == head.rotation)
			return;

		_oldQuaternion = head.rotation;

		Ray ray = new(head.position + _startHandPos, hand.forward);

		float distance = distanceCust;

		if (Physics.SphereCast(ray, radiysCast, out _hit, distanceCust, layer, QueryTriggerInteraction.Ignore))
		{
			distance = _hit.distance;
			IsContact = true;
		}
		else
		{
			IsContact = false;
		}

		hand.localPosition = _startHandPos + (head.forward * distance);
	}


#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if (head == null)
			return;

		Gizmos.color = gizmo;
		Vector3
			originCast = head.position + _startHandPos,
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
		Gizmos.DrawLine(pointCast, head.position + _startHandPos);
	}
#endif
}
