using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialCollider3 : MonoBehaviour
{
	[SerializeField] private LayerMask targetLayer;

	private void OnTriggerEnter(Collider other)
	{
		if (IsInPlayerLayer(other.gameObject))
		{
			GameManager.Instance.isTutorial = false;
			SceneManager.LoadScene("MainGame");
		}
	}

	private bool IsInPlayerLayer(GameObject target)
	{
		return (targetLayer.value & (1 << target.layer)) != 0;
	}
}
