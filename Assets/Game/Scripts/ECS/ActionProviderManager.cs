using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionProviderManager : MonoBehaviour
{
	private List<ActionProvider> _actionProviders;

	[SerializeField] private GameObject objActionProviders;

	public ActionProvider GetByName(string name)
	{
		if(_actionProviders == null)
			_actionProviders = objActionProviders.GetComponents<ActionProvider>().ToList();


		ActionProvider buffer = null;
		buffer = _actionProviders.Find(x => x.Name == name);
		return buffer;
	}
}