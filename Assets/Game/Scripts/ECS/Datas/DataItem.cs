using UnityEngine;

public class DataItem : BaseData
{
	public string Name, Description;
	public Sprite InventiryImg, FaviconImg;
	public GameObject Object;

	public override BaseData Copy()
	{
		DataItem copy = new();

		copy.Name = Name;
		copy.Description = Description;
		copy.InventiryImg = InventiryImg;
		copy.FaviconImg = FaviconImg;
		copy.Object = MonoBehaviour.Instantiate(Object);

		return copy;
	}
}
