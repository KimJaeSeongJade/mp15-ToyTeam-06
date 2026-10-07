using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonBehaviour<PoolManager>
{
	[SerializeField] private GameObject monsterPrefab;
	[SerializeField] private int monsterPoolSize;

	[SerializeField] private GameObject coinPrefab;
	[SerializeField] private int coinPoolSize;

	[SerializeField] private GameObject itemBoxPrefab;
	[SerializeField] private int itemBoxPoolSize;

	[SerializeField] private GameObject healPotionPrefab;
	[SerializeField] private int healPotionPoolSize;

	[SerializeField] private GameObject slashEffectPrefab;
	[SerializeField] private int slashEffectPoolSize;

	private Dictionary<PoolType, Stack<GameObject>> _dict;

	private Stack<GameObject> _monsterPool;
	private Stack<GameObject> _coinPool;
	private Stack<GameObject> _itemBoxPool;
	private Stack<GameObject> _healPotionPool;
	private Stack<GameObject> _slashEffectPool;

	private void Awake()
	{
		InitPool();
	}

	public GameObjectBuilder Take(GameObject gameObject)
	{
		PoolType poolId = gameObject.GetComponent<IPoolable>().PoolId;
		if (!_dict.ContainsKey(poolId))
		{
			return null;
		}
		gameObject.SetActive(true);
		return new GameObjectBuilder(_dict[poolId].Pop());
	}

	public bool TryReturn(GameObject gameObject)
	{
		PoolType poolId = gameObject.GetComponent<IPoolable>().PoolId;
		if (!_dict.ContainsKey(poolId))
		{
			return false;
		}

		gameObject.SetActive(false);
		_dict[poolId].Push(gameObject);
		return true;
	}

	private void InitPool()
	{
		_dict = new();

		_monsterPool = GetPool(monsterPrefab, monsterPoolSize);
		_coinPool = GetPool(coinPrefab, coinPoolSize);
		_itemBoxPool = GetPool(itemBoxPrefab, itemBoxPoolSize);
		_healPotionPool = GetPool(healPotionPrefab, healPotionPoolSize);
		_slashEffectPool = GetPool(slashEffectPrefab, slashEffectPoolSize);

		_dict.Add(PoolType.Monster, _monsterPool);
		_dict.Add(PoolType.Coin, _coinPool);
		_dict.Add(PoolType.ItemBox, _itemBoxPool);
		_dict.Add(PoolType.HealPotion, _healPotionPool);
		_dict.Add(PoolType.SlashEffect, _slashEffectPool);
	}

	public Stack<GameObject> GetPool(GameObject go, int poolSize)
	{
		Stack<GameObject> pool = new();
		for (int i = 0; i < poolSize; i++)
		{
			GameObject gameObject = Instantiate(go);
			gameObject.SetActive(false);
			pool.Push(gameObject);
		}

		return pool;
	}
}
