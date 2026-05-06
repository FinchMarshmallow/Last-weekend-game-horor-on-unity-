using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class DataInteractWithItemInHand : BaseData
{
	[SerializeField] private string nameActionProvider;
	[NonSerialized] public ActionProvider Action;
	public List<Sprite> NotificationImgs;
	public Sprite CurrentImg;
	public int
		IdActionDefaultStart,
		IdActionDefaultEnd,
		IdActionAdditionalStart,
		IdActionAdditionalEnd;

	public override BaseData Copy()
	{
		DataInteractWithItemInHand copy = new();

		copy.CurrentImg = CurrentImg;
		copy.nameActionProvider = nameActionProvider;
		copy.IdActionDefaultStart = IdActionDefaultStart;
		copy.IdActionDefaultEnd = IdActionDefaultEnd;
		copy.IdActionAdditionalStart = IdActionAdditionalStart;
		copy.IdActionAdditionalEnd = IdActionAdditionalEnd;

		return copy;
	}

	public override void Init(Entity entity)
	{
		entity.TryGetComponent(out ActionProviderManager manager);
		Action = manager.GetByName(nameActionProvider);
	}
}