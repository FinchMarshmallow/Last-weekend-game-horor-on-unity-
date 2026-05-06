using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionProvider : MonoBehaviour
{
	[SerializeField] private List<UnityEvent<int>> actions;

	public void Invoke(int id, int argument = 0)
	{
		if(id < 0 || id >= actions.Count)
		{
#if UNITY_EDITOR
			Debug.Log($"ActionProvider: Invoke: id overate: id {id}, argument: {argument}");
#endif
			return;
		}
		actions[id]?.Invoke(argument);
	}
}