using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProviderInteract : BaseProviders
{
	public string Name, Description;
	public Sprite AimImg, DescriptionImg;
	public UnityEvent<ProviderPester> EventSelect, EventDeSelect;
	public ProviderPester LastPester;

	public void Select(ProviderPester provider)
	{
		EventSelect?.Invoke(provider);
		LastPester = provider;
	}

	public void DeSelect(ProviderPester provider)
	{
		EventDeSelect?.Invoke(provider);
		LastPester = provider;
	}
}
