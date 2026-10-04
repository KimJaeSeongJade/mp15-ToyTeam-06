using UnityEngine;

public class AudioManager : SingletonBehaviour<AudioManager>
{
	[field : SerializeField] public AudioClip[] AudioClips { get; private set; }

	/// 0: 타이틀
	/// 1: 비전투
	/// 2: 몬스터 전투
	/// 3: 보스 전투
	/// 4: 엔딩

	public AudioSource audioSource { get; private set; } // 오디오 매니저 재생처
	public BackGroundAudioType AudioState; // 재생 상태

	private void Awake()
	{
		InitBGM();
		CacheComponents();
		SetSingleton();
	}

	private void Update()
	{
		PlayBGM();
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			AudioState = BackGroundAudioType.Title;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			AudioState = BackGroundAudioType.Idle;
		}
	}

	private void CacheComponents()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void InitBGM() // 오디오 상태 초기화
	{
		audioSource.clip = AudioClips[0];
		audioSource.Play();
	}

	public void PlayBGM() // 열거형을 통해 클립을 바꾸어서 재생
	{
		switch (AudioState)
		{
			case BackGroundAudioType.Title:
				audioSource.clip = AudioClips[0];
				audioSource.Play();
				break;
			case BackGroundAudioType.Idle:
				audioSource.clip = AudioClips[1];
				audioSource.Play();
				break;
			case BackGroundAudioType.MonsterBattle:
				audioSource.clip = AudioClips[2];
				audioSource.Play();
				break;
			case BackGroundAudioType.BossBattle:
				audioSource.clip = AudioClips[3];
				audioSource.Play();
				break;
			case BackGroundAudioType.Ending:
				audioSource.clip = AudioClips[4];
				audioSource.Play();
				break;
			default:
				break;
		}
	}

	// 현재 확인되는 문제
	// Stop 하지 않으면 오디오 중첩 재생되는 문제 해결 필요
	// 오디오를 Update 딴에서 교체해주어야하는데 조건식 설정...
}
