using UnityEngine;

public struct DamageInfo
{
	public int Damage { get; }
	public int DownValue { get; }
	public Vector3 HitPoint { get; }
	public Vector3 HitDirection { get; }

	public DamageInfo(int damage, int downValue, Vector3 hitPoint, Vector3 hitDirection)
	{
		Damage = damage;
		DownValue = downValue;
		HitPoint = hitPoint;
		HitDirection = hitDirection;
	}
}
