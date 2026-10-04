using UnityEngine;

public class BossAnimationHandler : MonoBehaviour
{
	[SerializeField] private Animator animator;

	[SerializeField] private string idleAnimParam;
	[SerializeField] private string chaseAnimParam;
	[SerializeField] private string attackAnimParam;
	[SerializeField] private string dieAnimParam;
	[SerializeField] private string knockbackAnimParam;

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
