using UnityEngine;

public class ProviderPesterItem : BaseProviders, IPester
{
	[SerializeField] private Transform headPoint;
	[SerializeField] private float radiusCust, distanceCust;
	[SerializeField] private LayerMask layer;

	public IInteractable curentIInteractable;

	public Entity Entity => entity;
	private Quaternion _oldQuaternion;

	private void Update()
	{
		if (_oldQuaternion == headPoint.rotation)
			return;

		_oldQuaternion = headPoint.rotation;

		Ray ray = new(headPoint.position, headPoint.forward);

		if(Physics.SphereCast(ray, radiusCust, out RaycastHit hit, distanceCust, layer, QueryTriggerInteraction.Ignore) &&
			hit.collider.TryGetComponent(out IInteractable interactable))
		{
			if (curentIInteractable == interactable)
				return;

			if (curentIInteractable != null)
				curentIInteractable.DeSelect(this);

			curentIInteractable = interactable;
			curentIInteractable.Select(this);
		}
		else if (curentIInteractable != null)
		{
			curentIInteractable.DeSelect(this);
			curentIInteractable = null;
		}
	}
}
