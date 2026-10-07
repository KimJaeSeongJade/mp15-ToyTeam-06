using UnityEngine;

public class MonsterContext : IContext
{
	public MonsterAnimHandler animHandler; // Animation
	public MonsterDetection monsterDetection;
	public Rigidbody rigidbody;
	public MonsterStat stat; // 스탯
	public Transform transform; // 자신
	public GameObject coin;
	public AttackHitBox leftHitBox;
	public AttackHitBox rightHitBox;
	public bool death; // 몬스터 죽었는지 확인용
	public bool isUnderAttack;

	public Vector3 hitPoint;
	public Vector3 hitDirection;
	public Vector3 knockDownDirection;
}
