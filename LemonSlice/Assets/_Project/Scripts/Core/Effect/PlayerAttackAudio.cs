using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAudio : MonoBehaviour, IPoolable
{
	[SerializeField] PoolType _poolType;
	[SerializeField] private float audioDalay;

	private AudioSource _audioSource;
	private WaitForSeconds audioDalayWait;
	private WaitForSeconds audioReturnWait;

	private void Awake() => CacheComponents();

	private void CacheComponents()
	{
		_audioSource = GetComponent<AudioSource>();
		audioDalayWait = new WaitForSeconds(audioDalay);
		audioReturnWait = new WaitForSeconds(_audioSource.clip.length);
	}

	private void OnEnable()
	{
		StartCoroutine(PlayAttackAudioRoutine());
	}

	private IEnumerator PlayAttackAudioRoutine()
	{
		yield return audioDalayWait;
		_audioSource.Play();
		StartCoroutine(ReturnToPoolRoutine());
	}

	private IEnumerator ReturnToPoolRoutine()
	{
		yield return audioReturnWait;
		PoolManager.Instance.TryReturn(gameObject);
	}

	public PoolType PoolId => _poolType;
}
