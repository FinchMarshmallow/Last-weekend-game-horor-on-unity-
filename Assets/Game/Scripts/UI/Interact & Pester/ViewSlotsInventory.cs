using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

	private DataPesterInventory _inventory;

	private void Start()
	{
		StartCoroutine(LongStart());
	}

	private IEnumerator LongStart()
	{
		yield return new WaitForSeconds(0.1f);

		entity.TryGetDataByType(out _inventory);

		if (_inventory.Items.Length != slots.Count)
			Debug.LogError($"ViewSlotsInventory: Start: inventory.Items.Length ({_inventory.Items.Length}) != slots.Count ({slots.Count})");



		for (int i = 0; i < slots.Count; i++)
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
		for(int i = 0; i < _inventory.Items.Length; i++)
		{
			if (_inventory.Items[i] == null)
			{
				slots[i].Img.enabled = false;
				slots[i].Name.text = "";
			}
			else
			{
				slots[i].Img.sprite = _inventory.Items[i].SlotImg;
				slots[i].Img.enabled = true;
				slots[i].Name.text = _inventory.Items[i].SlotName;
			}
		}
	}
}
