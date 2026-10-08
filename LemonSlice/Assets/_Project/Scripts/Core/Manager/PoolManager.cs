using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonBehaviour<PoolManager>
{
	[SerializeField] private GameObject monsterPrefab;
	[SerializeField] private int monsterPoolSize;

	[SerializeField] private GameObject newMonsterPrefab;
	[SerializeField] private int newMonsterPoolSize;

	[SerializeField] private GameObject coinPrefab;
	[SerializeField] private int coinPoolSize;

	[SerializeField] private GameObject itemBoxPrefab;
	[SerializeField] private int itemBoxPoolSize;

	[SerializeField] private GameObject healPotionPrefab;
	[SerializeField] private int healPotionPoolSize;

	[SerializeField] private GameObject slashEffectPrefab;
	[SerializeField] private int slashEffectPoolSize;

	[SerializeField] private GameObject healEffectPrefab;
	[SerializeField] private int effectPoolSize;

	[SerializeField] private GameObject bloodEffectPrefab;
	[SerializeField] private int bloodEffectPoolSize;

	[SerializeField] private GameObject playerAttack1Clip;
	[SerializeField] private int playerAttack1PoolSize;

	[SerializeField] private GameObject playerAttack2Clip;
	[SerializeField] private int playerAttack2PoolSize;

	[SerializeField] private GameObject playerAttack3Clip;
	[SerializeField] private int playerAttack3PoolSize;

	private Dictionary<PoolType, Stack<GameObject>> _dict;

	private Stack<GameObject> _monsterPool;
	private Stack<GameObject> _newMonsterPool;
	private Stack<GameObject> _coinPool;
	private Stack<GameObject> _itemBoxPool;
	private Stack<GameObject> _healPotionPool;
	private Stack<GameObject> _slashEffectPool;
	private Stack<GameObject> _healEffectPool;
	private Stack<GameObject> _bloodEffectPool;
	private Stack<GameObject> _playerAttack1ClipPool;
	private Stack<GameObject> _playerAttack2ClipPool;
	private Stack<GameObject> _playerAttack3ClipPool;

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
		_newMonsterPool = GetPool(newMonsterPrefab, newMonsterPoolSize);
		_coinPool = GetPool(coinPrefab, coinPoolSize);
		_itemBoxPool = GetPool(itemBoxPrefab, itemBoxPoolSize);
		_healPotionPool = GetPool(healPotionPrefab, healPotionPoolSize);
		_healEffectPool = GetPool(healEffectPrefab, effectPoolSize);
		_slashEffectPool = GetPool(slashEffectPrefab, slashEffectPoolSize);
		_bloodEffectPool = GetPool(bloodEffectPrefab, bloodEffectPoolSize);
		_playerAttack1ClipPool = GetPool(playerAttack1Clip, playerAttack1PoolSize);
		_playerAttack2ClipPool = GetPool(playerAttack2Clip, playerAttack2PoolSize);
		_playerAttack2ClipPool = GetPool(playerAttack3Clip, playerAttack3PoolSize);


		_dict.Add(PoolType.Monster, _monsterPool);
		_dict.Add(PoolType.NewMonster, _newMonsterPool);
		_dict.Add(PoolType.Coin, _coinPool);
		_dict.Add(PoolType.ItemBox, _itemBoxPool);
		_dict.Add(PoolType.HealPotion, _healPotionPool);
		_dict.Add(PoolType.HealEffect, _healEffectPool);
		_dict.Add(PoolType.SlashEffect, _slashEffectPool);
		_dict.Add(PoolType.BloodEffect, _bloodEffectPool);
		_dict.Add(PoolType.PlayerAttack1, _playerAttack1ClipPool);
		_dict.Add(PoolType.PlayerAttack2, _playerAttack2ClipPool);
		_dict.Add(PoolType.PlayerAttack3, _playerAttack3ClipPool);

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
