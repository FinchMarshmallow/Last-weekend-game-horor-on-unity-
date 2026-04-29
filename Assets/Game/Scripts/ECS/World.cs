using System.Collections.Generic;
using UnityEngine;
using UpdatesIntarfaces;

public class World : MonoBehaviour
{
	[SerializeField] private List<Entity> entities = new();
	public static List<Entity> Entities => _init.entities;

	[SerializeReference, SubclassSelector] private List<BaseSystem> systems = new();

	private List<IUpdate> _updates = new();
	private List<ILateUpdate> _lateUpdates = new();
	private List<IFixedUpdate> _fixedUpdates = new();

	private List<Filter> _filters = new();

	private static World _init;

	private void OnEnable()
	{
		_init = this;
	}

	private void OnDisable()
	{
		_init = null;
	}

	private void Awake()
	{
		_init = this;
		InitSystems();
	}

	private void InitSystems()
	{
		for(int i = 0; i < systems.Count; i++)
		{
#region log
#if UNITY_EDITOR
			Debug.Log($"World: InitSystems: i: {i}, system: {systems[i].GetType().Name}");
#endif
#endregion
			systems[i].Init();

			if (systems[i] is IUpdate u) _updates.Add(u);
			if (systems[i] is ILateUpdate lu) _lateUpdates.Add(lu);
			if (systems[i] is IFixedUpdate fu) _fixedUpdates.Add(fu);
		}
	}

	public static void AddEntity(Entity entity)
	{
		if (Entities.Contains(entity))
			return;

		Entities.Add(entity);

		for (int i = 0; i < _init._filters.Count; i++)
		{
			_init._filters[i].AddEntity(entity);
		}
	}

	public static void RemoveEntity(Entity entity)
	{
		if (Entities.Contains(entity))
			return;

		Entities.Remove(entity);

		for(int i = 0; i < _init._filters.Count; i++)
		{
			_init._filters[i].RemoveEntity(entity);
		}
	}

	public static void AddFilter(Filter filter)
	{
		_init._filters.Add(filter);

		for(int i = 0; i < Entities.Count; i++)
		{
			filter.AddEntity(Entities[i]);
		}
	}


	private void Update()
	{
		for(int i = 0; i < _updates.Count; i++)
		{
			_updates[i].Update();
		}
	}

	private void LateUpdate()
	{
		for (int i = 0; i < _lateUpdates.Count; i++)
		{
			_lateUpdates[i].LateUpdate();
		}
	}

	private void FixedUpdate()
	{
		for (int i = 0; i < _fixedUpdates.Count; i++)
		{
			_fixedUpdates[i].FixedUpdate();
		}
	}
}
