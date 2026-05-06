using UnityEngine;

public abstract class BaseProviders : MonoBehaviour
{
	public Entity Entity { get; private set; }

	private void Awake()
	{
		if (Entity == null)
			Entity = GetComponent<Entity>();
	}
}
