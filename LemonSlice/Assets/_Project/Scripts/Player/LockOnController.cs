using System.Collections.Generic;
using UnityEngine;

public class LockOnController : MonoBehaviour
{
	private Transform currentTarget;
	private List<Transform> enemyLists = new();
	private ILockonable monster;

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
				monster = enemy.GetComponent<ILockonable>();
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
			monster = enemyLists[i].GetComponent<ILockonable>();
			monster.SetLockOnUi(false);
		}

		currentTarget = null;
		transform.parent.rotation = Quaternion.Euler(0, transform.parent.eulerAngles.y, 0f);
	}

	public bool HasTarget()
	{
		return currentTarget != null;
	}

	public Transform LockOn()
	{
		monster = currentTarget.GetComponent<ILockonable>();
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
