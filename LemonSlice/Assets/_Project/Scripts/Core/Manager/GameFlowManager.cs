using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : SingletonBehaviour<GameFlowManager>
{
	[SerializeField] private GameObject spawnPoint;
	[SerializeField] private GameObject spawnPoint2;
	[SerializeField] private GameObject itemBoxPrefab;
	[SerializeField] private List<Transform> itemBoxList;

	public bool IsPlayerDead { get; set; }
	public bool IsBossDead { get; set; }

	private void Start()
	{
		InitStage();
	}

	private void Update()
	{
		CheckStageState();
	}

	private void CheckStageState()
	{
		if (IsBossDead)
		{
			GameManager.Instance.ChangeState(GameState.StageClear);
		} else if (IsPlayerDead)
		{
			GameManager.Instance.ChangeState(GameState.GameOver);
		}
	}

	private void InitStage()
	{
		IsPlayerDead = false;
		IsBossDead = false;
		spawnPoint.SetActive(false);
		spawnPoint2.SetActive(false);
		Spawn(itemBoxList, itemBoxPrefab);
	}

	public void EnterArea(List<Transform> spawnList, GameObject prefab)
	{
		Spawn(spawnList, prefab);
	}

	private void Spawn(List<Transform> spawnList, GameObject prefab)
	{
		foreach (Transform spawn in spawnList)
		{
			PoolManager.Instance
				.Take(prefab)
				.SetPosition(spawn.position)
				.SetRotation(spawn.rotation)
				.Build();
		}
	}
}
