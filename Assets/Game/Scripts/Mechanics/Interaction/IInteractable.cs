using UnityEngine;

public interface IInteractable
{
	public string Name { get; }
	public string Description { get; }
	public void Select(IPester pester);
	public void DeSelect(IPester pester);
	public void Interaction(IPester pester);
}
