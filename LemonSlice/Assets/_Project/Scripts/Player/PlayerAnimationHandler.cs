using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
	[SerializeField] private Animator _animator;

	private void Awake()
	{
		CacheComponents();
	}

	public void PlayerIdleAnimation()
	{
		_animator.Play("PlayerIdle");
	}

	public void PlayerRollAnimation()
	{
		_animator.Play("PlayerRoll");
	}

	private void CacheComponents()
	{
		_animator = GetComponent<Animator>();
	}
}
