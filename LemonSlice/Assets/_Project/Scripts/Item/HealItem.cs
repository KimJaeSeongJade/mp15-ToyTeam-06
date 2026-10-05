using System;
using UnityEngine;

public class HealItem : MonoBehaviour, IInteractable, IPoolable
{
	private HealingPack healingPack;
	public LayerMask targetLayer;

	public GameObject GameObject => gameObject;

	private void Awake()
	{
		healingPack = GetComponent<HealingPack>();
	}

	public void Interact(GameObject interactor)
	{
		int layer = (1 << interactor.layer);
		if ((targetLayer.value & layer) != 0)
		{
			// healingPack.Heal();
			PoolManager.Instance.TryReturn(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		int layer = (1 << other.gameObject.layer);
		if ((targetLayer.value & layer) != 0)
		{
			Interact(other.gameObject);
		}
	}
	public PoolType PoolId => PoolType.HealPotion;
}
