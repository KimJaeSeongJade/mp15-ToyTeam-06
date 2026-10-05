using System.Collections.Generic;
using UnityEngine;

public class LockOnController : MonoBehaviour
{
	private List<Transform> enemyLists = new();

	private Transform currentTarget;

	// ---------------------

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
				break;
			}
		}

		if (currentTarget != null && enemy == currentTarget.gameObject)
		{
			currentTarget = null;
		}
	}

	public bool TryLockOn()
	{

		if (enemyLists.Count == 0) return false;

		currentTarget = enemyLists[0];
		return true;
	}

	public void ClearLockOn()
	{
		currentTarget = null;
	}

	public bool HasTarget()
	{
		return currentTarget != null;
	}

	public Transform LockOn()
	{
		return currentTarget;
	}

	public void ChangeLockOn()
	{
		if (currentTarget == null || enemyLists.Count == 0) return;

		int currentIndex = enemyLists.IndexOf(currentTarget);

		currentIndex++;
		if (currentIndex >= enemyLists.Count)
		{
			currentIndex = 0;
		}

		currentTarget = enemyLists[currentIndex];
	}
}
