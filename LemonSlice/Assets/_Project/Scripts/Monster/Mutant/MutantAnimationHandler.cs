using UnityEngine;

public class MutantAnimationHandler : MonoBehaviour
{
	[SerializeField] private Animator animator;

	[SerializeField] private string idleAnimParam;
	[SerializeField] private string chaseAnimParam;
	[SerializeField] private string[] attackAnimParams;
	[SerializeField] private string dieAnimParam;


	[SerializeField] private string startAttackEventKey;
	[SerializeField] private string endHitBoxEventKey;
	[SerializeField] private string endAttackEventKey;
	[SerializeField] private string endPhaseChangeEventKey;

	public string StartAttack => startAttackEventKey;
	public string EndHitBox => endHitBoxEventKey;
	public string EndAttack => endAttackEventKey;
	public string EndPhaseChange => endPhaseChangeEventKey;

	private void Awake()
	{
		if (animator == null)
		{
			animator = GetComponent<Animator>();
		}
	}

	public void PlayIdleAnim()
	{
		animator.Play(idleAnimParam);
	}

	public void PlayChaseAnim()
	{
		animator.Play(chaseAnimParam);
	}

	public void PlayAttackAnim(int animIndex)
	{
		if (animIndex >= 0 && animIndex < attackAnimParams.Length)
		{
			animator.Play(attackAnimParams[animIndex]);
		}
	}

	public void PlayPhaseChangeAnim()
	{
	}

	public void PlayDieAnim()
	{
		animator.Play(dieAnimParam);
	}
}
