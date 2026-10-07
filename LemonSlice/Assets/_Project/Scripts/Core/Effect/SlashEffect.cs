using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour, IPoolable
{
	private Coroutine slashEffectRoutine;
	private WaitForSeconds slashEffectWait;
	
	public PoolType PoolId =>  PoolType.SlashEffect;

	private void OnEnable()
	{
		StartCoroutine(SlashEffectRoutine());
	}

	private IEnumerator SlashEffectRoutine()
	{
		slashEffectWait = new WaitForSeconds(1f);
		yield return slashEffectWait;
		PoolManager.Instance.TryReturn(gameObject);
	}
}
