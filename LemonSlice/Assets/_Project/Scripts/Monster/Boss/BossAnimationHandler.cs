using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationHandler : MonoBehaviour
{
	[SerializeField] private Animator animator;

	[SerializeField] private string idleAnimParam;
	[SerializeField] private string chaseAnimParam;
	[SerializeField] private string attackAnimParam;
	[SerializeField] private string dieAnimParam;

	private void Awake()
	{
		CacheComponents();
	}

	public void PlayIdleAnim()
	{
		animator.Play(idleAnimParam);
	}

	public void PlayChaseAnim()
	{
		animator.Play(chaseAnimParam);
	}

	public void PlayAttackAnim()
	{
		animator.Play(attackAnimParam);
	}

	public void PlayDieAnim(int animIndex)
	{
		animator.Play($"{dieAnimParam}{animIndex}");
	}

	private void CacheComponents()
	{
		if (animator == null)
		{
			animator = GetComponent<Animator>();
		}
	}
}
