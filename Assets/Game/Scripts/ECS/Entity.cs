using System;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
	public TagEntity Tag;

	[NonSerialized] public Rigidbody2D Rb;
	[SerializeReference, SubclassSelector] public List<BaseData> Datas;

	private void Awake()
	{
		InitDatas();
		TryGetComponent(out Rb);
	}

	private void Start()
	{
		World.AddEntity(this);
	}

	private void InitDatas()
	{
		for (int i = 0; i < Datas.Count; i++)
		{
			Datas[i].Init(this);
		}
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
