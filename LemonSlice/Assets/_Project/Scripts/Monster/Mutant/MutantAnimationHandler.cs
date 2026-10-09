using UnityEngine;

public class MutantAnimationHandler : MonoBehaviour
{
	[SerializeField] private Animator animator;

	[SerializeField] private string idleAnimParam;
	[SerializeField] private string chaseAnimParam;
	[SerializeField] private string[] attackAnimParams;
	[SerializeField] private string dieAnimParam;
	[SerializeField] private string phaseChangeAnimParam;
	[SerializeField] private string startAttackEventKey;
	[SerializeField] private string endHitBoxEventKey;
	[SerializeField] private string endAttackEventKey;
	[SerializeField] private string endPhaseChangeEventKey;

	public int a;
	public string StartAttack => startAttackEventKey;
	public string EndHitBox => endHitBoxEventKey;
	public string EndAttack => endAttackEventKey;
	public string EndPhaseChange => endPhaseChangeEventKey;

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

	public void PlayAttackAnim(int animIndex)
	{
		a = animIndex;
		animator.Play(attackAnimParams[animIndex]);
	}

	public void PlayPhaseChangeAnim()
	{
		animator.Play(phaseChangeAnimParam);
	}

	public void PlayDieAnim()
	{
		animator.Play(dieAnimParam);
	}

	private void CacheComponents()
	{
			animator = GetComponent<Animator>();
	}
}
