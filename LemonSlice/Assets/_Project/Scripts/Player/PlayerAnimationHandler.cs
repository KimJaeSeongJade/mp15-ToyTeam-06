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

	public void PlayBlendAnim()
	{
		animator.Play("IdleAndMove");
	}

	public void PlayRollAnim()
	{
		animator.Play("PaladinRoll");
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
