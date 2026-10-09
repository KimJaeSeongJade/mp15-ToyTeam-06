using System.Collections.Generic;
using UnityEngine;

public class MutantContext : IContext
{
	public MutantAnimationHandler animHandler;
	public MonsterDetection monsterDetection;
	public MutantStat stat;
	public Transform transform;
	public AttackHitBox hitBoxes;
	public bool isInAttackRange;
	public int attackIndex;

}
