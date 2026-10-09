using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollider2 : MonoBehaviour
{
	[SerializeField] private LayerMask targetLayer;
	[SerializeField] private GameObject[] tutorialMonster;
	[SerializeField] private Canvas CantEntercanvas;

	private BoxCollider collider;

	// --------- 이벤트 함수 ------------

	private void Awake() => CacheComponents();

	private void Update()
	{
		if (!tutorialMonster[0].activeSelf && !tutorialMonster[1].activeSelf)
		{
			collider.isTrigger = true;
			CantEntercanvas.enabled = false;
		}
	}

	// -------------------------------

	private void OnTriggerEnter(Collider other)
	{
		if (IsInPlayerLayer(other.gameObject))
		{
			TutorialUIManager.Instance.ShowTutorialUI(2);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (IsInPlayerLayer(other.gameObject))
		{
			gameObject.SetActive(false);
		}
	}

	private void CacheComponents()
	{
		collider = GetComponent<BoxCollider>();
	}

	private bool IsInPlayerLayer(GameObject target)
	{
		return (targetLayer.value & (1 << target.layer)) != 0;
	}
}
