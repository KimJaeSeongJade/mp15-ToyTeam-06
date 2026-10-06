using TMPro;
using UnityEngine;

public class MainUIManager : SingletonBehaviour<MainUIManager>
{
	[SerializeField] private TextMeshProUGUI scoreUI;
	[SerializeField] private TextMeshProUGUI totalTimeUI;
	[SerializeField] private GameObject bossUI;

	private void Update()
	{
		RefreshTimeUI();
		RefreshScoreUI();
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

	public void SetBossUI(bool isActive)
	{
		bossUI.SetActive(isActive);
	}
}
