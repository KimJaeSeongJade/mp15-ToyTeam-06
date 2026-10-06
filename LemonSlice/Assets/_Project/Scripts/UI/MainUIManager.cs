using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI scoreUI;
	[SerializeField] private TextMeshProUGUI totalTimeUI;

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
}
