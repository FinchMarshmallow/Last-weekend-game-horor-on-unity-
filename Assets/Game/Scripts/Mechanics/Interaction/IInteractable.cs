using UnityEngine;

public interface IInteractable
{
	public string Name { get; }
	public string Description { get; }
	public void Select(IOpener opener);
	public void DeSelect(IOpener opener);
	public void Interaction(IOpener opener);
}
