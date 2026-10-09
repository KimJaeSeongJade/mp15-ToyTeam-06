using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHitEffect : MonoBehaviour, IPoolable
{
	public PoolType PoolId => PoolType.MonsterHitEffect;
}
