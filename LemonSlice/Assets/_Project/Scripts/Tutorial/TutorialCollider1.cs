using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollider1 : MonoBehaviour
{
	[SerializeField] private LayerMask targetLayer;

	private void OnTriggerEnter(Collider other)
	{
		if (IsInPlayerLayer(other.gameObject))
		{
			TutorialUIManager.Instance.ShowTutorialUI(1);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (IsInPlayerLayer(other.gameObject))
		{
			gameObject.SetActive(false);
		}
	}

	private bool IsInPlayerLayer(GameObject target)
	{
		return (targetLayer.value & (1 << target.layer)) != 0;
	}
}
