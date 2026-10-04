using UnityEngine;

public class AudioManager : SingletonBehaviour<AudioManager>
{
	[field : SerializeField] public AudioClip[] audioClips { get; private set; }
	/// 0: 타이틀
	/// 1: 비전투
	/// 2: 몬스터 전투
	/// 3: 보스 전투
	/// 4: 엔딩

	public AudioSource audioSource { get; private set; } // 오디오 매니저 재생처

	private void Awake()
	{
		CacheComponents();
		SetSingleton();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{

		}
	}

	private void CacheComponents()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void PlayTitleBGM() // 타이틀 음악 재생
	{
		audioSource.clip = audioClips[0];
	}

	public void PlayIdleBGM() // 비전투 음악 재생
	{
		audioSource.clip = audioClips[1];
	}

	public void PlayMonsterBGM() // 몬스터 전투 음악 재생
	{
		audioSource.clip = audioClips[2];
	}

	public void PlayBossBGM() // 보스 전투 음악 재생
	{
		audioSource.clip = audioClips[3];
	}

	public void PlayEndingBGM() // 엔딩 음악 재생
	{
		audioSource.clip = audioClips[4];
	}

	public void PauseBGM() // 음악 일시정지
	{
		audioSource.Pause();
	}
}
