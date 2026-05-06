using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class Filter
{
	public virtual void AddEntity(Entity entity) { }
	public virtual void RemoveEntity(Entity entity) { }

	public virtual event Action<Entity> HandlerAddEntity;
	public virtual event Action<Entity> HandlerBeforeRemoveEntity;

	protected void OnAddEntity(Entity entity) => HandlerAddEntity?.Invoke(entity);
	protected void BeforeOnRemoveEntity(Entity entity) => HandlerBeforeRemoveEntity?.Invoke(entity);
}

public class Filter<A> : Filter
	where A : BaseData
{
	private List<Entity> _entities;
	private List<A> _a;
	private TagEntity _tagFilter;

	public Filter(out List<Entity> entities, out List<A> a, TagEntity tagFilter)
	{
		_entities = entities = new List<Entity>();
		_a = a = new List<A>();
		_tagFilter = tagFilter;
		World.AddFilter(this);
	}

	public override void AddEntity(Entity entity)
	{
		if ((entity.Tag & _tagFilter) == 0)
			return;

		A a = null;

		if (entity.TryGetDataByType(out a))
		{
			_entities.Add(entity);
			_a.Add(a);
		}

		OnAddEntity(entity);
	}

	public override void RemoveEntity(Entity entity)
	{
		if ((entity.Tag & _tagFilter) == 0)
			return;

		int removeI = _entities.FindIndex(a => a.GetInstanceID() == entity.GetInstanceID());

		if (removeI == -1)
			return;

		BeforeOnRemoveEntity(entity);
		_entities.RemoveAt(removeI);
		_a.RemoveAt(removeI);

	}
}

public class Filter<A, B> : Filter
	where A : BaseData
	where B : BaseData
{
	private List<Entity> _entities;
	private List<A> _a;
	private List<B> _b;
	private TagEntity _tagFilter;

	public Filter(out List<Entity> entities, out List<A> a, out List<B> b, TagEntity tagFilter)
	{
		_entities = entities = new List<Entity>();
		_a = a = new List<A>();
		_b = b = new List<B>();
		_tagFilter = tagFilter;
		World.AddFilter(this);
	}

	public override void AddEntity(Entity entity)
	{
		if ((entity.Tag & _tagFilter) == 0)
			return;

		A a = null;
		B b = null;

		if (entity.TryGetDataByType(out a) &&
			entity.TryGetDataByType(out b))
		{
			_entities.Add(entity);
			_a.Add(a);
			_b.Add(b);
		}

		OnAddEntity(entity);
	}

	public override void RemoveEntity(Entity entity)
	{
		if ((entity.Tag & _tagFilter) == 0)
			return;

		int removeI = _entities.FindIndex(a => a.GetInstanceID() == entity.GetInstanceID());

		if (removeI == -1)
			return;

		BeforeOnRemoveEntity(entity);

		_entities.RemoveAt(removeI);
		_a.RemoveAt(removeI);
		_b.RemoveAt(removeI);
	}
}

public class Filter<A, B, C> : Filter
	where A : BaseData 
	where B : BaseData
	where C : BaseData
{
	private List<Entity> _entities;
	private List<A> _a;
	private List<B> _b;
	private List<C> _c;
	private TagEntity _tagFilter;

	public Filter(out List<Entity> entities, out List<A> a, out List<B> b, out List<C> c, TagEntity tagFilter)
	{
		_entities = entities = new List<Entity>();
		_a = a = new List<A>();
		_b = b = new List<B>();
		_c = c = new List<C>();
		_tagFilter = tagFilter;
		World.AddFilter(this);
	}

	public override void AddEntity(Entity entity)
	{
		if ((entity.Tag & _tagFilter) == 0)
			return;

		A a = null;
		B b = null;
		C c = null;

		if (entity.TryGetDataByType(out a) &&
			entity.TryGetDataByType(out b) &&
			entity.TryGetDataByType(out c))
		{
			_entities.Add(entity);
			_a.Add(a);
			_b.Add(b);
			_c.Add(c);
		}
		//Debug.Log($"add {entity.name}, tag: {_tagFilter}, F: {typeof(A).Name}, {typeof(B).Name}, {typeof(C).Name}");
		OnAddEntity(entity);
	}

	public override void RemoveEntity(Entity entity)
	{
		if ((entity.Tag & _tagFilter) == 0)
			return;

		int removeI = _entities.FindIndex(a => a.GetInstanceID() == entity.GetInstanceID());

		if (removeI == -1)
			return;

		BeforeOnRemoveEntity(entity);

		_entities.RemoveAt(removeI);
		_a.RemoveAt(removeI);
		_b.RemoveAt(removeI);
		_c.RemoveAt(removeI);
	}
}

public class Filter<A, B, C, D> : Filter
	where A : BaseData
	where B : BaseData
	where C : BaseData
	where D : BaseData
{
	private List<Entity> _entities;
	private List<A> _a;
	private List<B> _b;
	private List<C> _c;
	private List<D> _d;
	private TagEntity _tagFilter;

	public Filter(out List<Entity> entities, out List<A> a, out List<B> b, out List<C> c, out List<D> d, TagEntity tagFilter)
	{
		_entities = entities = new List<Entity>();
		_a = a = new List<A>();
		_b = b = new List<B>();
		_c = c = new List<C>();
		_d = d = new List<D>();
		_tagFilter = tagFilter;
		World.AddFilter(this);
	}

	public override void AddEntity(Entity entity)
	{
		if ((entity.Tag & _tagFilter) == 0)
			return;

		A a = null;
		B b = null;
		C c = null;
		D d = null;

		if (entity.TryGetDataByType(out a) &&
			entity.TryGetDataByType(out b) &&
			entity.TryGetDataByType(out c) &&
			entity.TryGetDataByType(out d))
		{
			_entities.Add(entity);
			_a.Add(a);
			_b.Add(b);
			_c.Add(c);
			_d.Add(d);
		}

		OnAddEntity(entity);

	}

	public override void RemoveEntity(Entity entity)
	{
		if ((entity.Tag & _tagFilter) == 0)
			return;

		int removeI = _entities.FindIndex(a => a.GetInstanceID() == entity.GetInstanceID());

		if (removeI == -1)
			return;

		BeforeOnRemoveEntity(entity);

		_entities.RemoveAt(removeI);
		_a.RemoveAt(removeI);
		_b.RemoveAt(removeI);
		_c.RemoveAt(removeI);
		_d.RemoveAt(removeI);
	}
}