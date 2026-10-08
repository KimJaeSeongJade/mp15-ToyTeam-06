using UnityEngine;

public class PlayerAnimHandler : MonoBehaviour
{
	[SerializeField] private Animator animator;

	[SerializeField] private string moveXParam;
	[SerializeField] private string moveZParam;
	[SerializeField] private string idleAndMoveAnimName;
	[SerializeField] private string rollAnimName;
	[SerializeField] private string attackAnimName;
	[SerializeField] private string dieAnimName;
	[SerializeField] private string hitAnimName;
	[SerializeField] private string knockDownAnimName;
	[SerializeField] private string standUpAnimName;

	[SerializeField] private int hitAnimCount;

	[SerializeField] private GameObject slashEffect;

	private void Awake()
	{
		CacheComponents();
	}

	public void PlayIdleAndMoveAnim()
	{
		animator.CrossFade(idleAndMoveAnimName,0.1f);
		//animator.Play(idleAndMoveAnimName);
	}

	public void PlayRollAnim()
	{
		// animator.CrossFade(rollAnimName,0.5f);
		animator.Play(rollAnimName);
	}

	public void PlayAttackAnim(int animIndex)
	{
		animator.Play($"{attackAnimName}{animIndex}");
		// TODO EffectManager - 이동
		PoolManager.Instance.Take(slashEffect).SetPosition(new Vector3(
			this.gameObject.transform.position.x,
			this.gameObject.transform.position.y + 1,
			this.gameObject.transform.position.z)).Build();
	}

	public void PlayDieAnim()
	{
		animator.Play(dieAnimName);
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

	public void SetMoveParam(Vector3 input)
	{
		animator.SetFloat(moveXParam, input.x);
		animator.SetFloat(moveZParam, input.z);
	}

	private void CacheComponents()
	{
	}
}
