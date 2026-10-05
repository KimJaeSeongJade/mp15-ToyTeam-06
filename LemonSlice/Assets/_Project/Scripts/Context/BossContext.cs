using UnityEngine;

public class BossContext : IContext
{
	public BossAnimationHandler animHandler;
	public int attackIndex;
	public MonsterDetection monsterDetection;
	public BossStat stat;
	public Transform transform;
}
