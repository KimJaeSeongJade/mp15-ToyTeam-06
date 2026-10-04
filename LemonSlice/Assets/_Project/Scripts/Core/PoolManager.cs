using System;
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

	private Dictionary<Type, Stack<GameObject>> _dict;

	private Stack<GameObject> _monsterPool;
	private Stack<GameObject> _coinPool;
	private Stack<GameObject> _itemBoxPool;
	private Stack<GameObject> _healPotionPool;

	private void Awake()
	{
		InitPool();
	}

	public GameObjectBuilder Take(Component component)
	{
		Type type = component.GetType();
		if (!_dict.ContainsKey(type))
		{
			return null;
		}

		return new GameObjectBuilder(_dict[type].Pop());
	}

	public bool TryReturn(GameObject go)
	{
		Type type = go.GetType();
		if (!_dict.ContainsKey(type))
		{
			return false;
		}

		go.SetActive(false);
		_dict[type].Push(go);
		return true;
	}

	private void InitPool()
	{
		_dict = new();

		_monsterPool = GetPool(monsterPrefab, monsterPoolSize);
		_coinPool = GetPool(coinPrefab, coinPoolSize);
		_itemBoxPool = GetPool(itemBoxPrefab, itemBoxPoolSize);
		_healPotionPool = GetPool(healPotionPrefab, healPotionPoolSize);

		_dict.Add(monsterPrefab.GetType(), _monsterPool);
		_dict.Add(coinPrefab.GetType(), _coinPool);
		_dict.Add(itemBoxPrefab.GetType(), _itemBoxPool);
		_dict.Add(healPotionPrefab.GetType(), _healPotionPool);
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
