using System.Collections.Generic;
using UnityEngine;

public class MutantContext : IContext
{
	public MutantAnimationHandler animHandler;
	public MonsterDetection monsterDetection;
	public BossStat stat;
	public Transform transform;
	public List<AttackHitBox> hitBoxes;
	
	public int attackIndex;
	public bool isInAttackRange;
}
