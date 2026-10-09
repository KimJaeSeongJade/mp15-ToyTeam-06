using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantAnimEventSender : MonoBehaviour
{
	private MutantController _mutantController;

	private void Awake() => CacheComponents();

	private void CacheComponents()
	{
		_mutantController = GetComponentInParent<MutantController>();
	}

	public void OnAnimEvent(string animEvent)
	{
		if (_mutantController != null)
		{
			_mutantController.OnAnimEvent(animEvent);
		}
	}
}
