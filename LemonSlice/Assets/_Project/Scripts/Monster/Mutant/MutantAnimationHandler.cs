using UnityEngine;

public class MutantAnimationHandler : MonoBehaviour
{
	[SerializeField] private Animator animator;

	[SerializeField] private string idleAnimParam;
	[SerializeField] private string chaseAnimParam;
	[SerializeField] private string attackAnimParam;
	[SerializeField] private string groggyAnimParam;
	[SerializeField] private string dieAnimParam;
	[SerializeField] private string phaseChangeAnimParam;
	[SerializeField] private string screamAnimParam;

	[SerializeField] private string startAttack;
	[SerializeField] private string endHitBox;
	[SerializeField] private string endAttack;
	[SerializeField] private string endPhaseChange;
	[SerializeField] private string endScream;
	[SerializeField] private string startJump;

	public string StartAttack => startAttack;
	public string EndHitBox => endHitBox;
	public string EndAttack => endAttack;
	public string EndPhaseChange => endPhaseChange;
	public string EndScream => endScream;
	public string StartJump => startJump;

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
		animator.Play($"{attackAnimParam}{animIndex}");
	}

	public void PlayGroggyAnim()
	{
		animator.Play(groggyAnimParam);
	}

	public void PlayPhaseChangeAnim()
	{
		animator.Play(phaseChangeAnimParam);
	}

	public void PlayScreamAnim()
	{
		animator.Play(screamAnimParam);
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
