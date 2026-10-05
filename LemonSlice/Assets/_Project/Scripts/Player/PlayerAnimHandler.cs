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

	[SerializeField] private string endRollAnim;
	[SerializeField] private string endAttackAnim;
	[SerializeField] private string openCombo;
	[SerializeField] private string closeCombo;

	public string EndRollAnim => endRollAnim;
	public string EndAttackAnim => endAttackAnim;
	public string OpenCombo => openCombo;
	public string CloseCombo => closeCombo;

	private void Awake()
	{
		CacheComponents();
	}

	public void PlayIdleAndMoveAnim()
	{
		animator.Play(moveAnimParam);
	}

	public void PlayRollAnim()
	{
		animator.Play(rollAnimParam);
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
