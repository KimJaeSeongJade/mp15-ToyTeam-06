using System.Collections;
using UnityEngine;

public class EffectController : MonoBehaviour, IPoolable
{
	[SerializeField] private float _deactivateDelay;


	public PoolType PoolId => PoolType.HealEffect;

	private void OnEnable()
	{
		StartCoroutine(ReturnToPool());
	}

	private IEnumerator ReturnToPool()
	{
		yield return new WaitForSeconds(_deactivateDelay);

		PoolManager.Instance.TryReturn(gameObject);
	}
}
