using UnityEngine;

public class ProviderPesterItem : BaseProviders, IPester, IDataPesterProvider
{
	[SerializeField] private Transform headPoint;
	[SerializeField] private float radiusCast, distanceCust;
	[SerializeField] private LayerMask layer;

	public IInteractable CurentInteractable { get; private set; }
	public RaycastHit CurentHit { get; private set; }

	public Entity Entity => entity;
	private Quaternion _oldQuaternion;

#if UNITY_EDITOR
	[SerializeField] private Color gizmo;
#endif

	private void Update()
	{
		if (_oldQuaternion == headPoint.rotation)
			return;

		_oldQuaternion = headPoint.rotation;

		Ray ray = new(headPoint.position, headPoint.forward);

		if(Physics.SphereCast(ray, radiusCast, out RaycastHit hit, distanceCust, layer, QueryTriggerInteraction.Ignore) &&
			hit.collider.TryGetComponent(out IInteractable interactable))
		{
			if (CurentInteractable == interactable)
				return;

			if (CurentInteractable != null)
				CurentInteractable.DeSelect(this);

			CurentInteractable = interactable;
			CurentInteractable.Select(this);
			CurentHit = hit;
		}
		else if (CurentInteractable != null)
		{
			CurentInteractable.DeSelect(this);
			CurentInteractable = null;
		}
	}


#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.color = gizmo;
		Vector3
			originCast = headPoint.position,
			pointCast;

		if (CurentInteractable == null)
		{
			pointCast = originCast + headPoint.forward * distanceCust;
		}
		else
		{
			pointCast = CurentHit.point;
		}

		Gizmos.DrawSphere(pointCast, radiusCast);
		Gizmos.DrawLine(pointCast, headPoint.position);
	}
#endif

}
