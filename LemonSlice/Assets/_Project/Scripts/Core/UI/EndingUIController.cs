using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingUIController : MonoBehaviour
{
	[SerializeField] private Button _titleButton; // 타이틀
	[SerializeField] private Button _creditButton; // 크레딧
	[SerializeField] private Button _exitCreditButton;
	[SerializeField] private GameObject _creditPanel; // 크레딧 판넬
	[SerializeField] private TextMeshProUGUI _clearTimeText; // 클리어타임 텍스트

	//private GameManager _gameManager;

	public void CheckClearTime(int totalSeconds)
	{
		totalSeconds = (int)GameManager.Instance.PlayTime; // 소수점 안 찍히게 Int로 전환
		int min = totalSeconds / 60; // 60초로 나눠서 분 계산
		int sec = totalSeconds % 60; // 60초로 나누고 나머지로 초 계산
		_clearTimeText.text = $"Clear Time : {min:00} : {sec:00}";
	}

	private void Awake()
	{
		//_gameManager = FindObjectOfType<GameManager>();
	}

	private void Start()
	{
		CloseCredit();
		BindButtonEvents();
		CheckClearTime(0);
	}

	private void OnDisable()
	{
		UnBindButtonEvents();
	}

	private void BindButtonEvents()
	{
		_titleButton.onClick.AddListener(LoadTitle);
		_creditButton.onClick.AddListener(OpenCredit);
		_exitCreditButton.onClick.AddListener(CloseCredit);
	}

	private void UnBindButtonEvents()
	{
		_titleButton.onClick.RemoveListener(LoadTitle);
		_creditButton.onClick.RemoveListener(OpenCredit);
		_exitCreditButton.onClick.RemoveListener(CloseCredit);
	}

	private void LoadTitle() // 타이틀 씬 전환
	{
		SceneManager.LoadScene("Title");
	}

	private void OpenCredit() // 크레딧 창 활성화
	{
		_creditPanel.SetActive(true);
	}

	private void CloseCredit() // 크레딧 창 비활성화
	{
		_creditPanel.SetActive(false);
	}
}
