using UnityEngine;

public class MonsterContext : IContext
{
	public MonsterAnimHandler animHandler; // Animation
	public bool deadth; // 몬스터 죽었는지 확인용
	public PlayerDetection playerDetection;
	public Rigidbody rigidbody;
	public MonsterStat stat; // 스탯
	public Transform transform; // 자신
}
