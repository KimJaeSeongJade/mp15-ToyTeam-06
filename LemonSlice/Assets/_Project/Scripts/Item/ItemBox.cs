using UnityEngine;

public class ItemBox : MonoBehaviour, IInteractable, IPoolable
{
	[SerializeField] private HealItem _healItem;

	public LayerMask InteractedLayer;
	public GameObject GameObject => this.gameObject;


	private void Update()
	{
	}

	public void Interact(GameObject interactor)
	{
		if (!(interactor.layer == LayerMask.NameToLayer("Weapon")))
		{
			return;
		}

		Destroy(gameObject);
		SpawnHealPotion();
	}


	private void SpawnHealPotion()
	{
		Instantiate(_healItem, transform.position, Quaternion.identity);
	}

	public PoolType PoolId => PoolType.ItemBox;
}
