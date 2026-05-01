using UnityEngine;

public interface IDataPesterProvider
{
	public IInteractable CurentInteractable { get; }
	public RaycastHit CurentHit { get; }
}