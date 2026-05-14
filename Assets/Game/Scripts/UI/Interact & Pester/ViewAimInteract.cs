using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewAimInteract : MonoBehaviour
{
	[SerializeField] private Entity entity;
	[SerializeField] private Image aimImg;
	[SerializeField] private HandPoint handPoint;

	private DataPesterHand _hand;

	[SerializeField] private TextMeshProUGUI description;

	private void Awake()
	{
		handPoint.ActionContact += UpdateUI;

		description.enabled = false;
		aimImg.enabled = false;
	}

	private void UpdateUI(bool isContact)
	{
		if (isContact &&
			handPoint.Hit.transform.TryGetComponent(out Entity entity) &&
			entity.TryGetDataByType(out DataInteract data))
		{
			description.text = data.DescriptionOnAim;
			aimImg.sprite = data.SpriteOnAim;

			description.enabled = true;
			aimImg.enabled = true;
		}
		else
		{
			description.enabled = false;
			aimImg.enabled = false;
		}
	}
}