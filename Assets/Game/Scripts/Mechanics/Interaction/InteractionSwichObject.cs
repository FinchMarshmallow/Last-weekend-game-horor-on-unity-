using UnityEngine;

public class InteractionSwichObject : MonoBehaviour, IInteractable
{
	[SerializeField] private string nameInteraction, description;

	[SerializeField] private GameObject selectObject, InteractionObject;

	public string Name => nameInteraction;
	public string Description => description;

	public void DeSelect(IPester opener)
	{

	}

	public void Interaction(IPester opener)
	{
		throw new System.NotImplementedException();
	}

	public void Select(IPester opener)
	{
		throw new System.NotImplementedException();
	}
}
