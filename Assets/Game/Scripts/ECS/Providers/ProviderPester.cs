using System;
using System.Collections.Generic;
using UnityEngine;

public class ProviderPester : BaseProviders
{
	[SerializeField] private Transform headPoint;
	[SerializeField] private float radiusCast, distanceCust;
	[SerializeField] private LayerMask layer;

	public InteractByffer CurrentSelectObject { get; private set; }

	public RaycastHit CurentHit { get; private set; }

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

		if (Physics.SphereCast(ray, radiusCast, out RaycastHit hit, distanceCust, layer, QueryTriggerInteraction.Ignore) &&
			hit.collider.TryGetComponent(out Entity entity))
		{
			InteractByffer b = CurrentSelectObject;

			if (CurrentSelectObject.Entity == entity || 
				!entity.TryGetComponent(out b.Provider))
				return;

			if (CurrentSelectObject.Entity != null)
				CurrentSelectObject.Provider.DeSelect(this);


			b.Entity = entity;
			entity.TryGetDataByType(out b.Data);
			b.State = b.Data.State;
			b.Entity = entity;
			b.Provider = b.Data.Provider;

			CurrentSelectObject.Provider.Select(this);
			CurentHit = hit;

			CurrentSelectObject = b;
		}
		else if (CurrentSelectObject.Entity != null)
		{
			CurrentSelectObject.Provider.DeSelect(this);
			InteractByffer b = CurrentSelectObject;

			b.Entity = null;
			b.Provider = null;
			b.Data = null;
			b.State &= 0;

			CurrentSelectObject = b;
		}
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.color = gizmo;
		Vector3
			originCast = headPoint.position,
			pointCast;

		if (CurrentSelectObject.Entity == null)
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