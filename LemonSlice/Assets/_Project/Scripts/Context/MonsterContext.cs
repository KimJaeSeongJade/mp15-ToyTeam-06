using UnityEngine;

public class MonsterContext : IContext
{
	public Transform targetTransform; // 타겟
	public MonsterAnimationHandler animHandler; // Animation
	public bool deadth; // 몬스터 죽었는지 확인용
	public bool isPlayerEnter; // Player가 들어왔나?
	public MonsterStat stat; // 스탯
	public Transform transform; // 자신
}
