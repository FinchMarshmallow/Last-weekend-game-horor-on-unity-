using System.Collections.Generic;
using UnityEngine;

public class DataCharecterInventory : BaseData
{
	public List<DataItem> Items = new();
	public DataItem IntemInHand;

	public override BaseData Copy()
	{
		DataCharecterInventory copy = new();

		return copy;
	}
}
