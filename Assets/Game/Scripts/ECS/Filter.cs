using System.Collections.Generic;

public abstract class Filter
{
	public virtual void AddEntity(Entity entity) { }
	public virtual void RemoveEntity(Entity entity) { }
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

		if (entity.TryGetDataByID(out a))
		{
			_entities.Add(entity);
			_a.Add(a);
		}
	}

	public override void RemoveEntity(Entity entity)
	{
		if ((entity.Tag & _tagFilter) == 0)
			return;

		int removeI = _entities.FindIndex(a => a.GetInstanceID() == entity.GetInstanceID());

		if (removeI != -1)
			return;

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

		if (entity.TryGetDataByID(out a) &&
			entity.TryGetDataByID(out b))
		{
			_entities.Add(entity);
			_a.Add(a);
			_b.Add(b);
		}
	}

	public override void RemoveEntity(Entity entity)
	{
		if ((entity.Tag & _tagFilter) == 0)
			return;

		int removeI = _entities.FindIndex(a => a.GetInstanceID() == entity.GetInstanceID());

		if (removeI != -1)
			return;

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

		if (entity.TryGetDataByID(out a) &&
			entity.TryGetDataByID(out b) &&
			entity.TryGetDataByID(out c))
		{
			_entities.Add(entity);
			_a.Add(a);
			_b.Add(b);
			_c.Add(c);
		}
	}

	public override void RemoveEntity(Entity entity)
	{
		if ((entity.Tag & _tagFilter) == 0)
			return;

		int removeI = _entities.FindIndex(a => a.GetInstanceID() == entity.GetInstanceID());

		if (removeI != -1)
			return;

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

		if (entity.TryGetDataByID(out a) &&
			entity.TryGetDataByID(out b) &&
			entity.TryGetDataByID(out c) &&
			entity.TryGetDataByID(out d))
		{
			_entities.Add(entity);
			_a.Add(a);
			_b.Add(b);
			_c.Add(c);
			_d.Add(d);
		}
	}

	public override void RemoveEntity(Entity entity)
	{
		if ((entity.Tag & _tagFilter) == 0)
			return;

		int removeI = _entities.FindIndex(a => a.GetInstanceID() == entity.GetInstanceID());

		if (removeI != -1)
			return;

		_entities.RemoveAt(removeI);
		_a.RemoveAt(removeI);
		_b.RemoveAt(removeI);
		_c.RemoveAt(removeI);
		_d.RemoveAt(removeI);
	}
}