using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimHandler : MonoBehaviour
{
	[SerializeField] private Animator animator;

	[SerializeField] private string idleAnimParam;

	public void PlayIdleAnim()
	{
		animator.Play(idleAnimParam);
	}
}
