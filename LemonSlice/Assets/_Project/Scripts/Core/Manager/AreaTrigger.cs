using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
	[SerializeField] private GameFlowManager flowManager;
	[SerializeField] private LayerMask targetLayerMask;

	// AreaData
	[SerializeField] private GameObject prefab;
	[SerializeField] private List<Transform> spawnList;

	private void OnTriggerEnter(Collider other)
	{
		if (targetLayerMask.Contains(other.gameObject.layer))
		{
			flowManager.EnterArea(spawnList, prefab);
		}
	}
}
