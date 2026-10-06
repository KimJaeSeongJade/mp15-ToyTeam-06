using UnityEngine;

public class PlayerAnimHandler : MonoBehaviour
{
	[SerializeField] private Animator animator;

	[SerializeField] private string moveXParam;
	[SerializeField] private string moveZParam;
	[SerializeField] private string moveAnimParam;
	[SerializeField] private string rollAnimParam;
	[SerializeField] private string attackAnimParam;
	[SerializeField] private string dieAnimParam;

	private void Awake()
	{
		CacheComponents();
	}

	public void PlayIdleAndMoveAnim()
	{
		animator.CrossFade(moveAnimParam,0.1f);
	}

	public void PlayRollAnim()
	{
		animator.CrossFade(rollAnimParam,0.1f);
	}

	public void PlayAttackAnim(int animIndex)
	{
		animator.Play($"{attackAnimParam}{animIndex}");
	}

	public void PlayDieAnim()
	{
		animator.Play(dieAnimParam);
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
