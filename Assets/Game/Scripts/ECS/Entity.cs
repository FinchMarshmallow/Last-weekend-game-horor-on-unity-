using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
	public TagEntity Tag;

	[HideInInspector] public Rigidbody2D Rb;
	[SerializeReference, SubclassSelector] public List<BaseData> Datas;

	private void Start()
	{
		TryGetComponent(out Rb);
		World.AddEntity(this);
	}

	public bool TryGetDataByID<T>(out T data) where T : BaseData
	{
		for (int i = 0; i < Datas.Count; i++)
		{
			if (Datas[i] is T t)
			{
				data = t;
				return true;
			}
		}

		data = null;
		return false;
	}

	public List<BaseData> CopyAllData()
	{
		List<BaseData> copy = new(Datas.Count);

		for (int i = 0; i < Datas.Count; i++)
		{
			copy.Add(Datas[i]);
		}

		return copy;
	}

	public Entity Copy()
	{
		Entity copy = Instantiate(this);
		copy.Tag = Tag;
		copy.Datas = CopyAllData();
		return copy;
	}
}
