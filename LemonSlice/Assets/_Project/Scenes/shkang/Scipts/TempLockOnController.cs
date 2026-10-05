using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLockOnController : MonoBehaviour
{
	private List<Transform> enemyLists = new();

	private int lockOnIndex = 0;

	public bool CheckCanLockOn()
	{
		return enemyLists.Count > 0;
	}

	public void AddEnemy(GameObject enemy)
	{
		for (int i = 0; i < enemyLists.Count; i++)
		{
			if (enemy == enemyLists[i].gameObject) return;
		}

		enemyLists.Add(enemy.transform);
	}

	public void RemoveEnemy(GameObject enemy)
	{
		for (int i = 0; i < enemyLists.Count; i++)
		{
			if (enemy == enemyLists[i].gameObject)
			{
				enemyLists.RemoveAt(i);
				return;
			}
		}
	}

	public Transform TempLockOn()
	{
		return enemyLists[lockOnIndex];
	}

	public void TempChangeLockOn()
	{
		lockOnIndex++;
		if (lockOnIndex >= enemyLists.Count)
		{
			lockOnIndex = 0;
		}
	}

}
