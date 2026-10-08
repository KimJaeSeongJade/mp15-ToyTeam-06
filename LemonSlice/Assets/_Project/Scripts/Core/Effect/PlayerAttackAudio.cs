using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAudio : MonoBehaviour, IPoolable
{
	[SerializeField] PoolType _poolType;
	
	private AudioSource _audioSource;

	private void Awake() => CacheComponents();

	private void CacheComponents()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	private void OnEnable()
	{
		_audioSource.PlayOneShot(_audioSource.clip);
	}
	
	public PoolType PoolId => _poolType;
}
