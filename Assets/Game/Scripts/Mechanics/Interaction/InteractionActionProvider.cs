using UnityEngine;
using UnityEngine.Events;

public class InteractionActionProvider : MonoBehaviour, IInteractable
{
	[SerializeField] private string nameInteraction, description;
	[SerializeField] private UnityEvent<IPester> actionInteraction;
	[SerializeField] private GameObject selectObject;

	public string Name => nameInteraction;
	public string Description => description;

	public void DeSelect(IPester pester)
	{
		selectObject.SetActive(false);
	}

	public void Interaction(IPester pester)
	{
		actionInteraction?.Invoke(pester);
	}

	public void Select(IPester pester)
	{
		selectObject.SetActive(true);
	}
}
