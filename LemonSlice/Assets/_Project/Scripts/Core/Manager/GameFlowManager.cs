using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
	[SerializeField] private GameObject spawnPoint;
	[SerializeField] private GameObject itemBoxPrefab;
	[SerializeField] private List<Transform> itemBoxList;
	private bool _isStageClear;

	private void Start()
	{
		InitStage();
	}

	private void Update()
	{
		CheckStageClear();
	}

	private void CheckStageClear()
	{
		if (_isStageClear)
		{
			GameManager.Instance.ChangeState(GameState.StageClear);
		}
	}

	private void InitStage()
	{
		spawnPoint.SetActive(false);
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
