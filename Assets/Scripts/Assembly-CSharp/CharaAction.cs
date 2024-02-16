using System;
using UnityEngine;

[Serializable]
public class CharaAction
{
	public string AnimationName;

	public Vector3 TargetPoint;

	public Transform TargetObject;

	public int Speed;

	public bool GravityCheck;
}
