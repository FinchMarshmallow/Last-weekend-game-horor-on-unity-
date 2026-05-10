using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct SlotBuffer
{
	public Image Img;
	public TextMeshProUGUI Num, Name;
}

public class ViewSlotsInventory : MonoBehaviour
{
	[SerializeField] private Entity entity;
	[SerializeField] private List<SlotBuffer> slots = new();

	[SerializeField] private GameObject addSlot;

	private DataPesterInventory inventory;

	private void Start()
	{
		entity.TryGetDataByType(out inventory);

		if (inventory.Items.Length != slots.Count)
			Debug.LogError($"ViewSlotsInventory: Start: inventory.Items.Length ({inventory.Items.Length}) != slots.Count ({slots.Count})");

		for (int i = 0;i < slots.Count; i++)
		{
			slots[i].Img.enabled = false;
			slots[i].Num.text = $"{i}";
			slots[i].Name.text = "";
		}
	}

	private void OnValidate()
	{
		if (addSlot == null)
			return;

		SlotBuffer slot = new();

		TextMeshProUGUI[] texts = addSlot.GetComponentsInChildren<TextMeshProUGUI>();

		slot.Img = addSlot.GetComponent<Image>();
		slot.Num = texts[0];
		slot.Name = texts[1];

		slots.Add(slot);

		addSlot = null;
	}

	public void UpdateSlots()
	{
		for(int i = 0; i < inventory.Items.Length; i++)
		{
			if (inventory.Items[i] == null)
			{
				slots[i].Img.enabled = false;
				slots[i].Name.text = "";
			}
			else
			{
				slots[i].Img.sprite = inventory.Items[i].SlotImg;
				slots[i].Img.enabled = true;
				slots[i].Name.text = inventory.Items[i].SlotName;
			}
		}
	}
}
