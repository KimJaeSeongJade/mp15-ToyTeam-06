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
	[SerializeField] private GameObject[] attackAudio;

	[SerializeField] private PlayerAudioClip playerAudioClip;

	private GameObject currentAtatckSound;
	private AudioSource audioSource;

	private void Awake()
	{
		CacheComponents();
	}

	public void PlayIdleAndMoveAnim()
	{
		animator.CrossFade(idleAndMoveAnimName, 0.1f);
		//animator.Play(idleAndMoveAnimName);
	}

	public void PlayRollAnim()
	{
		// animator.CrossFade(rollAnimName,0.5f);
		animator.Play(rollAnimName);
		//audioSource.PlayOneShot(playerAudioClip.RollSoundClip());
	}

	public void PlayAttackAnim(int animIndex)
	{
		animator.Play($"{attackAnimName}{animIndex}");
		//if (animIndex >= 1)
		//{
		//	int audioNumber = animIndex - 1;
		//	Debug.Log(audioNumber);
		//	currentAtatckSound = AttackSound(audioNumber);
		//}
	}

	private GameObject AttackSound(int audioNumber)
	{
		return PoolManager.Instance.Take(attackAudio[audioNumber]).Build();
	}

	private void StopAttackSound()
	{
		if (currentAtatckSound != null)
		{
			PoolManager.Instance.TryReturn(currentAtatckSound);
		}
	}

	public void PlayDieAnim()
	{
		animator.Play(dieAnimName);
		//StopAttackSound();
	}

	public void PlayHitAnim(int hitIndex)
	{
		animator.Play($"{hitAnimName}{hitIndex}");
		//StopAttackSound();
		//audioSource.PlayOneShot(playerAudioClip.HitSoundClip());
	}

	public void PlayKnockDownAnim()
	{
		animator.Play(knockDownAnimName);
		//StopAttackSound();
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
		audioSource = GetComponent<AudioSource>();
	}
}
