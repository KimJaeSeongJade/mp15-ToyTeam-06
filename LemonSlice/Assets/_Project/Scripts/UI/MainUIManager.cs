using TMPro;
using UnityEngine;

public class MainUIManager : SingletonBehaviour<MainUIManager>
{
	[SerializeField] private KeyCode isPressedPause = KeyCode.Escape;
	[SerializeField] private TextMeshProUGUI scoreUI;
	[SerializeField] private TextMeshProUGUI totalTimeUI;
	[SerializeField] private GameObject bossUI;
	[SerializeField] private GameObject pauseUI;
	[SerializeField] private GameObject gameOverUI;

	private void Start()
	{
		RefreshGameState();
	}

	private void Awake()
	{
		GameManager.Instance.ResetGameData();
		pauseUI.SetActive(false);
		bossUI.SetActive(false);
	}
	private void Update()
	{
		RefreshTimeUI();
		RefreshScoreUI();
		if (Input.GetKeyDown(isPressedPause))
		{
			ControlPauseUI();
		}
		ShowGameOver();
	}

	private void ControlPauseUI()
	{
		bool isActive = !pauseUI.activeSelf;

		if (!isActive)
		{
			if (GameManager.Instance.isTutorial)
			{
				GameManager.Instance.ChangeState(GameState.Tutorial);
			}
			else
			{
				GameManager.Instance.ChangeState(GameState.PlayMain);
			}
		}
		else
		{
			GameManager.Instance.ChangeState(GameState.Paused);
		}

		pauseUI.gameObject.SetActive(isActive);
	}

	public void RefreshScoreUI()
	{
		int score = GameManager.Instance.CurrentScore;
		scoreUI.text = $"{score: 0 0}";
	}

	public void RefreshTimeUI()
	{
		int totalTime = (int)GameManager.Instance.PlayTime;
		int min = totalTime / 60;
		int sec = totalTime % 60;

		totalTimeUI.text = $"{min:0 0} : {sec:0 0}";
	}

	private void RefreshGameState()
	{
		GameManager.Instance.ChangeState(GameState.PlayMain);
	}

	public void SetBossUI(bool isActive)
	{
		bossUI.SetActive(isActive);
	}
	public void SetPauseUI(bool isActive)
	{
		pauseUI.SetActive(isActive);
  }

	private void ShowGameOver()
	{
		if (GameManager.Instance.CurrentState == GameState.GameOver)
		{
			gameOverUI.SetActive(true);
		}
	}

}
