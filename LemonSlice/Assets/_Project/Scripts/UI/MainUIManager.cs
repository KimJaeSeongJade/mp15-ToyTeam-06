using TMPro;
using UnityEngine;

public class MainUIManager : SingletonBehaviour<MainUIManager>
{
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
		pauseUI.SetActive(false);
	}
	private void Update()
	{
		RefreshTimeUI();
		RefreshScoreUI();
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			pauseUI.gameObject.SetActive(true);
			GameManager.Instance.ChangeState(GameState.Paused);
		}
		ShowGameOver();
	}

	public void RefreshScoreUI()
	{
		int score = GameManager.Instance.CurrentScore;

		if (score < 10)
		{
			scoreUI.text = $": 0{score}";
		}
		else
		{
			scoreUI.text = $": {score}";
		}
	}

	public void RefreshTimeUI()
	{
		int totalTime = (int)GameManager.Instance.PlayTime;
		int min = totalTime / 60;
		int sec = totalTime % 60;

		totalTimeUI.text = $"PlayTime - {min:00} : {sec:00}";
	}

	private void RefreshGameState()
	{
		GameManager.Instance.ChangeState(GameState.Playing);
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
