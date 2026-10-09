using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIManager : SingletonBehaviour<TutorialUIManager>
{
	[SerializeField] private KeyCode isPressedExit = KeyCode.Return;
	[SerializeField] private KeyCode isPressedPause = KeyCode.Escape;
	[SerializeField] private GameObject[] TutorialUI;
	[SerializeField] private GameObject pauseUI;
	[SerializeField] private TextMeshProUGUI scoreUI;
	[SerializeField] private TextMeshProUGUI totalTimeUI;

	private bool isOpenTutorialUI;

	// --------- 이벤트 함수 ------------

	private void Awake()
	{
		RefreshGameState();

		for(int i = 0; i < TutorialUI.Length; i++)
		{
			TutorialUI[i].SetActive(false);
		}

		ShowTutorialUI(0);
	}

	private void Update()
	{
		RefreshScoreUI();
		RefreshTimeUI();

		if (Input.GetKeyDown(isPressedPause))
		{
			if (isOpenTutorialUI)
			{
				ExitTutorialUI();
			}
			else
			{
				ControlPauseUI();
			}
		}
	}

	// -------------------------------

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

	public void ShowTutorialUI(int index)
	{
		TutorialUI[index].SetActive(true);

		isOpenTutorialUI = true;

		Time.timeScale = 0f;
	}

	public void ExitTutorialUI()
	{
		GameObject openUI = null;

		for (int i = 0; i < TutorialUI.Length; i++)
		{
			if (TutorialUI[i].activeSelf)
			{
				openUI = TutorialUI[i];
			}
		}

		if (openUI == null) return;

		openUI.SetActive(false);

		isOpenTutorialUI = false;

		Time.timeScale = 1f;
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
		GameManager.Instance.ChangeState(GameState.Tutorial);
		GameManager.Instance.ResetGameData();
	}
}
