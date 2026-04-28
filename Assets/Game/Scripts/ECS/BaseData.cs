using System;
using UnityEngine;

[Serializable]
public abstract class BaseData
{
	public abstract BaseData Copy();
	public virtual void Init(Entity entity) { }
}
