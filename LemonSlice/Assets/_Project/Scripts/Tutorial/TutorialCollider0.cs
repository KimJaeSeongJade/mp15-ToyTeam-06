using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollider0 : MonoBehaviour
{
	[SerializeField] private LayerMask targetLayer;

	private void OnTriggerEnter(Collider other)
	{
		if (IsInPlayerLayer(other.gameObject))
		{
			TutorialManager.Instance.ShowTutorialUI(0);
		}
	}

	private bool IsInPlayerLayer(GameObject target)
	{
		return (targetLayer.value & (1 << target.layer)) != 0;
	}
}
