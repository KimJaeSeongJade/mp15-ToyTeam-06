using UnityEngine;

public class ItemBox : MonoBehaviour, IInteractable, IPoolable,IDamageable
{
	[SerializeField] private GameObject _healItem;
	[SerializeField] private int _health;
	public GameObject GameObject => gameObject;


	private void Update()
	{
	}

	public void Interact(GameObject interactor)
	{
		Destroy(gameObject);
		// 이팩트 효과 추가
		SpawnHealPotion();
	}


	private void SpawnHealPotion()
	{
		PoolManager.Instance.Take(_healItem.gameObject).SetPosition(gameObject.transform.position).Build();
	}


	public PoolType PoolId => PoolType.ItemBox;
	public void TakeDamage(DamageInfo damageInfo)
	{
		_health -= damageInfo.Damage;

		if (_health <= 0)
		{
			Interact(GameObject);
		}
	}
}
