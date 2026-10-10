using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
	public GameState CurrentState { get; private set; }

	public int CurrentScore { get; private set; }
	public float PlayTime { get; private set; }
	public bool isTutorial;

	private void Awake() => SetSingleton();

	private void Start()
	{
		isTutorial = true;
	}

	private void Update()
	{
		if (CurrentState == GameState.PlayMain || CurrentState == GameState.Tutorial)
		{
			PlayingTime();
		}
	}

	public void ChangeState(GameState state)
	{
		CurrentState = state; // 게임 상태 변경

		switch (CurrentState)
		{
			case GameState.PlayMain:
			{
				LockCursor();
				Time.timeScale = 1;
				break;
			}
			case GameState.StageClear:
			{
				SceneManager.LoadScene("Ending");
				UnlockCursor();
				break;
			}
			case GameState.GameOver:
			{
				UnlockCursor();
				break;
			}
			case GameState.Paused:
			{
				UnlockCursor();
				Time.timeScale = 0;
				break;
			}
			case GameState.Tutorial:
			{
				LockCursor();
				Time.timeScale = 1;
				break;
			}
		}
	}

	public void AddScore(int score)
	{
		CurrentScore += score; // 코인 획득시 점수 추가
	}

	private void PlayingTime()
	{
		PlayTime += Time.deltaTime;
	}

	public void ResetGameData()
	{
		// 게임 오버 -> 점수, 시간 초기화
		CurrentScore = 0;
		PlayTime = 0f;
	}

	private void LockCursor() // 마우스 잠금
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void UnlockCursor() // 마우스 활성화
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
