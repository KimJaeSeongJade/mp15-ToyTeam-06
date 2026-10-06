using System.Collections.Generic;
using UnityEngine;

public class LockOnController : MonoBehaviour
{
	private Transform currentTarget;
	private List<Transform> enemyLists = new();
	private MonsterController monster;

	// ---------------------

	public void AddEnemy(GameObject enemy)
	{
		for (int i = 0; i < enemyLists.Count; i++)
		{
			if (enemy == enemyLists[i].gameObject)
			{
				return;
			}
		}

		enemyLists.Add(enemy.transform);
	}

	public void RemoveEnemy(GameObject enemy)
	{
		for (int i = 0; i < enemyLists.Count; i++)
		{
			if (enemy == enemyLists[i].gameObject)
			{
				monster = enemy.GetComponent<MonsterController>();
				monster.SetLockOnUi(false);

				enemyLists.RemoveAt(i);
				break;
			}
		}

		if (currentTarget != null && enemy == currentTarget.gameObject)
		{
			ClearLockOn();
		}
	}

	public bool TryLockOn()
	{
		if (enemyLists.Count == 0)
		{
			return false;
		}

		currentTarget = enemyLists[0];
		return true;
	}

	public void ClearLockOn()
	{
		for (int i = 0; i < enemyLists.Count; i++)
		{
			monster = enemyLists[i].GetComponent<MonsterController>();
			monster.SetLockOnUi(false);
		}

		currentTarget = null;
	}

	public bool HasTarget()
	{
		return currentTarget != null;
	}

	public Transform LockOn()
	{
		monster = currentTarget.GetComponent<MonsterController>();
		monster.SetLockOnUi(true);

		return currentTarget;
	}

	public void ChangeLockOn()
	{
		if (currentTarget == null || enemyLists.Count == 0)
		{
			return;
		}

		int currentIndex = enemyLists.IndexOf(currentTarget);

		currentIndex++;
		if (currentIndex >= enemyLists.Count)
		{
			currentIndex = 0;
		}

		ClearLockOn();

		currentTarget = enemyLists[currentIndex];
	}
}
