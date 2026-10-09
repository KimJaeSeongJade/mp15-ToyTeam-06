using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour, IPoolable
{
	public PoolType PoolId => PoolType.PlayerHitEffect;
}
