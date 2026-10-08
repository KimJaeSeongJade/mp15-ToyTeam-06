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

	[SerializeField] private string hitAnimName;
	[SerializeField] private string knockDownAnimName;
	[SerializeField] private string standUpAnimName;

	[SerializeField] private int hitAnimCount;

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

	public void PlayHitAnim(int hitIndex)
	{
		animator.Play($"{hitAnimName}{hitIndex}");
	}

	public void PlayKnockDownAnim()
	{
		animator.Play(knockDownAnimName);
	}

	public void PlayStandUpAnim()
	{
		animator.Play(standUpAnimName);
	}

	private void CacheComponents()
	{
		if (animator == null)
		{
			animator = GetComponent<Animator>();
		}
	}
}
