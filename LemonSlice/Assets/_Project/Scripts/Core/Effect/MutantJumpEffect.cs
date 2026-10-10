using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantJumpEffect : MonoBehaviour, IPoolable
{
	private Coroutine jumpEffectRoutine;
	private WaitForSeconds jumpEffectWait;

	public PoolType PoolId => PoolType.MutantJumpEffect;

	private void OnEnable()
	{
		StartCoroutine(SlashEffectRoutine());
	}

	private IEnumerator SlashEffectRoutine()
	{
		jumpEffectWait = new WaitForSeconds(1f);
		yield return jumpEffectWait;
		PoolManager.Instance.TryReturn(gameObject);
	}
}
