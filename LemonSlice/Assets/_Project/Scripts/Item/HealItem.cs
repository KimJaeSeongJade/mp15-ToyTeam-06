using UnityEngine;

public class HealItem : MonoBehaviour, IInteractable, IPoolable
{
	public LayerMask targetLayer;
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

	public GameObject GameObject => gameObject;


	public void Interact(GameObject interactor)
	{
		int layer = (1 << interactor.layer);
		if ((targetLayer.value & layer) != 0)
		{
			playerStat = GetComponent<PlayerStat>();
			if (playerStat != null)
			{
				healingPack.Heal(playerStat);
			}
			PoolManager.Instance.TryReturn(gameObject);
		}
	}

	public PoolType PoolId => PoolType.HealPotion;
}
