using System;
using System.Collections.Generic;
using UnityEngine;



public class ActionProviderManager : MonoBehaviour
{
	[Serializable]
	private struct KeyValueData
	{
		public string key;
		public ActionProvider data;
	}

	[SerializeField] private List<KeyValueData> actionProviders;

	public ActionProvider GetByName(string name)
	{
		KeyValueData buffer = new KeyValueData();
		buffer = actionProviders.Find(x => x.key == name);
		return buffer.data;
	}
}