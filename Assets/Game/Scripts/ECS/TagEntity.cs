using UnityEngine;
using System;

[Serializable, Flags]
public enum TagEntity
{
	None = 0,

	Player = 1,
	Enemy = 1 << 1,

	[HideInInspector] All = Player | Enemy,
}