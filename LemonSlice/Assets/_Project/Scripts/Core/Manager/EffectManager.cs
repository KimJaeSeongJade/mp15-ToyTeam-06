using UnityEngine;

public class EffectManager : SingletonBehaviour<EffectManager>
{
	[SerializeField] private GameObject bloodPrefab;
	[SerializeField] private GameObject slashPrefab;

	public GameObject PlayBloodEffect(Vector3 pos, Vector3 direction)
	{
		Quaternion rot = Quaternion.LookRotation(direction);
		return PoolManager.Instance.Take(bloodPrefab)
			.SetPosition(pos)
			.SetRotation(rot)
			.Build();
	}

	public void StopBloodEffect(GameObject go)
	{
		PoolManager.Instance.TryReturn(go);
	}

	public GameObject PlaySlashEffect(Vector3 pos, Vector3 direction)
	{
		Quaternion rot = Quaternion.LookRotation(direction);
		return PoolManager.Instance.Take(slashPrefab)
			.SetPosition(pos)
			.SetRotation(rot)
			.Build();
	}
}
