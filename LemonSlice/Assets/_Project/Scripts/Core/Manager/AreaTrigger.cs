using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
	[SerializeField] private GameFlowManager flowManager;
	[SerializeField] private LayerMask targetLayerMask;

	// AreaData
	[SerializeField] private GameObject prefab;
	[SerializeField] private List<Transform> spawnList;

	private bool _canActivate;

	private void Start()
	{
		_canActivate = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!_canActivate)
		{
			return;
		}

		if (targetLayerMask.Contains(other.gameObject.layer))
		{
			_canActivate = false;
			flowManager.EnterArea(spawnList, prefab);
		}
	}
}
