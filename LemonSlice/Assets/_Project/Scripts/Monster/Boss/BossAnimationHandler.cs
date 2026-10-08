using UnityEngine;

public class BossAnimationHandler : MonoBehaviour
{
	[SerializeField] private Animator animator;

	[SerializeField] private string idleAnimParam;
	[SerializeField] private string chaseAnimParam;
	[SerializeField] private string attackAnimParam;
	[SerializeField] private string knockbackAnimParam;
	[SerializeField] private string groggyAnimParam;
	[SerializeField] private string dieAnimParam;

	[SerializeField] private string startAttack;
	[SerializeField] private string endAttack;
	[SerializeField] private string endHitBox;

	public string StartAttack => startAttack;
	public string EndAttack => endAttack;
	public string EndHitBox => endHitBox;

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

	public void PlayKnockbackAnim()
	{
		animator.Play(knockbackAnimParam);
	}

	public void PlayGroggyAnim()
	{
		animator.Play(groggyAnimParam);
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
