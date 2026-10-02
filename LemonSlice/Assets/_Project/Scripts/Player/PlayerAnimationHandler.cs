using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private string moveXParam;
	[SerializeField] private string moveZParam;

	private void Awake()
	{
		CacheComponents();
	}

	public void PlayerIdleAnim()
	{
		animator.Play("PlayerIdle");
	}

	public void PlayerRollAnim()
	{
		animator.Play("PlayerRoll");
	}

	public void SetMoveParam(Vector3 input)
	{
		animator.SetFloat(moveXParam, input.x);
		animator.SetFloat(moveZParam, input.z);
	}

	private void CacheComponents()
	{
	}
}
