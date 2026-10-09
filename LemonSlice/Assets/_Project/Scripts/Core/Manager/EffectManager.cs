using UnityEngine;
using UnityEngine.Serialization;

public class EffectManager : SingletonBehaviour<EffectManager>
{
	[SerializeField] private GameObject bloodPrefab;
	[SerializeField] private GameObject slashPrefab;
	[SerializeField] private GameObject playerHitEffectPrefab;
	[SerializeField] private GameObject monsterHitEffectPrefab;

public GameObject PlayBloodEffect(Vector3 pos, Vector3 direction)
	{
		Quaternion rot = Quaternion.LookRotation(direction);
		return PoolManager.Instance
			.Take(bloodPrefab)
			.SetPosition(pos)
			.SetRotation(rot)
			.Build();
	}

	public void StopEffect(GameObject go)
	{
		PoolManager.Instance.TryReturn(go);
	}

	public GameObject PlaySlashEffect(Vector3 pos, Quaternion rot)
	{
		return PoolManager.Instance
			.Take(slashPrefab)
			.SetPosition(pos)
			.SetRotation(rot)
			.Build();
	}

	public GameObject PlayHitEffect(Vector3 pos)
	{
		return PoolManager.Instance
			.Take(playerHitEffectPrefab)
			.SetPosition(pos)
			.Build();
	}

	public GameObject PlayMonsterHitEffect(Vector3 pos)
	{
		return PoolManager.Instance
			.Take(monsterHitEffectPrefab)
			.SetPosition(pos)
			.Build();
	}
}
