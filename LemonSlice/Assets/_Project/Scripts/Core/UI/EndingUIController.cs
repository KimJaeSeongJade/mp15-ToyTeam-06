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
	[SerializeField] private TextMeshProUGUI _totalCoinText; // 총 코인 텍스트

	private void Start()
	{
		CloseCredit();
		BindButtonEvents();
		RefreshClearTime();
		RefreshTotalCoin();
	}

	private void OnDisable()
	{
		UnBindButtonEvents();
	}

	private void RefreshClearTime()
	{
		int totalSeconds = (int)GameManager.Instance.PlayTime; // 소수점 안 찍히게 Int로 전환
		int min = totalSeconds / 60; // 60초로 나눠서 분 계산
		int sec = totalSeconds % 60; // 60초로 나누고 나머지로 초 계산
		_clearTimeText.text = $"Clear Time : {min:00} : {sec:00}";
	}

	private void RefreshTotalCoin()
	{
		int totalCoin = GameManager.Instance.CurrentScore;
		_totalCoinText.text = $"Total Coin : {totalCoin:00}";
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
		GameManager.Instance.ChangeState(GameState.Title);
		GameManager.Instance.ResetGameData();
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
