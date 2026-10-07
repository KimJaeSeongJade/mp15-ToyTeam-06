using UnityEngine;

public struct DamageInfo
{
	public int Damage { get; }
	public int DownValue { get; }
	public Vector3 HitPoint { get; }
	public Vector3 HitDirection { get; }
	public Vector3 KnockDownDirection { get; }

	public DamageInfo(int damage, int downValue, Vector3 hitPoint, Vector3 hitDirection, Vector3 knockDownDirection)
	{
		Damage = damage;
		DownValue = downValue;
		HitPoint = hitPoint;
		HitDirection = hitDirection;
		KnockDownDirection = knockDownDirection;
	}
}
