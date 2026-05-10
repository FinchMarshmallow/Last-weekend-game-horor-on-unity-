using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class ActionProviderManager : MonoBehaviour
{
	private List<ActionProvider> _actionProviders;

	[SerializeField] private GameObject objActionProviders;

	private void Awake()
	{
		_actionProviders = objActionProviders.GetComponents<ActionProvider>().ToList();
	}

	public ActionProvider GetByName(string name)
	{
		ActionProvider buffer = null;
		buffer = _actionProviders.Find(x => x.Name == name);
		return buffer;
	}
}