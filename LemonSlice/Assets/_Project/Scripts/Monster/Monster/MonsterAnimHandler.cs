using System;
using UnityEngine;

public class MonsterAnimHandler : MonoBehaviour
{
	[SerializeField] private Animator animator;

	[SerializeField] private string idleAnimParam;
	[SerializeField] private string chaseAnimParam;
	[SerializeField] private string attackAnimParam;
	[SerializeField] private string dieAnimParam;

	[SerializeField] private string endAttackAnim;
	[SerializeField] private string endDieAnim;
	public string EndAttackAnim => endAttackAnim;
	public string EndDieAnim => endDieAnim;

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

	public void PlayDieAnim()
	{
		animator.Play(dieAnimParam);
	}


	private void CacheComponents()
	{
		if (animator == null)
		{
			animator = GetComponent<Animator>();
		}
	}
}
