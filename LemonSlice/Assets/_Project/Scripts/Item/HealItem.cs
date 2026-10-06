using UnityEngine;

public class HealItem : MonoBehaviour, IInteractable, IPoolable
{
	[SerializeField] private GameObject healEffectPrefab;
	public LayerMask targetLayer;
	public GameObject GameObject => gameObject;
	private HealingPack healingPack;
	private PlayerStat playerStat;

	private void OnTriggerEnter(Collider other)
	{
		int layer = (1 << other.gameObject.layer);
		if ((targetLayer.value & layer) != 0)
		{
			Interact(other.gameObject);
		}
	}

	public void Interact(GameObject interactor)
	{
		int layer = (1 << interactor.layer);
		if ((targetLayer.value & layer) != 0)
		{
			playerStat = GetComponent<PlayerStat>();
			if (playerStat != null)
			{
				healingPack.Heal(playerStat);
				if (healEffectPrefab != null)
				{
					Vector3 spawnPosition = interactor.transform.position;
					PoolManager.Instance.Take(healEffectPrefab).SetPosition(spawnPosition).Build();
				}
			}
			PoolManager.Instance.TryReturn(gameObject);
		}
	}

	public PoolType PoolId => PoolType.HealPotion;
}
