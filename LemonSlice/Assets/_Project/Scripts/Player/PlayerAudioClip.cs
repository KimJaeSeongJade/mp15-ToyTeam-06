using System.Collections;
using UnityEngine;

public class PlayerAudioClip : MonoBehaviour
{
	[SerializeField] private AudioClip moveSound;
	[SerializeField] private AudioClip rollSound1;
	[SerializeField] private AudioClip rollSound2;
	// [SerializeField] private AudioSource moveSoundManager;

	[SerializeField] private AudioClip hitSound1;
	[SerializeField] private AudioClip hitSound2;
	// [SerializeField] private AudioSource hitSoundManager;
	private Coroutine rollSound1Routine;

	private WaitForSeconds rollSound1Wait;

	private void Awake() => CacheComponents();

	public AudioClip HitSoundClip()
	{
		return hitSound1;
	}

	public AudioClip RollSoundClip()
	{
		return rollSound1;
	}

	public void StopRollSoundRoutine()
	{
		StopCoroutine(rollSound1Routine);
	}

	private IEnumerator RollSoundRoutine()
	{
		// moveSoundManager.PlayOneShot(rollSound1);
		yield return rollSound1Wait;
		// moveSoundManager.PlayOneShot(rollSound2);
	}

	public void StopMoveSound()
	{
		// moveSoundManager.Stop();
	}

	public void PlayHitSound()
	{
		// hitSoundManager.Play();
	}

	public void StopHitSound()
	{
		// hitSoundManager.Stop();
	}

	private void CacheComponents()
	{
		// moveSoundManager = GetComponent<AudioSource>();
		// hitSoundManager = GetComponent<AudioSource>();
		rollSound1Wait = new WaitForSeconds(rollSound1.length);
	}
}
